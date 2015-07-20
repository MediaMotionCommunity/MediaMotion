using System;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDF Viewer session
	/// </summary>
	public class PDFSession {

		private IntPtr pdf_session;

		public PDFSession() {
			pdf_session = LibPDF.libpdf_load_session();
		}

		~PDFSession() {
			if (ok()) {
				LibPDF.libpdf_free_session(pdf_session);
			}
		}

		public bool ok() {
			return pdf_session != IntPtr.Zero;
		}

		public bool check() {
			if (!ok()) {
				Debug.LogError("Unable to load PDF renderer");
				return false;
			}
			return true;
		}

		public IntPtr get() {
			return pdf_session;
		}

	}
}
