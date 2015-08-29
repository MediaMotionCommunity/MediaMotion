using System;
using MediaMotion.Modules.VideoViewer.Models.Interfaces;
using MediaMotion.Modules.VideoViewer.Services.Session.Interfaces;
using MediaMotion.Modules.VideoViewer.Services.VLC.Bindings;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces;

namespace MediaMotion.Modules.VideoViewer.Services.VLC {
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
		/// <param name="video">The video.</param>
		/// <returns>The media.</returns>
		public IMedia GetMedia(IVideo video) {
			if (this.Session != IntPtr.Zero) {
				return (new Media(this.Session, video));
			}
			return (default(IMedia));
		}

		/// <summary>
		/// Gets the player for the specified video.
		/// </summary>
		/// <param name="video">The video.</param>
		/// <returns>The player.</returns>
		public IPlayer GetPlayer(IVideo video) {
			IMedia media = this.GetMedia(video);

			if (media != default(IMedia)) {
				return (new Player(media));
			}
			return (default(IPlayer));
		}
	}
}
