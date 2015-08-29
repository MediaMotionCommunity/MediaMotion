using System;
using MediaMotion.Modules.PDFViewer.Models;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models {
	/// <summary>
	/// PDF Document
	/// </summary>
	public class Document : IDocument {
		/// <summary>
		/// Initializes a new instance of the <see cref="Document" /> class.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="element">The element.</param>
		/// <exception cref="System.Exception">Could not load document  + this.Element.GetName()</exception>
		public Document(IntPtr session, IPDF element) {
			this.Session = session;
			this.Element = element;
			this.Resource = LibMuPDF.libpdf_load_document(this.Session, this.Element.GetPath());

			if (this.Resource == IntPtr.Zero) {
				throw new Exception("Could not load document " + this.Element.GetName());
			}
			this.Count = LibMuPDF.libpdf_count_pages(this.Session, this.Resource);
		}

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session { get; private set; }

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		public IPDF Element { get; private set; }

		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <value>
		/// The document.
		/// </value>
		public IntPtr Resource { get; private set; }

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <value>
		/// The pages.
		/// </value>
		public int Count { get; private set; }

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Resource != IntPtr.Zero) {
				LibMuPDF.libpdf_free_document(this.Session, this.Resource);
				this.Resource = IntPtr.Zero;
			}
		}

		/// <summary>
		/// Gets the pages.
		/// </summary>
		/// <returns>The pages</returns>
		public IPage[] GetPages() {
			IPage[] pages = new IPage[this.Count];

			for (int i = 0; i < this.Count; ++i) {
				pages[i] = new Page(this, i);
			}
			return (pages);
		}

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <param name="pageNumber">The page number.</param>
		/// <returns>The page</returns>
		public IPage GetPage(int pageNumber) {
			if (this.Resource != IntPtr.Zero && pageNumber > 0 && pageNumber <= this.Count) {
				return (new Page(this, pageNumber));
			}
			return (default(IPage));
		}
	}
}
