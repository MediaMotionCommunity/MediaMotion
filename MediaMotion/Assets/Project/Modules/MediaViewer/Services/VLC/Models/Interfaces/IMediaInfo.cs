using System;
using MediaMotion.Modules.MediaViewer.Models.Interfaces;
using MediaMotion.Modules.MediaViewer.SubModules.VideoPlayer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Services.VLC.Models.Interfaces {
	/// <summary>
	/// VLC media interface
	/// </summary>
	public interface IMediaInfo : IDisposable {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		IntPtr Session { get; }

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		IMedia Element { get; }

		/// <summary>
		/// Gets the resource.
		/// </summary>
		/// <value>
		/// The resource.
		/// </value>
		IntPtr Resource { get; }

		/// <summary>
		/// Gets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		int Duration { get; }

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		int Width { get; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		int Height { get; }

		/// <summary>
		/// Gets the channel.
		/// </summary>
		/// <value>
		/// The channel.
		/// </value>
		int Channel { get; }

		/// <summary>
		/// Gets the rate.
		/// </summary>
		/// <value>
		/// The rate.
		/// </value>
		int Rate { get; }
	}
}
