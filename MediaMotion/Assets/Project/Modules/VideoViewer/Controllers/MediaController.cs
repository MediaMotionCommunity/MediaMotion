using System;
using System.Threading;
using System.Runtime.InteropServices;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// VLC Media node
	/// </summary>
	public class MediaController : MonoBehaviour {

		// VLC components
		private VLCSession Session;
		private IntPtr Media = IntPtr.Zero;
		private IntPtr Player = IntPtr.Zero;

		// Rendering
		public IntPtr VideoBuffer = IntPtr.Zero;
		public IntPtr VideoResultBuffer = IntPtr.Zero;

		// Video infos
		public string Path = "";
		public uint VideoXSize = 0;
		public uint VideoYSize = 0;
		public uint VideoPitches = 0;
		public uint VideoLines = 0;
		public Mutex VideoLock = new Mutex();

		// Texture infos
		public Texture TextureBase = null;
		public Texture2D Texture = null;
		public AutoPinner TexturePixels = null;

		public void Init(VLCSession session, string path) {
			this.TextureBase = GetComponent<Renderer>().material.mainTexture;
			this.Session = session;
			this.Path = path;
			if (this.Session.Check()) {
				this.Media = LibVLC.libvlc_media_new_path(this.Session.Session, this.Path);
				if (this.Media != IntPtr.Zero) {
					LibVLC.libvlc_media_track_info_t[] tracks;
					LibVLC.libvlc_media_fetch_tracks_info(this.Media, out tracks);

					for (int i = 0; i < tracks.Length; i++) {
						if (tracks[i].i_type == LibVLC.libvlc_track_type_t.libvlc_track_video) {
							this.VideoXSize = tracks[i].video.i_width;
							this.VideoYSize = tracks[i].video.i_height;
						}
					}

					this.Player = LibVLC.libvlc_media_player_new_from_media(this.Media);
					if (this.Player != IntPtr.Zero) {
						if ((this.VideoXSize * this.VideoYSize) > 0) {
							this.Texture = new Texture2D((int)this.VideoXSize, (int)this.VideoYSize, TextureFormat.ARGB32, false);
							this.Texture.wrapMode = TextureWrapMode.Clamp;
							this.Texture.filterMode = FilterMode.Trilinear;

							this.TexturePixels = new AutoPinner(this.Texture.GetPixels32(0));
							LibVLC.libvlc_video_set_format_callbacks(this.Player, VLCCallbacks.VideoFormat, VLCCallbacks.VideoUnformat, (IntPtr)GCHandle.Alloc(this));
							LibVLC.libvlc_video_set_callbacks(this.Player, VLCCallbacks.VideoLock, VLCCallbacks.VideoUnlock, VLCCallbacks.VideoDisplay, (IntPtr)GCHandle.Alloc(this));
							if (this.GetComponent<Renderer>() && this.Ok()) {
								this.GetComponent<Renderer>().material.mainTexture = this.Texture;
								this.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
							}
							this.Play();
						}
					}
				}
			}
		}

		public void OnDestroy() {
			if (this.Player != IntPtr.Zero) {
				LibVLC.libvlc_media_player_stop(this.Player);
				LibVLC.libvlc_media_player_release(this.Player);
			}
			if (this.Media != IntPtr.Zero) {
				LibVLC.libvlc_media_release(this.Media);
			}
			this.GetComponent<Renderer>().material.mainTexture = this.TextureBase;
			if (this.Texture != null) {
				Texture2D.Destroy(this.Texture);
			}
			if (this.VideoBuffer != IntPtr.Zero) {
				Marshal.FreeHGlobal(this.VideoBuffer);
				this.VideoBuffer = IntPtr.Zero;
			}
			if (this.TexturePixels != null) {
				this.TexturePixels.Dispose();
			}
		}

		public void Update() {
			if (this.Ok()) {
				float size = 1.0f / 10.0f;

				this.transform.localScale = new Vector3(-(size / this.Ratio()), size, size);
				this.Texture.SetPixels32((Color32[])this.TexturePixels.Obj, 0);
				this.Texture.Apply();
			}
		}

		public void Play() {
			if (this.Check()) {
				LibVLC.libvlc_media_player_play(this.Player);
			}
		}

		public void Pause() {
			if (this.Check()) {
				LibVLC.libvlc_media_player_pause(this.Player);
			}
		}

		public void Stop() {
			if (this.Check()) {
				LibVLC.libvlc_media_player_stop(this.Player);
			}
		}

		public void Time(double ratio) {
			if (this.Check()) {
				Int64 length = LibVLC.libvlc_media_player_get_length(this.Player);
				Int64 time = (Int64)((double)length * ratio);
				LibVLC.libvlc_media_player_set_time(this.Player, time);
			}
		}

		public double Time() {
			if (this.Check()) {
				Int64 length = LibVLC.libvlc_media_player_get_length(this.Player);
				Int64 time = LibVLC.libvlc_media_player_get_time(this.Player);
				return ((double)length / (double)time);
			}
			return (0);
		}

		public float Ratio() {
			return ((float)this.VideoYSize / (float)this.VideoXSize);
		}

		public bool Ok() {
			return (this.Session.Ok() && this.Media != IntPtr.Zero && this.Player != IntPtr.Zero && this.TexturePixels != null && this.Texture != null);
		}

		public bool Check() {
			if (!this.Ok()) {
				Debug.LogError("Unable to load vlc media: " + Path);
				return (false);
			}
			return (true);
		}
	}
}
