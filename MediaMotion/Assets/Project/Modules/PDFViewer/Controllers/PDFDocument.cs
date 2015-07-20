using System;
using System.Collections.Generic;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDFViewer Document
	/// </summary>
	public class PDFDocument : MonoBehaviour {
		private PDFSession pdfSession;
		private List<GameObject> pdfPages = new List<GameObject>();

		private string pdfPath = "";
		private IntPtr pdfDocument = IntPtr.Zero;

		private float pdfCurrentRatio = 1;

		public void Init(PDFSession session, string path) {
			this.pdfSession = session;
			this.pdfPath = path;

			if (this.pdfSession.Check()) {
				this.pdfDocument = LibPDF.libpdf_load_document(this.pdfSession.Get(), this.pdfPath);
				if (this.Ok()) {
					int pagenum = Count();

					for (int i = 0; i < pagenum; ++i) {
						GameObject pdf_page = GameObject.CreatePrimitive(PrimitiveType.Plane);

						pdf_page.name = "Page " + i.ToString();
						pdf_page.AddComponent<PDFPage>();
						pdf_page.GetComponent<PDFPage>().Init(this.pdfSession, this, i);
						pdf_page.transform.parent = this.transform;
						this.pdfPages.Add(pdf_page);
					}
				}
			}
			this.View(0.0f);
		}

		public void OnDestroy() {
			if (this.Ok()) {
				LibPDF.libpdf_free_document(pdfSession.Get(), this.pdfDocument);
				this.pdfDocument = IntPtr.Zero;
			}
			for (int i = 0; i < pdfPages.Count; ++i) {
				GameObject.Destroy(pdfPages[i]);
			}
			this.pdfPages.Clear();
		}

		public float Ratio() {
			return (this.pdfCurrentRatio);
		}

		public float DegreeToRadian(float angle) {
			return (Mathf.PI * angle / 180.0f);
		}

		public void View(float rpage) {
			float page = Mathf.Min(Mathf.Max(rpage, 0.0f), this.pdfPages.Count - 1);
			int ipage = (int)page;
			int ipage_num = (int)(page / 2.0f);
			int ipage_off = (int)(page % 2.0f);
			int ipage2 = (int)(page + 1);
			int ipage2_num = (int)((page + 1) / 2.0f);
			float page_xoff = 0.505f;
			float rotation_mult = 0;
			float rotation_ratio = 0;
			float view_xpos = 0.0f;

			if (ipage_num != ipage2_num) {
				rotation_mult = 1;
				rotation_ratio = (float)ipage2 - page;
			}
			if (ipage_off == 0) {
				view_xpos = page_xoff - ((float)ipage2 - page);
			}
			if (ipage_off == 1) {
				view_xpos = -page_xoff + ((float)ipage2 - page);
			}

			this.pdfCurrentRatio = this.pdfPages[ipage].GetComponent<PDFPage>().Ratio();
			if (page > ipage) {
				float next_ratio = this.pdfPages[ipage2].GetComponent<PDFPage>().Ratio();

				this.pdfCurrentRatio = (this.pdfCurrentRatio + next_ratio) / 2.0f;
			}
			for (int i = 0; i < this.pdfPages.Count; ++i) {
				GameObject pdf_page = this.pdfPages[i];
				int i_num = (int)(i / 2.0f);
				int i_off = (int)(i % 2.0f);
				float zrot = 0;
				float xpos = 0;
				float ypos = 0.001f;

				if (i_off == 0) {
					xpos = page_xoff;
				}
				if (i_off == 1) {
					xpos = -page_xoff;
				}
				if (i_off == 0 && i_num == ipage2_num) {
					zrot = 180 * rotation_ratio * rotation_mult;
				}
				if (i_off == 1 && i_num == ipage_num) {
					zrot = -180 * (1.0f - rotation_ratio) * rotation_mult;
				}
				if (i_num == ipage_num || i_num == ipage2_num) {
					float rcos = Mathf.Cos(DegreeToRadian(zrot));
					float rsin = Mathf.Sin(DegreeToRadian(zrot));

					pdf_page.GetComponent<Renderer>().enabled = true;
					pdf_page.transform.localRotation = Quaternion.Euler(0, 0, zrot);
					pdf_page.transform.localPosition = new Vector3((((xpos * rcos) - (ypos * rsin)) + view_xpos), ((xpos * rsin) + (ypos * rcos)), 0);
				} else {
					pdf_page.GetComponent<Renderer>().enabled = false;
				}
			}
		}

		public int Count() {
			if (this.Check()) {
				return (LibPDF.libpdf_count_pages(this.pdfSession.Get(), this.pdfDocument));
			}
			return (-1);
		}

		public bool Ok() {
			return (this.pdfSession.Ok() && this.pdfDocument != IntPtr.Zero);
		}

		public bool Check() {
			if (!this.Ok()) {
				Debug.LogError("Unable to load PDF document: " + this.pdfPath);
				return (false);
			}
			return (true);
		}

		public IntPtr Get() {
			return (this.pdfDocument);
		}
	}
}
