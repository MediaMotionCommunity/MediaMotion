using System;
using MediaMotion.Modules.PDFViewer.Models;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces {
	/// <summary>
	/// Document interface
	/// </summary>
	public interface IDocument : IDisposable {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		IntPtr Session { get; }

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		IPDF Element { get; }

		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <value>
		/// The document.
		/// </value>
		IntPtr Resource { get; }

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>
		/// The pages.
		/// </value>
		int Count { get; }

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <returns>The page</returns>
		IPage GetPage(int pageNumber);
	}
}
