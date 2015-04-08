using UnityEngine;
using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using VLC;

public class VLCPlayer : MonoBehaviour {

	// Vlc player internal components
	private IntPtr vlc_session = IntPtr.Zero;
	private IntPtr vlc_media = IntPtr.Zero;
	private IntPtr vlc_player = IntPtr.Zero;

	// Vlc Video components
	private Texture2D vlc_video = null;
	private uint vlc_video_xsize = 0;
	private uint vlc_video_ysize = 0;
	private Mutex vlc_video_lock = new Mutex();
	private IntPtr vlc_video_buffer = IntPtr.Zero;
	private IntPtr vlc_video_result_buffer = IntPtr.Zero;
	private Color32[] vlc_video_pixels;
	private GCHandle vlc_video_pixels_handle;
	private uint vlc_video_pitches = 0;
	private uint vlc_video_lines = 0;

	void Start() {
		// Start a VLC runtime
		vlc_session = LibVLC.libvlc_new(0, new string[] {});
		// Demo - open sample file
		OpenFile(@"E:\Downloads\Raspoutine.avi");
		// Demo - play sample
		Play();
	}

	void OpenFile(string path) {
		// Clear video infos
		vlc_video_xsize = 0;
		vlc_video_ysize = 0;
		if (vlc_video != null) {
			UnityEngine.Object.Destroy(vlc_video);
			vlc_video = null;
		}
		// Free frame buffer
		if (vlc_video_buffer != IntPtr.Zero) {
			Marshal.FreeHGlobal(vlc_video_buffer);
			vlc_video_buffer = IntPtr.Zero;
		}
		// Free old vlc player
		if (vlc_player != IntPtr.Zero) {
			LibVLC.libvlc_media_player_release(vlc_player);
			vlc_player = IntPtr.Zero;
		}
		// Free old vlc media
		if (vlc_media != IntPtr.Zero) {
			LibVLC.libvlc_media_release(vlc_media);
			vlc_media = IntPtr.Zero;
		}
		// If vlc ready
		if (vlc_session != IntPtr.Zero) {
			// Load media
			vlc_media = LibVLC.libvlc_media_new_path(vlc_session, path);
			// If media loaded
			if (vlc_media != IntPtr.Zero) {
				// Fetch meta datas from the file (such as video size)
				LibVLC.libvlc_media_parse(vlc_media);
				LibVLC.libvlc_media_track_info_t[] tracks;
				int track_size = LibVLC.libvlc_media_fetch_tracks_info(vlc_media, out tracks);
				for (int i = 0; i < track_size; i++) {
					LibVLC.libvlc_media_track_info_t track_info = tracks[i];
					// If the file contains video, get its size
					if (track_info.i_type == LibVLC.libvlc_track_type_t.libvlc_track_video) {
						vlc_video_xsize = track_info.video.i_width;
						vlc_video_ysize = track_info.video.i_height;
					}
				}
				Debug.Log("Media file Loaded: " + path + ", xsize:" + vlc_video_xsize.ToString() + ", ysize:" + vlc_video_ysize.ToString());
				// Load the player for the media
				vlc_player = LibVLC.libvlc_media_player_new_from_media(vlc_media);
				// If player creation was successfull and media has a video
				if (vlc_player != IntPtr.Zero && vlc_video_xsize > 0 && vlc_video_ysize > 0) {
					// Create reception texture
					vlc_video = new Texture2D((int)vlc_video_xsize, (int)vlc_video_ysize, TextureFormat.ARGB32, false);
					vlc_video.hideFlags = HideFlags.HideAndDontSave;
					vlc_video.wrapMode = TextureWrapMode.Clamp;
					vlc_video.filterMode = FilterMode.Point;
					if (GetComponent<Renderer>()) {
						GetComponent<Renderer>().material.mainTexture = vlc_video;
					}
					// Fetch texture raw datas pointer
					vlc_video_pixels = vlc_video.GetPixels32(0);
					vlc_video_pixels_handle.Free();
					vlc_video_pixels_handle = GCHandle.Alloc(vlc_video_pixels, GCHandleType.Pinned);
					// Set video buffer format
					LibVLC.libvlc_video_set_format_callbacks(
						vlc_player,
						VLCPlayer.VideoFormat,
						VLCPlayer.VideoUnformat,
						(IntPtr)GCHandle.Alloc(this)
					);
					// Set video buffer rendering
					LibVLC.libvlc_video_set_callbacks(
						vlc_player,
						VLCPlayer.VideoLock,
						VLCPlayer.VideoUnlock,
						VLCPlayer.VideoDisplay,
						(IntPtr)GCHandle.Alloc(this)
					);
				}
			}
			// If media coult not be loaded
			else {
				Error("Could not load VLC media file");
			}
		}
		// If vlc is in error-state
		else {
			Error("VLC module could not be loaded");
		}
	}

