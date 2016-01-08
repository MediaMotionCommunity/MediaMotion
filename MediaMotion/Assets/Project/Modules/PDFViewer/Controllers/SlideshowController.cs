using System;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Core.Services.Playlist.Models.Abstracts;
using MediaMotion.Modules.PDFViewer.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// Slideshow Controller
	/// </summary>
	public class SlideshowController : ASlideshow<PDFViewerModule, SlideshowController, SlideshowTileController, SlideshowElementController> {
		/// <summary>
		/// The file system service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// The mupdf service
		/// </summary>
		private IMuPDFService mupdfService;

		/// <summary>
		/// The document
		/// </summary>
		private IDocument document;

		/// <summary>
		/// Initializes the specified file system service.
		/// </summary>
		/// <param name="fileSystemService">The file system service.</param>
		/// <param name="mupdfService">The mupdf service.</param>
		public void Init(IFileSystemService fileSystemService, IMuPDFService mupdfService) {
			this.fileSystemService = fileSystemService;
			this.mupdfService = mupdfService;
		}

		/// <summary>
		/// Called when [destroy].
		/// </summary>
		public override void OnDestroy() {
			this.document.Dispose();
			base.OnDestroy();
		}
		
		/// <summary>
		/// Initializes the playlist.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the playlist is correctly initialized, <c>false</c> otherwise
		/// </returns>
		protected override bool InitPlaylist() {
			IElement[] elements = this.module.Parameters;
			IElement element;

			if (this.module.Parameters == null) {
				elements = this.fileSystemService.GetFolderElements(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), this.module.SupportedExtensions);
			}
			element = elements.FirstOrDefault();
			if (element == null) {
				return (false);
			}
			this.document = this.mupdfService.GetDocument((IPDF)element);
			return (this.playlistService.Configure(this.document.GetPages()));
		}
	}
}
