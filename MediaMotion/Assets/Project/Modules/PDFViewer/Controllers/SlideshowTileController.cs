using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// Slideshow tile controller
	/// </summary>
	public class SlideshowTileController : ASlideshowTile<PDFViewerModule, SlideshowTileController> {
		/// <summary>
		/// The mupdf service
		/// </summary>
		private IMuPDFService mupdfService;

		/// <summary>
		/// Initializes the specified mupdf service.
		/// </summary>
		/// <param name="mupdfService">The mupdf service.</param>
		public void Init(IMuPDFService mupdfService) {
			this.mupdfService = mupdfService;
			this.oppositeYScale = true;
		}

		/// <summary>
		/// Apply the texture 2D.
		/// </summary>
		protected override void ApplyTexture2D() {
			if (this.texture2D != null && this.gameObject.GetComponent<Renderer>() != null) {
				IPage page = (IPage)this.element;

				this.texture2D.SetPixels32(page.Texture);
				this.texture2D.Apply();

				this.gameObject.GetComponent<Renderer>().material.mainTexture = this.texture2D;
				this.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
				this.Texture2DApplied = true;
			}
		}

		/// <summary>
		/// Loads the texture2 d.
		/// </summary>
		protected override void LoadTexture2D() {
			IPage page = (IPage)this.element;

			this.texture2D = new Texture2D(page.Width, page.Height, TextureFormat.RGBA32, false);
			this.texture2D.wrapMode = TextureWrapMode.Clamp;
			page.Render();
		}
	}
}
