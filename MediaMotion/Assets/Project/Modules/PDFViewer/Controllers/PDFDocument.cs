using System;
using System.Collections.Generic;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers
{
    /// <summary>
    /// PDFViewer Document
    /// </summary>
    public class PDFDocument : MonoBehaviour {

        private PDFSession       pdf_session;
        private List<GameObject> pdf_pages = new List<GameObject>();

        private string           pdf_path;
        private IntPtr           pdf_document = IntPtr.Zero;

        private float            pdf_current_ratio = 1;

        public void Init(PDFSession session, string path) {
            // External components
            pdf_session = session;
            pdf_path = path;
            // IF all is ready
            if (pdf_session.check()) {
                // Load document
                pdf_document = LibPDF.libpdf_load_document(pdf_session.get(), path);
                // Load pages
                if (ok()) {
                    int pagenum = count();
                    for (int i = 0; i < pagenum; ++i) {
                        // Create new unity node
                        GameObject pdf_page = GameObject.CreatePrimitive(PrimitiveType.Plane);
                        pdf_page.name = "Page " + i.ToString();
                        // Attach page script
                        pdf_page.AddComponent<PDFPage>();
                        pdf_page.GetComponent<PDFPage>().Init(pdf_session, this, i);
                        // Set rendering properties
                        pdf_page.transform.parent = transform;
                        pdf_page.GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Default");
                        // Save
                        pdf_pages.Add(pdf_page);
                    }
                }
            }
            View(0.0f);
        }

        public void Delete() {
            // Unload doducment
            if (ok()) {
                LibPDF.libpdf_free_document(pdf_session.get(), pdf_document);
                pdf_document = IntPtr.Zero;
            }
            // Unload pages
            for (int i = 0; i < pdf_pages.Count; ++i) {
                GameObject pdf_page = pdf_pages[i];
                pdf_page.GetComponent<PDFPage>().Delete();
                Destroy(pdf_page);
            }
            pdf_pages.Clear();
        }

        public float ratio() {
            return pdf_current_ratio;
        }

        public float DegreeToRadian(float angle) {
           return Mathf.PI * angle / 180.0f;
        }

        public void View(float rpage) {
            // Clean page position
            float page = Mathf.Min(Mathf.Max(rpage, 0.0f), pdf_pages.Count - 1);
            // Get book page indexes
            int ipage = (int)page;
            int ipage_num = (int)(page / 2.0f);
            int ipage_off = (int)(page % 2.0f);
            int ipage2 = (int)(page + 1);
            int ipage2_num = (int)((page + 1) / 2.0f);
            // Check if rotation is needed
            float rotation_mult = 0;
            float rotation_ratio = 0;
            if (ipage_num != ipage2_num) {
                rotation_mult = 1;
                rotation_ratio = (float)ipage2 - page;
            }
            // Focus on the page viewed
            float view_xpos = 0.0f;
            if (ipage_off == 0) {
                view_xpos = 0.5f - ((float)ipage2 - page);
            }
            if (ipage_off == 1) {
                view_xpos = -0.5f + ((float)ipage2 - page);
            }
            // Get zoom ratio
            pdf_current_ratio = pdf_pages[ipage].GetComponent<PDFPage>().ratio();
            if (page > ipage) {
                float next_ratio = pdf_pages[ipage2].GetComponent<PDFPage>().ratio();
                pdf_current_ratio = (pdf_current_ratio + next_ratio) / 2.0f;
            }
            // For every page
            for (int i = 0; i < pdf_pages.Count; ++i) {
                // Get page
                GameObject pdf_page = pdf_pages[i];
                // Get page book position
                int i_num = (int)(i / 2.0f);
                int i_off = (int)(i % 2.0f);
                // Right or left side of the book
                float zrot = 0;
                float xpos = 0;
                float ypos = 0.001f;
                if (i_off == 0) {
                    xpos = 0.51f;
                }
                if (i_off == 1) {
                    xpos = -0.51f;
                }
                // Set rotation for left and right sides
                if (i_off == 0 && i_num == ipage2_num) {
                    zrot = 180 * rotation_ratio * rotation_mult;
                }
                if (i_off == 1 && i_num == ipage_num) {
                    zrot = -180 * (1.0f - rotation_ratio) * rotation_mult;
                }
                // Apply transformations
                if (i_num == ipage_num || i_num == ipage2_num) {
                    // Show only good pages
                    pdf_page.GetComponent<Renderer>().enabled = true;
                    // Rotate center and angle
                    pdf_page.transform.localRotation = Quaternion.Euler(0, 0, zrot);
                    float rcos = Mathf.Cos(DegreeToRadian(zrot));
                    float rsin = Mathf.Sin(DegreeToRadian(zrot));
                    pdf_page.transform.localPosition = new Vector3(
                        xpos * rcos - ypos * rsin + view_xpos,
                        xpos * rsin + ypos * rcos,
                        0
                    );
                } else {
                    pdf_page.GetComponent<Renderer>().enabled = false;
                }
            }
        }

        public int count() {
            if (check()) {
                return LibPDF.libpdf_count_pages(pdf_session.get(), pdf_document);
            }
            return -1;
        }

        public bool ok() {
            if (pdf_session.ok() && pdf_document != IntPtr.Zero) {
                return true;
            }
            return false;
        }

        public bool check() {
            if (!ok()) {
                Debug.LogError("Unable to load PDF document: " + pdf_path);
                return false;
            }
            return true;
        }

        public IntPtr get() {
            return pdf_document;
        }
    }
}
