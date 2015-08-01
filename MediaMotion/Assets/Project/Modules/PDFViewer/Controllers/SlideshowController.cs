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
	public class SlideshowController : ASlideshow<PDFViewerModule, SlideshowController, SlideshowTileController> {
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

		/// <summary>
		/// Computes the local scale using the <see cref="offset" />.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The local scale
		/// </returns>
		protected override Vector3 ComputeLocalScale(int offset) {
			return (new Vector3(0.38f, 0.38f, 0.38f));
		}

		/// <summary>
		/// Computes the local position using the <see cref="offset" />.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The local position
		/// </returns>
		protected override Vector3 ComputeLocalPosition(int offset) {
			int index = this.playlistService.Index + offset;
			int diff = index % 2;

			return (new Vector3(((offset >= diff) ? (1) : (-1)) * 1.325f, 2.2f, (((offset + diff) > 1 || (offset + diff) < 0)) ? (1.0f) : (0.0f)));
		}

		/// <summary>
		/// Computes the local rotation using the <see cref="offset" />.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The local rotation
		/// </returns>
		protected override Quaternion ComputeLocalRotation(int offset) {
			int index = this.playlistService.Index + offset;

			if (offset > 1) {
				return (Quaternion.Euler(0.0f, (index % 2 == 0) ? (1.0f) : (179.0f), 0.0f));
			}
			if (offset < -1) {
				return (Quaternion.Euler(0.0f, (index % 2 == 0) ? (179.0f) : (1.0f), 0.0f));
			}
			if (offset == -1 && index % 2 == 0) {
				return (Quaternion.Euler(0.0f, 179.0f, 0.0f));
			}
			return (Quaternion.Euler(0.0f, 1.0f, 0.0f));
		}
	}
}
