using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Tile interface
	/// </summary>
	public interface ISlideshowTile {
		/// <summary>
		/// Gets or sets the file.
		/// </summary>
		/// <value>
		/// The file.
		/// </value>
		IFile File { get; set; }
	}
}