	// Called when video frame is gonna be decoded
	static public IntPtr VideoLock(IntPtr opaque, ref IntPtr planes) {
		VLCPlayer instance = (((GCHandle)opaque).Target as VLCPlayer);
		// Block and allocate buffer, or use already allocated buffer
		instance.vlc_video_lock.WaitOne();
		if (instance.vlc_video_buffer == IntPtr.Zero) {
			instance.vlc_video_buffer = Marshal.AllocHGlobal((Int32)(instance.vlc_video_pitches * instance.vlc_video_lines));
		}
		planes = instance.vlc_video_buffer;
		return instance.vlc_video_buffer;
	}
	// Called when video frame is decoded
	static public void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes) {
		VLCPlayer instance = (((GCHandle)opaque).Target as VLCPlayer);
		// Unlock buffer
		instance.vlc_video_lock.ReleaseMutex();
	}
	// Called when video frame is ready to render
	static public void VideoDisplay(IntPtr opaque, IntPtr picture) {
		VLCPlayer instance = (((GCHandle)opaque).Target as VLCPlayer);
		// Save the frame buffer pointer
		instance.vlc_video_result_buffer = picture;
		IntPtr pixels = instance.vlc_video_pixels_handle.AddrOfPinnedObject();
		// Copy the picture content to texture pixels :)
		if (pixels != IntPtr.Zero && picture != IntPtr.Zero) {
			// Use memcpy as the destination texture and the vlc buffer are configured with the same memory pattern
			LibVLC.memcpy(pixels, picture, (int)(instance.vlc_video_xsize * instance.vlc_video_ysize * 4));
		}
	}

	// Configure video frame format
	static public uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines) {
		VLCPlayer instance = (((GCHandle)opaque).Target as VLCPlayer);
		// Set chroma
		chroma = 'B';
		chroma <<= 8;
		chroma += 'G';
		chroma <<= 8;
		chroma += 'R';
		chroma <<= 8;
		chroma += 'A';
		// Calc pitch/lines
		pitches = width * 4;
		lines = height;
		instance.vlc_video_pitches = pitches;
		instance.vlc_video_lines = lines;
		return 1;
	}
	// Ends video frame formating
	static public void VideoUnformat(IntPtr opaque) {
		VLCPlayer instance = (((GCHandle)opaque).Target as VLCPlayer);
	}

	void Play() {
		// If vlc player is ready
		if (vlc_player != IntPtr.Zero) {
			// Call play on vlc media
			LibVLC.libvlc_media_player_play(vlc_player);
		}
		// If vlc player is not loaded
		else {
			Error("VLC Media player is not correctly initialized.");
		}
	}

	void Error(string msg) {
		// Display an error
		Debug.LogError(msg);
	}

	void Update() {
		// If video buffer ready apply copied texture on mesh
		if (vlc_video_result_buffer != IntPtr.Zero) {
			vlc_video.SetPixels32(vlc_video_pixels, 0);
			vlc_video.Apply();
		}
	}

	void OnDestroy() {
		// Clear vlc player
		if (vlc_player != IntPtr.Zero) {
			LibVLC.libvlc_media_player_release(vlc_player);
			vlc_player = IntPtr.Zero;
		}
		// Clear vlc media
		if (vlc_media != IntPtr.Zero) {
			LibVLC.libvlc_media_release(vlc_media);
			vlc_media = IntPtr.Zero;
		}
		// Clear vlc session
		if (vlc_session != IntPtr.Zero) {
			LibVLC.libvlc_release(vlc_session);
			vlc_session = IntPtr.Zero;
		}
		// Free frame buffer
		if (vlc_video_buffer != IntPtr.Zero) {
			Marshal.FreeHGlobal(vlc_video_buffer);
			vlc_video_buffer = IntPtr.Zero;
		}
	}
}
