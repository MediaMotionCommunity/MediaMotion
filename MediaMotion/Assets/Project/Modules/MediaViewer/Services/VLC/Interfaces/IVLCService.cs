using System;
using MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Services.Session.Interfaces {
	/// <summary>
	/// VLC service interface
	/// </summary>
	public interface IVLCService {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		IntPtr Session { get; }

		/// <summary>
		/// Loads the specified media.
		/// </summary>
		/// <param name="video">The video.</param>
		/// <returns>The player</returns>
		IMediaInfo GetMedia(IMedia video);

		/// <summary>
		/// Gets the player.
		/// </summary>
		/// <param name="video">The video.</param>
		/// <returns>The player</returns>
		IPlayer GetPlayer(IMedia video);
	}
}
