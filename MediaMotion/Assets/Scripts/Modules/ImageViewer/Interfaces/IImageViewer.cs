using MediaMotion.Modules.Components.Playlist;
using MediaMotion.Modules.Components.Rotate;
using MediaMotion.Modules.Components.Zoom;

namespace MediaMotion.Modules.ImageViewer.Interfaces {
	/// <summary>
	/// Image Viewer Interface
	/// </summary>
	public interface IImageViewer : IZoom, IRotate, IPlaylist {
	}
}
