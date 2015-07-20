using System.Collections.Generic;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers {
	/// <summary>
	/// PDFViewer Controller (apply it on the scene camera)
	/// </summary>
	public class PDFViewerController : AScript<PDFViewerModule, PDFViewerController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		// Pdf components
		private GameObject pdf_scene;
		private PDFSession pdf_session;
		private List<GameObject> pdf_documents = new List<GameObject>();

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
			this.InitViewer();
			this.AddDocument("/Users/vincentbrunet/TEST-PDF.pdf");
		}

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public void OnDestroy() {
			this.DeleteViewer();
		}

		/// <summary>
		/// Init the Scene
		/// </summary>
		public void InitViewer() {
			this.pdf_scene = new GameObject();
			this.pdf_scene.name = "PDF Viewer Scene";
			this.pdf_session = new PDFSession();
		}

		/// <summary>
		/// Delete the Scene
		/// </summary>
		public void DeleteViewer() {
			for (int i = 0; i < this.pdf_documents.Count; ++i) {
				GameObject.Destroy(this.pdf_documents[i]);
			}
			this.pdf_documents.Clear();
			GameObject.Destroy(this.pdf_scene);
		}

		/// <summary>
		/// Load a document in the Scene
		/// </summary>
		public void AddDocument(string path) {
			// Create new unity node
			GameObject pdf_document = new GameObject();

			pdf_document.name = "Document " + path;
			pdf_document.AddComponent<PDFDocument>();
			pdf_document.GetComponent<PDFDocument>().Init(this.pdf_session, path);
			pdf_document.transform.parent = this.pdf_scene.transform;
			pdf_document.AddComponent<MeshRenderer>();
			this.pdf_documents.Add(pdf_document);
		}

		/// <summary>
		/// Set the view to a document specific page
		/// </summary>
		public void ViewDocument(int idx, float page, float zoom, float yoff) {
			if (this.pdf_documents.Count > 0) {
				GameObject pdf_document = this.pdf_documents[idx % this.pdf_documents.Count];
				PDFDocument behavior = pdf_document.GetComponent<PDFDocument>();

				behavior.View(page);
				pdf_document.transform.position = new Vector3(0, 0, 0);
				pdf_document.transform.rotation = Quaternion.Euler(90, 0, 0);

				float iratio = behavior.Ratio();
				this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
				this.transform.position = new Vector3(0, yoff / 2.0f * iratio, zoom * iratio);
				// TODO set position and states of non-focused documents
			}
		}

		/// <summary>
		/// Sample View
		/// </summary>
		private int pdf_document_idx = 0;
		private float pdf_page = 1;
		private float pdf_zoom = 0.865f;
		private float pdf_yoff = 0.0f;

		public void Update() {
			if (this.pdf_documents.Count > 0) {
				float slow_motion = 10.0f;
				GameObject document = this.pdf_documents[0];
				PDFDocument behavior = document.GetComponent<PDFDocument>();
				float hpagecount = (behavior.Count() - 1) / 2.0f;

				this.pdf_page = hpagecount + Mathf.Sin(Time.time / slow_motion) * hpagecount;
				this.pdf_yoff = Mathf.Sin(Time.time / slow_motion * hpagecount * 2.0f) / 2.0f;
				this.ViewDocument(this.pdf_document_idx, this.pdf_page, this.pdf_zoom, this.pdf_yoff);
			}
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					default:
						break;
				}
			}
		}
	}
}
