using System;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDF Viewer session
	/// </summary>
	public class PDFSession : IDisposable {
		/// <summary>
		/// The PDF session
		/// </summary>
		private IntPtr pdfSession;

		/// <summary>
		/// Initializes a new instance of the <see cref="PDFSession"/> class.
		/// </summary>
		public PDFSession() {
			this.pdfSession = LibMuPDF.libpdf_load_session();
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Check()) {
				LibMuPDF.libpdf_free_session(this.pdfSession);
			}
		}

		/// <summary>
		/// Checks this instance.
		/// </summary>
		/// <returns></returns>
		public bool Check() {
			return (this.pdfSession != IntPtr.Zero);
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <returns></returns>
		public IntPtr Get() {
			return (this.pdfSession);
		}
	}
}
