using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Tile interface
	/// </summary>
	public interface ISlideshowTile : ISlideshowEnvironment {
		/// <summary>
		/// Loads the file.
		/// </summary>
		/// <param name="file">The file.</param>
		void LoadFile(IFile file);

		/// <summary>
		/// Rotate the tile
		/// </summary>
		/// <param name="angle">The angle.</param>
		void Rotate(float angle);
	}
}
