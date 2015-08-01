using System;
using System.Runtime.InteropServices;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings {
	/// <summary>
	/// LibPDF CSharp binding
	/// </summary>
	public static class LibMuPDF {
		/// <summary>
		/// Load a LibPDF's session.
		/// </summary>
		/// <returns>The session.</returns>
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_session();

		/// <summary>
		/// Dispose a LibPDF's session.
		/// </summary>
		/// <param name="session">The session.</param>
		[DllImport("libpdf")]
		public static extern void libpdf_free_session(IntPtr session);

		/// <summary>
		/// Load a PDF document.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="filename">The filename.</param>
		/// <returns>The document.</returns>
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_document(IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string filename);

		/// <summary>
		/// Dispose a PDF document.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="document">The document.</param>
		[DllImport("libpdf")]
		public static extern void libpdf_free_document(IntPtr session, IntPtr document);

		/// <summary>
		/// Load a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="document">The document.</param>
		/// <param name="pagenum">The page number.</param>
		/// <param name="xdpi">The absis resolution.</param>
		/// <param name="ydpi">The ordonate resolution.</param>
		/// <returns>The page.</returns>
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_page(IntPtr session, IntPtr document, int pagenum, float xdpi, float ydpi);

		/// <summary>
		/// Dispose a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		[DllImport("libpdf")]
		public static extern void libpdf_free_page(IntPtr session, IntPtr page);

		/// <summary>
		/// Count the number of PDF pages in a PDF document.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="document">The document.</param>
		/// <returns>The number of PDF pages.</returns>
		[DllImport("libpdf")]
		public static extern int libpdf_count_pages(IntPtr session, IntPtr document);

		/// <summary>
		/// Get errors on a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <returns>if no error <c>0</c>, otherwise the error.</returns>
		[DllImport("libpdf")]
		public static extern int libpdf_error_page(IntPtr session, IntPtr page);

		/// <summary>
		/// Get the absis size of a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <returns>The absis size.</returns>
		[DllImport("libpdf")]
		public static extern int libpdf_xsize_page(IntPtr session, IntPtr page);

		/// <summary>
		/// Get the ordonate size of a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <returns>The ordonate size.</returns>
		[DllImport("libpdf")]
		public static extern int libpdf_ysize_page(IntPtr session, IntPtr page);

		/// <summary>
		/// Libpdf_pixels_pages the specified session.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <returns>The pixels.</returns>
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_pixels_page(IntPtr session, IntPtr page);

		/// <summary>
		/// Libpdf_render_pages the specified session.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <param name="ox">The ox.</param>
		/// <param name="oy">The oy.</param>
		/// <param name="xscale">The absis scale.</param>
		/// <param name="yscale">The ordonate scale.</param>
		/// <param name="rotation">The rotation.</param>
		[DllImport("libpdf")]
		public static extern void libpdf_render_page(IntPtr session, IntPtr page, float ox, float oy, float xscale, float yscale, float rotation);

		/// <summary>
		/// Binary copy of <see cref="count" /> bytes from <see cref="source" /> to <see cref="destination" />.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <param name="source">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns>
		/// The <see cref="destination" />.
		/// </returns>
		[DllImport("msvcrt")]
		public static extern IntPtr memcpy(IntPtr destination, IntPtr source, int count);
	}
}
