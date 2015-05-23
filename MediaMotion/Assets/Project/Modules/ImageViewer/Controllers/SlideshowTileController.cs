using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// SlideshowTileController
	/// </summary>
	public class SlideshowTileController : ASlideshowTile<ImageViewerModule, SlideshowTileController> {
		/// <summary>
		/// The image download
		/// </summary>
		private WWW imageDownload;

		/// <summary>
		/// Loads the texture 2D.
		/// </summary>
		protected override void LoadTexture2D() {
			if (this.File != null) {
				if (this.imageDownload == null) {
					this.imageDownload = new WWW("file:///" + this.File.GetPath());
				}
				if (this.imageDownload.isDone) {
					this.texture2D = this.imageDownload.texture;
					this.imageDownload = null;
				}
			}
		}
	}
}
