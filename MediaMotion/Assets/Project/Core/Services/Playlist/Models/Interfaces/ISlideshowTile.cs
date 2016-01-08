using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.Playlist.Models.Interfaces {
	/// <summary>
	/// Slideshow Tile interface
	/// </summary>
	public interface ISlideshowTile : ISlideshowEnvironment {
		/// <summary>
		/// Loads the element.
		/// </summary>
		/// <param name="element">The element.</param>
		void Load(object element);

		/// <summary>
		/// Zooms the file
		/// </summary>
		/// <param name="multiplier">The multiplier.</param>
		void Zoom(float multiplier);

		/// <summary>
		/// Clears the zoom.
		/// </summary>
		void ClearZoom();

		/// <summary>
		/// Rotate the tile
		/// </summary>
		/// <param name="angle">The angle.</param>
		void Rotate(float angle);

		/// <summary>
		/// Clears the rotation.
		/// </summary>
		void ClearRotation();
	}
}
