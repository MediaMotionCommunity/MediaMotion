using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Modules.VideoViewer.Models.Interfaces {
	/// <summary>
	/// Video Model Interface
	/// </summary>
	public interface IVideo : IFile {
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
		/// Gets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		int Duration { get; }
	}
}
