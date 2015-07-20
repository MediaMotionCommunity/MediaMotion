using UnityEngine;
using System;
using System.Runtime.InteropServices;

/**
    How to compile:

Mac OS:
    Compiled MuPDF -> $CCDIR
    PDFViewer Binding path -> $BIND
    Output DLL path -> $OUT

    mkdir $OUT
    mkdir $OUT/pdf/
    gcc -dynamiclib -o $OUT/pdf/libpdf.bundle $BIND/LibPDF.c $CCDIR/build/debug/*.a -I$CCDIR/include -lm

    Example (with $PWD == LibPDF.c directory):
        export CCDIR="/Users/vincentbrunet/Downloads/mupdf"
        export OUT="../../../../../ModulesLibraries"
        export BIND="."

Windows:
    FIX-ME

 */
namespace MediaMotion.Modules.PDFViewer.Controllers.Binding {
	/// <summary>
	/// LibPDF CSharp binding
	/// </summary>
	static class LibPDF {
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
		/// <param name="pagenum">The pagenum.</param>
		/// <param name="xdpi">The xdpi.</param>
		/// <param name="ydpi">The ydpi.</param>
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
		/// Get the ordonat size of a PDF page.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="page">The page.</param>
		/// <returns>The ordonat size.</returns>
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
		/// <param name="xscale">The xscale.</param>
		/// <param name="yscale">The yscale.</param>
		/// <param name="rotation">The rotation.</param>
		[DllImport("libpdf")]
		public static extern void libpdf_render_page(IntPtr session, IntPtr page, float ox, float oy, float xscale, float yscale, float rotation);

		/// <summary>
		/// Binary copy of <see cref="count"/> bytes from <see cref="src"/> to <see cref="dest"/>.
		/// </summary>
		/// <param name="dest">The dest.</param>
		/// <param name="src">The source.</param>
		/// <param name="count">The count.</param>
		/// <returns>The <see cref="dest"/>.</returns>
		[DllImport("msvcrt")]
		public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
	}
}
