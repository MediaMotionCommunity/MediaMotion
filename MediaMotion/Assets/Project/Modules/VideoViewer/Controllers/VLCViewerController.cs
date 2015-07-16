using System.Collections.Generic;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers
{
	/// <summary>
	/// vlcViewer Controller (apply it on the scene camera)
	/// </summary>
	public class VLCViewerController : AScript<VideoViewerModule, VLCViewerController>
	{
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		// vlc components
		private GameObject       vlc_scene;
		private VLCSession       vlc_session;
		private List<GameObject> vlc_medias = new List<GameObject>();

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
			InitViewer();
			AddMedia("/Users/vincentbrunet/TEST-VIDEO.mkv");
		}

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public void OnDestroy() {
			DeleteViewer();
		}

		/// <summary>
		/// Init the Scene
		/// </summary>
		public void InitViewer()
		{
			// Load scene
			vlc_scene = new GameObject();
			vlc_scene.name = "VLC Viewer Scene";
			// Load session
			vlc_session = new VLCSession();
		}

		/// <summary>
		/// Delete the Scene
		/// </summary>
		public void DeleteViewer()
		{
			// Unload documents
            for (int i = 0; i < vlc_medias.Count; ++i) {
                DeleteMedia(vlc_medias[i]);
            }
            vlc_medias.Clear();
            // Destroy scene
			Destroy(vlc_scene);
		}

		/// <summary>
		/// Load a media in the Scene
		/// </summary>
		public void AddMedia(string path) {
			// Create new unity node
			GameObject vlc_media = GameObject.CreatePrimitive(PrimitiveType.Plane);
			vlc_media.name = "Media " + path;
			// Attach page script
            vlc_media.AddComponent<VLCMedia>();
            vlc_media.GetComponent<VLCMedia>().Init(vlc_session, path);
            // Set rendering properties
            vlc_media.transform.parent = vlc_scene.transform;
            // Save
            vlc_medias.Add(vlc_media);
		}

		/// <summary>
		/// Delete a media
		/// </summary>
		public void DeleteMedia(GameObject vlc_media) {
            Destroy(vlc_media);
		}

		/// <summary>
		/// Set the view to a document specific page
		/// </summary>
		public void ViewMedia(int idx, float timer, float zoom, float yoff) {
			// Do nothing if nothing loaded
			if (vlc_medias.Count > 0) {
				// Get focused doc
				GameObject vlc_media = vlc_medias[idx % vlc_medias.Count];
				VLCMedia behavior = vlc_media.GetComponent<VLCMedia>();
				// Update document display
				vlc_media.transform.position = new Vector3(0, 0, 0);
				vlc_media.transform.rotation = Quaternion.Euler(90, 0, 0);
				// Set camera
				float iratio = behavior.ratio();
				transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
				transform.position = new Vector3(0, yoff / 2.0f, zoom);
				// TODO set position and states of non-focused medias
			}
		}

		/// <summary>
		/// Sample View
		/// </summary>
		private int   vlc_media_idx = 0; // Current focused document index
		private float vlc_zoom = 0.865f; // Current view zoom (bigger == farther)
		private float vlc_yoff = 0.0f;   // Current yoffset (1 == top, -1 == bottom)

		public void Update()
		{
			// Sample view example (display first media loaded)
			if (vlc_medias.Count > 0) {
				GameObject document = vlc_medias[0];
				VLCMedia behavior = document.GetComponent<VLCMedia>();
				ViewMedia(vlc_media_idx, 0.0f, vlc_zoom, vlc_yoff);
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
