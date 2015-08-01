using System;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Modules.PDFViewer.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Interfaces {
	/// <summary>
	/// MuPDF service interface
	/// </summary>
	public interface IMuPDFService : IDisposable, IResetable {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		IntPtr Session { get; }

		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <param name="pdf">The PDF.</param>
		/// <returns>The document.</returns>
		IDocument GetDocument(IPDF pdf);
	}
}
