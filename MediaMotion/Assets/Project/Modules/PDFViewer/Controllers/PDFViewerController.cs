using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MuPDF;
using UnityEngine;


namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// C# Unmanaged pointer wrapper
	/// </summary>
	class AutoPinner : IDisposable {
		object _obj;
		GCHandle _pinnedArray;

		public AutoPinner(object obj) {
			_obj = obj;
			_pinnedArray = GCHandle.Alloc(_obj, GCHandleType.Pinned);
		}
		public static implicit operator IntPtr(AutoPinner ap) {
			return ap._pinnedArray.AddrOfPinnedObject();
		}
		public object get() {
			return _obj;
		}
		public void Dispose() {
			_pinnedArray.Free();
		}
	}

	/// <summary>
	/// PDFViewer Controller
	/// </summary>
	public class PDFViewerController : AScript<PDFViewerModule, PDFViewerController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The playlist service
		/// </summary>
		private IPlaylistService playlistService;

		private IntPtr pdf_session = IntPtr.Zero;
		private IntPtr pdf_document = IntPtr.Zero;
		private IntPtr pdf_page = IntPtr.Zero;

		private int pdf_texture_size = 0;
		private AutoPinner pdf_pixels = null;
		private Texture2D pdf_texture = null;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(IInputService input, IPlaylistService playlist) {
			this.inputService = input;
			this.playlistService = playlist;
			this.playlistService.Configure(((this.module.Parameters.Length > 0) ? (this.module.Parameters[0]) : (null)), new string[] { ".pdf", ".xps" });
			this.InitPDF();
			this.LoadPDF();
		}

		private void InitPDF() {
			this.ClearSession();
			pdf_session = LibPDF.libpdf_load_session();
		}

		private void LoadPDF() {
			string path = this.playlistService.Current().GetPath();
			Debug.Log(path);
			this.ClearBuffer();
			this.ClearTexture();
			this.ClearPage();
			this.ClearDocument();
			pdf_document = LibPDF.libpdf_load_document(pdf_session, path);
			pdf_page = LibPDF.libpdf_load_page(pdf_session, pdf_document, 0);
			int tex_xsize = LibPDF.libpdf_xsize_page(pdf_session, pdf_page);
			int tex_ysize = LibPDF.libpdf_ysize_page(pdf_session, pdf_page);
			pdf_texture_size = tex_xsize * tex_ysize;
			pdf_texture = new Texture2D(tex_xsize, tex_ysize, TextureFormat.RGBA32, false);
			if (this.GetComponent<Renderer>()) {
				this.GetComponent<Renderer>().material.mainTexture = pdf_texture;
			}
			float ratio = 5.0f / tex_ysize;
			transform.localScale = new Vector3(-ratio * tex_xsize, ratio * tex_ysize, 1);
			pdf_pixels = new AutoPinner(new Color32[pdf_texture_size]);
		}

		public void Update() {
			// If page buffer ready apply copied texture on mesh
			if (pdf_pixels != null) {
				// Copy rendering into custom buffer
				LibPDF.libpdf_render_page(pdf_session, pdf_page);
				LibPDF.memcpy(
					(IntPtr)pdf_pixels,
					LibPDF.libpdf_pixels_page(pdf_session, pdf_page),
					pdf_texture_size * 4
				);
				// Set custom buffer as texture
				pdf_texture.SetPixels32((Color32[])pdf_pixels.get(), 0);
				pdf_texture.Apply();
			}
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					default:
						// TODO add movement
						break;
				}
			}
		}

		private void ClearTexture() {
			if (pdf_texture != null) {
				UnityEngine.Object.Destroy(pdf_texture);
				pdf_texture = new Texture2D(1, 1);
				if (this.GetComponent<Renderer>()) {
					this.GetComponent<Renderer>().material.mainTexture = pdf_texture;
				}
			}
		}

		private void ClearSession() {
			if (pdf_session != IntPtr.Zero) {
				LibPDF.libpdf_free_session(pdf_session);
				pdf_session = IntPtr.Zero;
			}
		}

		private void ClearDocument() {
			if (pdf_document != IntPtr.Zero) {
				LibPDF.libpdf_free_document(pdf_session, pdf_document);
				pdf_document = IntPtr.Zero;
			}
		}

		private void ClearPage() {
			if (pdf_page != IntPtr.Zero) {
				LibPDF.libpdf_free_page(pdf_session, pdf_page);
				pdf_page = IntPtr.Zero;
			}
		}

		private void ClearBuffer() {
			if (pdf_pixels != null) {
				pdf_pixels = null;
			}
		}

		public void OnDestroy() {
			this.ClearBuffer();
			this.ClearTexture();
			this.ClearPage();
			this.ClearDocument();
			this.ClearSession();
		}

	}
}
