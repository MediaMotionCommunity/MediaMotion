using System;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models {
	/// <summary>
	/// Page
	/// </summary>
	public class Page : IPage {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public IntPtr Session { get; private set; }

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
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		public int Width { get; private set; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		public int Height { get; private set; }

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <value>
		/// The page.
		/// </value>
		public IntPtr Resource { get; private set; }

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>
		/// The texture.
		/// </value>
		public AutoPinner Texture { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Resource"/> class.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <param name="pageNumber">The page number.</param>
		public Page(IDocument document, int pageNumber) {
			this.Session = document.Session;
			this.Document = document;
			this.PageNumber = pageNumber;
			this.Resource = LibMuPDF.libpdf_load_page(this.Session, this.Document.Resource, this.PageNumber, 101, 101);

			if (this.Resource == IntPtr.Zero) {
				throw new Exception("Could not load page " + this.PageNumber.ToString() + " of the document " + this.Document.Element.GetName());
			}
			this.Width = LibMuPDF.libpdf_xsize_page(this.Session, this.Resource);
			this.Height = LibMuPDF.libpdf_ysize_page(this.Session, this.Resource);
		}

		/// <summary>
		/// Sets the texture.
		/// </summary>
		/// <param name="texture">The texture.</param>
		public void SetTexture(object texture) {
			if (this.Texture != null) {
				this.Texture.Dispose();
			}
			this.Texture = new AutoPinner(texture);
			LibMuPDF.libpdf_render_page(this.Session, this.Resource, 0.0f, 0.0f, 1.0f, 1.0f, 0.0f);
			LibMuPDF.memcpy(this.Texture.Ptr, LibMuPDF.libpdf_pixels_page(this.Session, this.Resource), this.Width * this.Height * 4);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			if (this.Texture != null) {
				this.Texture.Dispose();
				this.Texture = null;
			}
			if (this.Resource != IntPtr.Zero) {
				LibMuPDF.libpdf_free_page(this.Document.Session, this.Resource);
				this.Resource = IntPtr.Zero;
			}
		}
	}
}
