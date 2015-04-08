using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Modules.ImageViewer.Models.Interfaces {
	/// <summary>
	/// Image model interface
	/// </summary>
	public interface IImage : IFile {
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
	}
}
