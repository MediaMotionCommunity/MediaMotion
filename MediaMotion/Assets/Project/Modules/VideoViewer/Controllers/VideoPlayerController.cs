using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;
using System;
using System.IO;
using System.Threading;
using System.Collections;
using System.Runtime.InteropServices;
using VLC;

namespace MediaMotion.Modules.VideoViewer.Controllers {

	/// <summary>
	/// VideoPlayer Controller
	/// </summary>
	public class VideoPlayerController : BaseUnityScript<VideoPlayerController> {

		/// <summary>
		/// The module instance
		/// </summary>
		private VideoViewerModule moduleInstance;
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;
		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		// VLC Player internal components
		private IntPtr vlc_session = IntPtr.Zero;
		private IntPtr vlc_media = IntPtr.Zero;
		private IntPtr vlc_player = IntPtr.Zero;
		private IntPtr vlc_video_buffer = IntPtr.Zero;
		private IntPtr vlc_video_result_buffer = IntPtr.Zero;
		// VLC Video components
		private Mutex vlc_video_lock = new Mutex();
		private Color32[] vlc_video_pixels;
		private GCHandle vlc_video_pixels_handle;
		private Texture2D vlc_video = null;
		private uint vlc_video_xsize = 0;
		private uint vlc_video_ysize = 0;
		private uint vlc_video_pitches = 0;
		private uint vlc_video_lines = 0;

		/// <summary>
		/// VLC callback : Called when video frame is gonna be decoded
		/// </summary>
		static public IntPtr VideoLock(IntPtr opaque, ref IntPtr planes) {
			VideoPlayerController instance = (((GCHandle)opaque).Target as VideoPlayerController);
			// Block and allocate buffer, or use already allocated buffer
			instance.vlc_video_lock.WaitOne();
			if (instance.vlc_video_buffer == IntPtr.Zero) {
				instance.vlc_video_buffer = Marshal.AllocHGlobal((Int32)(instance.vlc_video_pitches * instance.vlc_video_lines));
			}
			planes = instance.vlc_video_buffer;
			return instance.vlc_video_buffer;
		}

		/// <summary>
		/// VLC callback : Called when video frame is decoded
		/// </summary>
		static public void VideoUnlock(IntPtr opaque, IntPtr picture, ref IntPtr planes) {
			VideoPlayerController instance = (((GCHandle)opaque).Target as VideoPlayerController);
			// Unlock buffer
			instance.vlc_video_lock.ReleaseMutex();
		}

		/// <summary>
		/// VLC callback : Called when video frame is ready to render
		/// </summary>
		static public void VideoDisplay(IntPtr opaque, IntPtr picture) {
			VideoPlayerController instance = (((GCHandle)opaque).Target as VideoPlayerController);
			// Save the frame buffer pointer
			instance.vlc_video_result_buffer = picture;
			IntPtr pixels = instance.vlc_video_pixels_handle.AddrOfPinnedObject();
			// Copy the picture content to texture pixels :)
			if (pixels != IntPtr.Zero && picture != IntPtr.Zero) {
				// Use memcpy as the destination texture and the vlc buffer are configured with the same memory pattern
				LibVLC.memcpy(pixels, picture, (int)(instance.vlc_video_xsize * instance.vlc_video_ysize * 4));
			}
		}

