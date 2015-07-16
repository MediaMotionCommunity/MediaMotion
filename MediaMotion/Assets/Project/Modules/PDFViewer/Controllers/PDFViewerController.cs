using System.Collections.Generic;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Modules.PDFViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Controllers
{
	/// <summary>
	/// PDFViewer Controller (apply it on the scene camera)
	/// </summary>
	public class PDFViewerController : AScript<PDFViewerModule, PDFViewerController>
	{
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		// Pdf components
		private GameObject       pdf_scene;
		private PDFSession 	     pdf_session;
		private List<GameObject> pdf_documents = new List<GameObject>();

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
			InitViewer();
			AddDocument("/Users/vincentbrunet/TEST-PDF.pdf");
		}

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public void Delete() {
			DeleteViewer();
		}

		/// <summary>
		/// Init the Scene
		/// </summary>
		public void InitViewer()
		{
			// Load scene
			pdf_scene = new GameObject();
			pdf_scene.name = "PDF Viewer Scene";
			// Load session
			pdf_session = new PDFSession();
		}

		/// <summary>
		/// Delete the Scene
		/// </summary>
		public void DeleteViewer()
		{
			// Unload documents
            for (int i = 0; i < pdf_documents.Count; ++i) {
                DeleteDocument(pdf_documents[i]);
            }
            pdf_documents.Clear();
            // Destroy scene
			Destroy(pdf_scene);
		}

		/// <summary>
		/// Load a document in the Scene
		/// </summary>
		public void AddDocument(string path) {
			// Create new unity node
			GameObject pdf_document = new GameObject();
			pdf_document.name = "Document " + path;
			// Attach page script
            pdf_document.AddComponent<PDFDocument>();
            pdf_document.GetComponent<PDFDocument>().Init(pdf_session, path);
            // Set rendering properties
            pdf_document.transform.parent = pdf_scene.transform;
            pdf_document.AddComponent<MeshRenderer>();
            // Save
            pdf_documents.Add(pdf_document);
		}

		/// <summary>
		/// Delete a document
		/// </summary>
		public void DeleteDocument(GameObject pdf_document) {
            pdf_document.GetComponent<PDFDocument>().Delete();
            Destroy(pdf_document);
		}

		/// <summary>
		/// Set the view to a document specific page
		/// </summary>
		public void ViewDocument(int idx, float page, float zoom, float yoff) {
			// Do nothing if nothing loaded
			if (pdf_documents.Count > 0) {
				// Get focused doc
				GameObject pdf_document = pdf_documents[idx % pdf_documents.Count];
				PDFDocument behavior = pdf_document.GetComponent<PDFDocument>();
				// Update document display
				behavior.View(page);
				pdf_document.transform.position = new Vector3(0, 0, 0);
				pdf_document.transform.rotation = Quaternion.Euler(90, 0, 0);
				// Set camera
				transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
				transform.position = new Vector3(0, yoff / 2.0f * behavior.ratio(), zoom * behavior.ratio());
				// TODO set position and states of non-focused documents
			}
		}

		/// <summary>
		/// Sample View
		/// </summary>
		private int   pdf_document_idx = 0; // Current focused document index
		private float pdf_page = 1;         // Current focused document page position (can be float for animations)
		private float pdf_zoom = 0.9f;      // Current view zoom (bigger == farther)
		private float pdf_yoff = 0.0f;      // Current yoffset (1 == top, -1 == bottom)
		public void Update()
		{
			// Sample view example (display all moving pages of first doc loaded)
			if (pdf_documents.Count > 0) {
				float slow_motion = 10.0f;
				GameObject document = pdf_documents[0];
				PDFDocument behavior = document.GetComponent<PDFDocument>();
				float hpagecount = (behavior.count() - 1) / 2.0f;
				pdf_page = hpagecount + Mathf.Sin(Time.time / slow_motion) * hpagecount;
				pdf_yoff = Mathf.Sin(Time.time / slow_motion * hpagecount * 2.0f);
				ViewDocument(pdf_document_idx, pdf_page, pdf_zoom, pdf_yoff);
			}
			// Handle user actions
			/*
			foreach (IAction action in this.inputService.GetMovements()) {
				Debug.Log(action.Type);
				switch (action.Type) {
				}
			}
			*/
		}
	}
}
