using System;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Binding;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models {
	/// <summary>
	/// Page
	/// </summary>
	public class Page : IPage {
		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <value>
		/// The document.
		/// </value>
		public IDocument Document { get; private set; }

		/// <summary>
		/// Gets the page number.
		/// </summary>
		/// <value>
		/// The page number.
		/// </value>
		public int PageNumber { get; private set; }

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <value>
		/// The page.
		/// </value>
		public IntPtr Resource { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Resource"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="pageNumber">The page number.</param>
		public Page(IDocument document, int pageNumber) {
			this.Document = document;
			this.PageNumber = pageNumber;
			this.Resource = LibMuPDF.libpdf_load_page(this.Document.Session, this.Document.Resource, this.PageNumber, 101, 101);

			if (this.Resource == IntPtr.Zero) {
				throw new Exception("Could not load page " + this.PageNumber.ToString() + " of the document " + this.Document.Element.GetName());
			}
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		/// <exception cref="System.NotImplementedException"></exception>
		public void Dispose() {
			if (this.Resource != IntPtr.Zero) {
				LibMuPDF.libpdf_free_page(this.Document.Session, this.Resource);
				this.Resource = IntPtr.Zero;
			}
		}
	}
}
