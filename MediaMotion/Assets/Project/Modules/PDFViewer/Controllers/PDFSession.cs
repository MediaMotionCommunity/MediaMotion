using System;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDF Viewer session
	/// </summary>
	public class PDFSession : IDisposable {
		private IntPtr pdfSession;

		public PDFSession() {
			this.pdfSession = LibPDF.libpdf_load_session();
		}

		public void Dispose() {
			if (this.Ok()) {
				LibPDF.libpdf_free_session(pdfSession);
			}
		}

		public bool Ok() {
			return (this.pdfSession != IntPtr.Zero);
		}

		public bool Check() {
			if (!this.Ok()) {
				Debug.LogError("Unable to load PDF renderer");
				return (false);
			}
			return (true);
		}

		public IntPtr Get() {
			return (this.pdfSession);
		}
	}
}
