using System;
using MediaMotion.Modules.PDFViewer.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF {
	/// <summary>
	/// MuPDF service
	/// </summary>
	public class MuPDFService : IMuPDFService {
	/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		private IntPtr session;

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session {
			get {
				if (this.session == IntPtr.Zero) {
					this.session = LibMuPDF.libpdf_load_session();
				}
				return (this.session);
			}
			private set {
				this.session = value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PlayerManagerService"/> class.
		/// </summary>
		public MuPDFService() {
			this.Session = IntPtr.Zero;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Session != IntPtr.Zero) {
				LibMuPDF.libpdf_free_session(this.Session);
			}
		}

		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <param name="pdf">The PDF.</param>
		/// <returns>The document.</returns>
		public IDocument GetDocument(IPDF pdf) {
			if (this.Session != IntPtr.Zero) {
				return (new Document(this.Session, pdf));
			}
			return (default(IDocument));
		}
	}
}
