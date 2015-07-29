using System;
using MediaMotion.Core.Utils;
using MediaMotion.Modules.PDFViewer.Services.MuPDF.Bindings;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDFViewer page
	/// </summary>
	public class PDFPage : MonoBehaviour {
		private PDFSession pdfSession;
		private PDFDocument pdfDocument;
		private IntPtr pdfPage = IntPtr.Zero;
		private int pdfPagenum;
		private int pdfTextureXSize;
		private int pdfTextureYSize;
		private Texture pdfTextureBase = null;
		private Texture2D pdfTexture = null;
		private AutoPinner pdfTexturePixels = null;

		public void Init(PDFSession session, PDFDocument document, int pagenum) {
			float pdf_dpi = 101;

			this.pdfTextureBase = GetComponent<Renderer>().material.mainTexture;
			this.pdfSession = session;
			this.pdfDocument = document;
			this.pdfPagenum = pagenum;
			if (this.pdfSession.Check() && this.pdfDocument.Check()) {
				this.pdfPage = LibMuPDF.libpdf_load_page(this.pdfSession.Get(), this.pdfDocument.Get(), this.pdfPagenum, pdf_dpi, pdf_dpi);
				if (this.pdfPage != IntPtr.Zero) {
					float size = 1.0f / 10.0f;

					this.pdfTextureXSize = LibMuPDF.libpdf_xsize_page(pdfSession.Get(), pdfPage);
					this.pdfTextureYSize = LibMuPDF.libpdf_ysize_page(pdfSession.Get(), pdfPage);
					this.pdfTexturePixels = new AutoPinner(new Color32[this.pdfTextureXSize * this.pdfTextureYSize]);
					this.pdfTexture = new Texture2D(this.pdfTextureXSize, this.pdfTextureYSize, TextureFormat.RGBA32, false);
					this.Render();
					this.transform.localScale = new Vector3(size, size, -Ratio() * size);
					if (this.GetComponent<Renderer>() && this.Ok()) {
						this.GetComponent<Renderer>().material.mainTexture = this.pdfTexture;
						this.GetComponent<Renderer>().material.shader = Shader.Find("Unlit/Texture");
					}
				}
			}
		}

		public void OnDestoy() {
			if (this.pdfPage != IntPtr.Zero) {
				LibMuPDF.libpdf_free_page(this.pdfSession.Get(), this.pdfPage);
			}
			this.GetComponent<Renderer>().material.mainTexture = this.pdfTextureBase;
			if (this.pdfTexture != null) {
				Texture2D.Destroy(this.pdfTexture);
			}
		}

		public void Render() {
			if (this.Ok()) {
				LibMuPDF.libpdf_render_page(this.pdfSession.Get(), this.pdfPage, 0, 0, 1, 1, 0);
				if (this.pdfTexturePixels != null) {
					LibMuPDF.memcpy(this.pdfTexturePixels.Ptr, LibMuPDF.libpdf_pixels_page(this.pdfSession.Get(), this.pdfPage), this.pdfTextureXSize * this.pdfTextureYSize * 4);
				}
				if (this.pdfTexture != null) {
					this.pdfTexture.SetPixels32((Color32[])this.pdfTexturePixels.Obj);
					this.pdfTexture.Apply();
				}
			}
		}

		public float Ratio() {
			return ((float)this.pdfTextureYSize / (float)this.pdfTextureXSize);
		}

		public int Error() {
			if (this.pdfSession.Check() && this.pdfDocument.Ok() && this.pdfPage != IntPtr.Zero) {
				return (LibMuPDF.libpdf_error_page(this.pdfSession.Get(), this.pdfPage));
			}
			return (-1);
		}

		public bool Ok() {
			if (this.Error() == 0) {
				if (this.pdfTexturePixels != null && this.pdfTexture != null) {
					return (true);
				}
			}
			return (false);
		}

		public bool Check() {
			if (!this.Ok()) {
				Debug.LogError("Unable to load PDF page: " + this.pdfPagenum + " (error " + this.Error().ToString() + ")");
				return (false);
			}
			return (true);
		}

		public IntPtr Get() {
			return (this.pdfPage);
		}
	}
}
