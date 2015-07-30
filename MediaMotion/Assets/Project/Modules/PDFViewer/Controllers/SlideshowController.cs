using System;
using System.Linq;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Modules.PDFViewer.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// Slideshow Controller
	/// </summary>
	public class SlideshowController : ASlideshow<PDFViewerModule, SlideshowTileController> {
		/// <summary>
		/// Initializes the playlist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the playlist is correctly initialized, <c>false</c> otherwise
		/// </returns>
		protected override bool InitPlaylist() {
			// TODO: Do something better (add DI or something like that)
			IMuPDFService mupdfService = this.module.Container.Get<IMuPDFService>();
			IDocument document = mupdfService.GetDocument((IPDF)((this.module.Parameters != null) ? (this.module.Parameters.FirstOrDefault()) : (this.elementFactory.CreateFile(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/test.pdf"))));

			return (this.playlistService.Configure(document.GetPages()));
		}
	}
}
