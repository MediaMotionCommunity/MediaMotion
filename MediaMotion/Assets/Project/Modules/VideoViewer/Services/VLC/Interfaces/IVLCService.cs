using System;
using MediaMotion.Modules.VideoViewer.Models.Interfaces;
using MediaMotion.Modules.VideoViewer.Services.VLC.Models.Interfaces;

namespace MediaMotion.Modules.VideoViewer.Services.Session.Interfaces {
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
		IMedia GetMedia(IVideo video);

		/// <summary>
		/// Gets the player.
		/// </summary>
		/// <param name="video">The video.</param>
		/// <returns>The player</returns>
		IPlayer GetPlayer(IVideo video);
	}
}
