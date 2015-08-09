using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.Models.Interfaces {
	/// <summary>
	/// Media model interface
	/// </summary>
	public interface IMedia : IFile {
		/// <summary>
		/// Gets the duration.
		/// </summary>
		/// <value>
		/// The duration.
		/// </value>
		int Duration { get; }
	}
}
