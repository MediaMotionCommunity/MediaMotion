using MediaMotion.Modules.MediaViewer.Models.Interfaces;

namespace MediaMotion.Modules.MediaViewer.SubModules.MusicPlayer.Models.Interfaces {
	/// <summary>
	/// Video Model Interface
	/// </summary>
	public interface IMusic : IMedia {
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
