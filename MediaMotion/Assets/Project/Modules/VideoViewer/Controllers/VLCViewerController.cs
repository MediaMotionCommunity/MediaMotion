using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Modules.VideoViewer.Controllers.Binding;
using UnityEngine;

namespace MediaMotion.Modules.VideoViewer.Controllers {
	/// <summary>
	/// vlcViewer Controller (apply it on the scene camera)
	/// </summary>
	public class VLCViewerController : AScript<VideoViewerModule, VLCViewerController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		// vlc components
		private GameObject vlcScene;
		private VLCSession vlcSession;
		private List<GameObject> vlcMedias;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;

			this.InitViewer();
			this.vlcMedias = new List<GameObject>();
			this.AddMedia(this.module.Parameters.FirstOrDefault().GetPath());
		}

		/// <summary>
		/// Destroy this instance.
		/// </summary>
		public void OnDestroy() {
			for (int i = 0; i < this.vlcMedias.Count; ++i) {
				GameObject.Destroy(this.vlcMedias[i]);
			}
			this.vlcMedias.Clear();
			GameObject.Destroy(this.vlcScene);
		}

		/// <summary>
		/// Init the Scene
		/// </summary>
		public void InitViewer() {
			// Load scene
			this.vlcScene = new GameObject();
			this.vlcScene.name = "VLC Viewer Scene";

			this.vlcSession = new VLCSession();
		}

		/// <summary>
		/// Load a media in the Scene
		/// </summary>
		public void AddMedia(string path) {
			GameObject vlc_media = GameObject.CreatePrimitive(PrimitiveType.Plane);

			vlc_media.name = "Media " + path;
			vlc_media.AddComponent<VLCMedia>();
			vlc_media.GetComponent<VLCMedia>().Init(this.vlcSession, path);
			vlc_media.transform.parent = this.vlcScene.transform;
			this.vlcMedias.Add(vlc_media);
		}

		/// <summary>
		/// Set the view to a document specific page
		/// </summary>
		public void ViewMedia(int idx, float timer, float zoom, float yoff) {
			if (vlcMedias.Count > 0) {
				GameObject vlc_media = vlcMedias[idx % vlcMedias.Count];
				VLCMedia behavior = vlc_media.GetComponent<VLCMedia>();

				vlc_media.transform.position = new Vector3(0, 0, 0);
				vlc_media.transform.rotation = Quaternion.Euler(90, 0, 0);

				float iratio = behavior.ratio();
				this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
				this.transform.position = new Vector3(0, yoff / 2.0f, zoom);
				
				// TODO set position and states of non-focused medias
			}
		}

		/// <summary>
		/// Sample View
		/// </summary>
		private int vlcMediaIdx = 0; // Current focused document index
		private float vlcZoom = 0.865f; // Current view zoom (bigger == farther)
		private float vlcYoff = 0.0f;   // Current yoffset (1 == top, -1 == bottom)

		public void Update() {
			if (vlcMedias.Count > 0) {
				GameObject document = vlcMedias[0];
				VLCMedia behavior = document.GetComponent<VLCMedia>();

				this.ViewMedia(vlcMediaIdx, 0.0f, vlcZoom, vlcYoff);
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