		/// <summary>
		/// VLC callback : Configure video frame format
		/// </summary>
		static public uint VideoFormat(ref IntPtr opaque, ref uint chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines) {
			VideoPlayerController instance = (((GCHandle)opaque).Target as VideoPlayerController);
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

		/// <summary>
		/// VLC callback : Ends video frame formating
		/// </summary>
		static public void VideoUnformat(IntPtr opaque) {
			VideoPlayerController instance = (((GCHandle)opaque).Target as VideoPlayerController);
			return;
		}

		/// <summary>
		/// Initializes the module.
		/// </summary>
		public void Init(VideoViewerModule module, IInputService input, IPlaylistService playlist) {
			// Configure module components
			this.moduleInstance = module;
			this.inputService = input;
			this.playlistService = playlist;
			this.playlistService.Configure(((this.moduleInstance.Parameters.Length > 0) ? (this.moduleInstance.Parameters[0]) : (null)), new string[] { ".mkv", ".avi", ".wav", ".mp4" });
			// Start VLC playing
			this.Start();
			this.LoadFile();
			this.Play();
		}

		/// <summary>
		/// Update the unity instance
		/// </summary>
		public void Update()
		{
			// If video buffer ready apply copied texture on mesh
			if (vlc_video_result_buffer != IntPtr.Zero) {
				vlc_video.SetPixels32(vlc_video_pixels, 0);
				vlc_video.Apply();
			}
			// Read events
			/*
			foreach (IAction action in this.inputService.GetMovements()) {
				if (action.Type == ActionType.Right) {
					this.playlistService.Next();
					this.gameObject.transform.Rotate(new Vector3(0, 90, 0));
					this.LoadFile();
				} else if (action.Type == ActionType.Left) {
					this.playlistService.Previous();
					this.gameObject.transform.Rotate(new Vector3(0, -90, 0));
					this.LoadFile();
				}
			}
			*/
		}

		/// <summary>
		/// Destroy the unity instance
		/// </summary>
		public void OnDestroy()
		{
			// Clear everything
			ClearTexture();
			ClearPlayer();
			ClearMedia();
			ClearSession();
			ClearBuffer();
		}

		/// <summary>
		/// Start the VLC instance
		/// </summary>
		private void Start()
		{
			// Start the VLC runtime
			vlc_session = LibVLC.libvlc_new(0, new string[] {});
		}

		/// <summary>
		/// Load the current media file
		/// </summary>
		private void LoadFile()
		{
			// Get loaded file
			string path = this.playlistService.Current().GetPath();
			// Clear previous video
			ClearTexture();
			ClearPlayer();
			ClearMedia();
			ClearBuffer();
			// If vlc ready
			if (CheckSession()) {
				// Load media
				vlc_media = LibVLC.libvlc_media_new_path(vlc_session, path);
				// If media loaded
				if (CheckMedia()) {
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
					if (CheckPlayer() && vlc_video_xsize > 0 && vlc_video_ysize > 0) {
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
							VideoPlayerController.VideoFormat,
							VideoPlayerController.VideoUnformat,
							(IntPtr)GCHandle.Alloc(this)
						);
						// Set video buffer rendering
						LibVLC.libvlc_video_set_callbacks(
							vlc_player,
							VideoPlayerController.VideoLock,
							VideoPlayerController.VideoUnlock,
							VideoPlayerController.VideoDisplay,
							(IntPtr)GCHandle.Alloc(this)
						);
					}
				}
			}
		}

		/// <summary>
		/// Play the loaded media file
		/// </summary>
		private void Play()
		{
			// If vlc player is ready
			if (CheckPlayer()) {
				// Call play on vlc media
				LibVLC.libvlc_media_player_play(vlc_player);
			}
		}

		/// <summary>
		/// Pause the loaded media file
		/// </summary>
		private void Pause()
		{
			// If vlc player is ready
			if (CheckPlayer()) {
				// Call pause on vlc media
				LibVLC.libvlc_media_player_pause(vlc_player);
			}
		}

		/// <summary>
		/// Stop the loaded media file
		/// </summary>
		private void Stop()
		{
			// If vlc player is ready
			if (CheckPlayer()) {
				// Call stop on vlc media
				LibVLC.libvlc_media_player_stop(vlc_player);
			}
		}

		/// <summary>
		/// Clear internal texture
		/// </summary>
		private void ClearTexture()
		{
			// Clear video infos
			vlc_video_xsize = 0;
			vlc_video_ysize = 0;
			if (vlc_video != null) {
				UnityEngine.Object.Destroy(vlc_video);
				vlc_video = null;
			}
		}

		/// <summary>
		/// Clear internal buffer
		/// </summary>
		private void ClearBuffer()
		{
			// Free frame buffer
			if (vlc_video_buffer != IntPtr.Zero) {
				Marshal.FreeHGlobal(vlc_video_buffer);
				vlc_video_buffer = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Clear internal VLC session
		/// </summary>
		private void ClearSession()
		{
			// Clear vlc session
			if (vlc_session != IntPtr.Zero) {
				LibVLC.libvlc_release(vlc_session);
				vlc_session = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Clear internal VLC player
		/// </summary>
		private void ClearPlayer()
		{
			// Clear vlc player
			if (vlc_player != IntPtr.Zero) {
				LibVLC.libvlc_media_player_release(vlc_player);
				vlc_player = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Clear internal VLC media
		/// </summary>
		private void ClearMedia()
		{
			// Clear vlc media
			if (vlc_media != IntPtr.Zero) {
				LibVLC.libvlc_media_release(vlc_media);
				vlc_media = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Check for correct VLC session
		/// </summary>
		private bool CheckSession()
		{
			if (vlc_session != IntPtr.Zero) {
				return true;
			}
			Error("VLC runtime could not be loaded");
			return false;
		}

		/// <summary>
		/// Check for correct VLC media
		/// </summary>
		private bool CheckMedia()
		{
			if (vlc_media != IntPtr.Zero) {
				return true;
			}
			Error("Could not load VLC media file");
			return false;
		}

		/// <summary>
		/// Check for correct VLC player
		/// </summary>
		private bool CheckPlayer()
		{
			if (vlc_player != IntPtr.Zero) {
				return true;
			}
			Error("VLC Media player is not correctly initialized.");
			return false;
		}

		/// <summary>
		/// Print an internal error
		/// </summary>
		private void Error(string msg) {
			// Display an error
			Debug.LogError(msg);
		}
	}
}
