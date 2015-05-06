using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;
using PDF;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDFViewer Controller
	/// </summary>
	public class PDFViewerController : BaseUnityScript<PDFViewerController> {
		/// <summary>
		/// The module instance
		/// </summary>
		private PDFViewerModule moduleInstance;
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
		private IntPtr pdf_pixels = IntPtr.Zero;

		private Texture2D pdf_texture = null;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		/// <param name="input">The input.</param>
		/// <param name="playlist">The playlist.</param>
		public void Init(PDFViewerModule module, IInputService input, IPlaylistService playlist) {
			this.moduleInstance = module;
			this.inputService = input;
			this.playlistService = playlist;
			this.playlistService.Configure(((this.moduleInstance.Parameters.Length > 0) ? (this.moduleInstance.Parameters[0]) : (null)), new string[] { ".pdf", ".xps" });
			this.InitPDF();
			this.LoadPDF();
		}

		private void InitPDF()
		{
			ClearSession();
			pdf_session = LibPDF.libpdf_load_session();
		}

		private void LoadPDF()
		{
			string path = this.playlistService.Current().GetPath();
			Debug.Log(path);
			ClearTexture();
			ClearPage();
			ClearDocument();
			pdf_document = LibPDF.libpdf_load_document(pdf_session, path);
			pdf_page = LibPDF.libpdf_load_page(pdf_session, pdf_document, 1);
			int tex_xsize = LibPDF.libpdf_xsize_page(pdf_session, pdf_page);
			int tex_ysize = LibPDF.libpdf_ysize_page(pdf_session, pdf_page);
			pdf_texture = Texture2D(tex_xsize, tex_ysize, TextureFormat.RGBA32, false);
			if (GetComponent<Renderer>()) {
				GetComponent<Renderer>().material.mainTexture = pdf_texture;
			}
			pdf_pixels = LibPDF.libpdf_pixels_page(pdf_session, pdf_page); // Ready to render
		}

		public void Update()
		{
			// If page buffer ready apply copied texture on mesh
			if (pdf_pixels != IntPtr.Zero) {
				LibPDF.libpdf_render_page(pdf_session, pdf_page);
				pdf_texture.SetPixels32(pdf_pixels, 0);
				pdf_texture.Apply();
			}
			/*
			foreach (IAction action in this.inputService.GetMovements()) {
				if (action.Type == ActionType.Right) {
					this.playlistService.Next();
					this.gameObject.transform.Rotate(new Vector3(0, 90, 0));
					this.LoadVideo();
				} else if (action.Type == ActionType.Left) {
					this.playlistService.Previous();
					this.gameObject.transform.Rotate(new Vector3(0, -90, 0));
					this.LoadVideo();
				}
			}
			*/
		}

		private void ClearTexture()
		{
			if (pdf_texture != null) {
				UnityEngine.Object.Destroy(pdf_texture);
				pdf_texture = Texture2D();
				if (GetComponent<Renderer>()) {
					GetComponent<Renderer>().material.mainTexture = pdf_texture;
				}
			}
		}

		private void ClearSession()
		{
			if (pdf_session != IntPtr.Zero) {
				LibPDF.libpdf_free_session(pdf_session);
				pdf_session = IntPtr.Zero;
			}
		}

		private void ClearDocument()
		{
			if (pdf_document != IntPtr.Zero) {
				LibPDF.libpdf_free_document(pdf_document);
				pdf_document = IntPtr.Zero;
			}
		}

		private void ClearPage()
		{
			if (pdf_page != IntPtr.Zero) {
				pdf_pixels = IntPtr.Zero;
				LibPDF.libpdf_free_page(pdf_page);
				pdf_page = IntPtr.Zero;
			}
		}

		public void OnDestroy() {
			ClearTexture();
			ClearPage();
			ClearDocument();
			ClearSession();
		}

	}
}
