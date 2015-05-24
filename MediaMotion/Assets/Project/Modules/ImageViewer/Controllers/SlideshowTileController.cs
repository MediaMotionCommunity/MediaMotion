using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Slideshow Tile Controller
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
			if (this.file != null) {
				if (this.imageDownload == null) {
					this.imageDownload = new WWW("file:///" + this.file.GetPath());
				}
				if (this.imageDownload.isDone) {
					if (string.IsNullOrEmpty(this.imageDownload.error)) {
						this.texture2D = this.imageDownload.texture;
					} else {
						this.texture2D = Resources.Load<Texture2D>("Images");
					}
					this.imageDownload = null;
				}
			}
		}
	}
}
