using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Tile interface
	/// </summary>
	public interface ISlideshowTile {
		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <param name="file">The file.</param>
		void LoadFile(IFile file);
	}
}
