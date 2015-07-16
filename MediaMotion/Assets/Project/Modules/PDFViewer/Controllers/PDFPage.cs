using System;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers
{
    /// <summary>
    /// PDFViewer page
    /// </summary>
    public class PDFPage : MonoBehaviour {

        // PDF components
        private PDFSession  pdf_session;
        private PDFDocument pdf_document;

        // Page infos
        private IntPtr pdf_page = IntPtr.Zero;
        private int pdf_pagenum;
        private int pdf_texture_xsize;
        private int pdf_texture_ysize;

        // Texture infos
        private Texture    pdf_texture_base = null;
        private Texture2D  pdf_texture = null;
        private AutoPinner pdf_texture_pixels = null;

        // Constructor
        public void Init(PDFSession session, PDFDocument document, int pagenum) {
            // Initial state
            pdf_texture_base = GetComponent<Renderer>().material.mainTexture;
            // External components
            float pdf_dpi = 101;
            pdf_session = session;
            pdf_document = document;
            pdf_pagenum = pagenum;
            // If all is good
            if (pdf_session.check() && pdf_document.check()) {
                // Load page
                pdf_page = LibPDF.libpdf_load_page(
                    pdf_session.get(),
                    pdf_document.get(),
                    pagenum, pdf_dpi, pdf_dpi
                );
                // If page loading success
                if (pdf_page != IntPtr.Zero) {
                    // Create rendering texture
                    pdf_texture_xsize = LibPDF.libpdf_xsize_page(pdf_session.get(), pdf_page);
                    pdf_texture_ysize = LibPDF.libpdf_ysize_page(pdf_session.get(), pdf_page);
                    pdf_texture_pixels = new AutoPinner(
                        new Color32[pdf_texture_xsize * pdf_texture_ysize]
                    );
                    pdf_texture = new Texture2D(
                        pdf_texture_xsize,
                        pdf_texture_ysize,
                        TextureFormat.RGBA32, false
                    );
                    // First render
                    render();
                    // Scale the model to match the pdf ratio
                    float size = 1.0f / 10.0f;
                    transform.localScale = new Vector3(size, size, -ratio() * size);
                    // Set mesh texture to pdf
                    if (GetComponent<Renderer>() && ok()) {
                        GetComponent<Renderer>().material.mainTexture = pdf_texture;
                    }
                }
            }
        }

        public void Delete() {
            // Unload page
            if (pdf_page != IntPtr.Zero) {
                LibPDF.libpdf_free_page(pdf_session.get(), pdf_page);
            }
            // Unload render texture
            GetComponent<Renderer>().material.mainTexture = pdf_texture_base;
            if (pdf_texture != null) {
                Destroy(pdf_texture);
            }
        }

        public float ratio() {
            return (float)pdf_texture_ysize / (float)pdf_texture_xsize;
        }

        public void render() {
            // If page loaded
            if (ok()) {
                // Render page into internal page buffer
                LibPDF.libpdf_render_page(
                    pdf_session.get(), pdf_page,
                    0, 0, // Offset
                    1, 1, // Scale
                    0     // Rotation
                );
                // Copy internal page buffer into pixel buffer
                if (pdf_texture_pixels != null) {
                    LibPDF.memcpy(
                        pdf_texture_pixels.Ptr(),
                        LibPDF.libpdf_pixels_page(pdf_session.get(), pdf_page),
                        pdf_texture_xsize * pdf_texture_ysize * 4
                    );
                }
                // Copy pixel buffer to texture
                if (pdf_texture != null) {
                    pdf_texture.SetPixels32((Color32[])pdf_texture_pixels.Obj());
                    pdf_texture.Apply();
                }
            }
        }

        public Texture2D texture() {
            return pdf_texture;
        }

        public int error() {
            // If dependencies are ok
            if (pdf_session.ok() && pdf_document.ok() && pdf_page != IntPtr.Zero) {
                // Get potential frame error code
                return LibPDF.libpdf_error_page(pdf_session.get(), pdf_page);
            }
            // Invalid load
            return -1;
        }

        public bool ok() {
            // If page loaded correctly
            if (error() == 0) {
                // If texture loaded correctly
                if (pdf_texture_pixels != null && pdf_texture != null) {
                    return true;
                }
            }
            return false;
        }

        public bool check() {
            if (!ok()) {
                Debug.LogError("Unable to load PDF page: " + pdf_pagenum + " (error " + error().ToString() + ")");
                return false;
            }
            return true;
        }

        public IntPtr get() {
            return pdf_page;
        }
    }
}
