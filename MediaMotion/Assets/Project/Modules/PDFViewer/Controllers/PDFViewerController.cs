using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;
using MediaMotion.Modules.PDFViewer.Models;

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
			this.AddDocument((IPDF)this.module.Parameters.FirstOrDefault());
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
		public void AddDocument(IPDF element) {
			// Create new unity node
			GameObject pdf_document = new GameObject();

			pdf_document.name = "Document " + element.GetName();
			pdf_document.AddComponent<PDFDocument>();
			pdf_document.GetComponent<PDFDocument>().Init(this.pdf_session, element);
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
		private int pdfDocumentIdx = 0;
		private float pdfPage = 1;
		private float pdfZoom = 0.865f;
		private float pdfYoff = 0.0f;

		public void Update() {
			if (this.pdf_documents.Count > 0) {
				float slowMotion = 10.0f;
				GameObject document = this.pdf_documents[0];
				PDFDocument behavior = document.GetComponent<PDFDocument>();
				float hpagecount = (behavior.Count() - 1) / 2.0f;

				this.pdfPage = hpagecount + Mathf.Sin(Time.time / slowMotion) * hpagecount;
				this.pdfYoff = Mathf.Sin(Time.time / slowMotion * hpagecount * 2.0f) / 2.0f;
				this.ViewDocument(this.pdfDocumentIdx, this.pdfPage, this.pdfZoom, this.pdfYoff);
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
