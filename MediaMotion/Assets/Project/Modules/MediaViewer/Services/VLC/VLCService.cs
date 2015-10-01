using System;
using MediaMotion.Modules.MediaViewer.Services.Session.Interfaces;
using MediaMotion.Modules.MediaViewer.Services.VLC.Bindings;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Services.VLC {
	/// <summary>
	/// VLC service
	/// </summary>
	public class VLCService : IVLCService {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		private IntPtr session;

		/// <summary>
		/// Initializes a new instance of the <see cref="VLCService"/> class.
		/// </summary>
		public VLCService() {
			this.Session = IntPtr.Zero;
		}

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session {
			get {
				if (this.session == IntPtr.Zero) {
					this.session = LibVLC.libvlc_new(0, new string[] { });
				}
				return (this.session);
			}
			private set {
				this.session = value;
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Session != IntPtr.Zero) {
				LibVLC.libvlc_release(this.Session);
			}
		}

		/// <summary>
		/// Get the media for the specified video.
		/// </summary>
		/// <param name="media">The video.</param>
		/// <returns>The media.</returns>
		public IMediaInfo GetMedia(IMedia media) {
			if (this.Session != IntPtr.Zero) {
				return (new MediaInfo(this.Session, media));
			}
			UnityEngine.Debug.Log("Can't load the media because the session is not loaded.");
			return (default(IMediaInfo));
		}

		/// <summary>
		/// Gets the player for the specified video.
		/// </summary>
		/// <param name="video">The video.</param>
		/// <returns>The player.</returns>
		public IPlayer GetPlayer(IMedia video) {
			IMediaInfo media = this.GetMedia(video);

			if (media != default(IMediaInfo)) {
				return (new Player(media));
			}
			UnityEngine.Debug.Log("Can't load the player because the media is not loaded.");
			return (default(IPlayer));
		}
	}
}
