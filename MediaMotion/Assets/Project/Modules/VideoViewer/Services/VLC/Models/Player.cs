using System;
using System.Runtime.InteropServices;
using System.Threading;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.VideoViewer.Services.VLC.Binding;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces;

namespace MediaMotion.Modules.VideoViewer.Services.VLC.Models {
	/// <summary>
	/// VLC player
	/// </summary>
	public class Player : IPlayer {
		/// <summary>
		/// The buffer
		/// </summary>
		private IntPtr buffer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Resource"/> class.
		/// </summary>
		/// <param name="media">The media.</param>
		public Player(IMedia media) {
			this.Media = media;
			this.Lock = new Mutex();
			this.Buffer = IntPtr.Zero;
			this.Texture = null;
			this.Resource = LibVLC.libvlc_media_player_new_from_media(this.Media.Resource);

			if (this.Resource == IntPtr.Zero) {
				throw new Exception("Could not load Player for " + this.Media.Element.GetName());
			}
			LibVLC.libvlc_video_set_format_callbacks(this.Resource, PlayerCallbacks.VideoFormat, PlayerCallbacks.VideoUnformat, (IntPtr)GCHandle.Alloc(this));
			LibVLC.libvlc_video_set_callbacks(this.Resource, PlayerCallbacks.VideoLock, PlayerCallbacks.VideoUnlock, PlayerCallbacks.VideoDisplay, (IntPtr)GCHandle.Alloc(this));
		}

		/// <summary>
		/// Gets the media.
		/// </summary>
		/// <value>
		/// The media.
		/// </value>
		public IMedia Media { get; private set; }

		/// <summary>
		/// Gets or sets the pitches.
		/// </summary>
		/// <value>
		/// The pitches.
		/// </value>
		public int Pitches { get; set; }

		/// <summary>
		/// Gets or sets the lines.
		/// </summary>
		/// <value>
		/// The lines.
		/// </value>
		public int Lines { get; set; }

		/// <summary>
		/// Gets the lock.
		/// </summary>
		/// <value>
		/// The lock.
		/// </value>
		public Mutex Lock { get; private set; }

		/// <summary>
		/// Gets the buffer.
		/// </summary>
		/// <value>
		/// The buffer.
		/// </value>
		public IntPtr Buffer {
			get {
				if (this.buffer == IntPtr.Zero) {
					this.buffer = Marshal.AllocHGlobal((int)(this.Pitches * this.Lines));
				}
				return (this.buffer);
			}
			private set {
				this.buffer = value;
			}
		}

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>
		/// The texture.
		/// </value>
		public AutoPinner Texture { get; private set; }

		/// <summary>
		/// Gets the player.
		/// </summary>
		/// <value>
		/// The player.
		/// </value>
		public IntPtr Resource { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is playing.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
		/// </value>
		public bool IsPlaying {
			get {
				return (this.Resource != IntPtr.Zero && LibVLC.libvlc_media_player_is_playing(this.Resource) != 0);
			}
			set {
				if (value != this.IsPlaying) {
					switch (value) {
						case true:
							this.Play();
							break;
						case false:
							this.Pause();
							break;
					}
				}
			}
		}

		/// <summary>
		/// Gets or sets the time.
		/// </summary>
		/// <value>
		/// The time.
		/// </value>
		public int Time {
			get {
				return ((int)(LibVLC.libvlc_media_player_get_time(this.Resource) / 1000));
			}
			set {
				LibVLC.libvlc_media_player_set_time(this.Resource, (long)value);
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Resource != IntPtr.Zero) {
				LibVLC.libvlc_media_player_stop(this.Resource);
				LibVLC.libvlc_media_player_release(this.Resource);
			}
			if (this.Texture != null) {
				this.Texture.Dispose();
				this.Texture = null;
			}
			if (this.Media != null) {
				this.Media.Dispose();
				this.Media = null;
			}
		}

		/// <summary>
		/// Sets the texture.
		/// </summary>
		/// <param name="texture">The texture.</param>
		public void SetTexture(object texture) {
			this.Texture = new AutoPinner(texture);
		}

		/// <summary>
		/// Plays this instance.
		/// </summary>
		public void Play() {
			if (this.Resource != IntPtr.Zero) {
				LibVLC.libvlc_media_player_play(this.Resource);
			}
		}

		/// <summary>
		/// Pauses this instance.
		/// </summary>
		public void Pause() {
			if (this.Resource != IntPtr.Zero) {
				LibVLC.libvlc_media_player_pause(this.Resource);
			}
		}

		/// <summary>
		/// Stops this instance.
		/// </summary>
		public void Stop() {
			if (this.Resource != IntPtr.Zero) {
				LibVLC.libvlc_media_player_stop(this.Resource);
			}
		}
	}
}
