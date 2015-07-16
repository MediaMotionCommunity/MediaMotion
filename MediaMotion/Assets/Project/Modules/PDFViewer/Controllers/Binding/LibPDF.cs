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
namespace MediaMotion.Modules.PDFViewer.Controllers.Binding
{
    static class LibPDF
    {
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
        public static extern IntPtr libpdf_load_page(IntPtr session, IntPtr document, int pagenum, float xdpi, float ydpi);
        [DllImport("libpdf")]
        public static extern void libpdf_free_page(IntPtr session, IntPtr page);

        // Utilities
        [DllImport("libpdf")]
        public static extern int libpdf_count_pages(IntPtr session, IntPtr document);

        [DllImport("libpdf")]
        public static extern int libpdf_error_page(IntPtr session, IntPtr page);
        [DllImport("libpdf")]
        public static extern int libpdf_xsize_page(IntPtr session, IntPtr page);
        [DllImport("libpdf")]
        public static extern int libpdf_ysize_page(IntPtr session, IntPtr page);
        [DllImport("libpdf")]
        public static extern IntPtr libpdf_pixels_page(IntPtr session, IntPtr page);
        [DllImport("libpdf")]
        public static extern void libpdf_render_page(
            IntPtr session, IntPtr page,
            float ox, float oy,
            float xscale, float yscale,
            float rotation
        );

        // Importing system memcpy from C
        [DllImport("msvcrt")]
        public static extern IntPtr memcpy(IntPtr dest, IntPtr src, int count);
    }
}
