using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace MuPDF {
	static class LibPDF {
		// Session
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_session();
		[DllImport("libpdf")]
		public static extern void libpdf_free_session(IntPtr session);

		// Document
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_document(IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string filename);
		[DllImport("libpdf")]
		public static extern void libpdf_free_document(IntPtr session, IntPtr document);

		// Frame
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_load_page(IntPtr session, IntPtr document, int pagenum);
		[DllImport("libpdf")]
		public static extern void libpdf_free_page(IntPtr session, IntPtr page);

		// Utilities
		[DllImport("libpdf")]
		public static extern int libpdf_count_pages(IntPtr session, IntPtr document);

		[DllImport("libpdf")]
		public static extern int libpdf_xsize_page(IntPtr session, IntPtr page);
		[DllImport("libpdf")]
		public static extern int libpdf_ysize_page(IntPtr session, IntPtr page);
		[DllImport("libpdf")]
		public static extern IntPtr libpdf_pixels_page(IntPtr session, IntPtr page);
		[DllImport("libpdf")]
		public static extern void libpdf_render_page(IntPtr session, IntPtr page);

		// Importing system memcpy from C
		[DllImport("msvcrt")]
		public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
	}
}
