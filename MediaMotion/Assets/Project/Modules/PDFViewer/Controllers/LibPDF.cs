using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace PDF
{
    static class LibPDF
    {
        // Session
        [DllImport("libmupdf")]
        public static extern IntPtr libpdf_load_session();
        [DllImport("libmupdf")]
        public static extern void libpdf_free_session(IntPtr session);

        // Document
        [DllImport("libmupdf")]
        public static extern IntPtr libpdf_load_document(IntPtr session, [MarshalAs(UnmanagedType.LPStr)] string filename);
        [DllImport("libmupdf")]
        public static extern void libpdf_free_document(IntPtr session, IntPtr document);

        // Frame
        [DllImport("libmupdf")]
        public static extern IntPtr libpdf_load_page(IntPtr session, IntPtr document, int pagenum);
        [DllImport("libmupdf")]
        public static extern void libpdf_free_page(IntPtr session, IntPtr page);

        // Utilities
        [DllImport("libmupdf")]
        public static extern int libpdf_count_pages(IntPtr session, IntPtr document);

        [DllImport("libmupdf")]
        public static extern int libpdf_xsize_page(IntPtr session, IntPtr page);
        [DllImport("libmupdf")]
        public static extern int libpdf_ysize_page(IntPtr session, IntPtr page);
        [DllImport("libmupdf")]
        public static extern IntPtr libpdf_pixels_page(IntPtr session, IntPtr page);
        [DllImport("libmupdf")]
        public static extern void libpdf_render_page(IntPtr session, IntPtr page);

        // Importing system memcpy from C
        [DllImport("msvcrt")]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
    }
}
