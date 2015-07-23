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
		private GameObject vlcScene;

		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		public VLCSession Session { get; private set; }

		/// <summary>
		/// Gets the medias.
		/// </summary>
		/// <value>
		/// The medias.
		/// </value>
		public List<GameObject> Medias { get; private set; }

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;

			this.InitViewer();
			this.Medias = new List<GameObject>();
			this.AddMedia(this.module.Parameters.FirstOrDefault().GetPath());
		}

		/// <summary>
		/// Destroy the object
		/// </summary>
		public void OnDestroy() {
			for (int i = 0; i < this.Medias.Count; ++i) {
				GameObject.Destroy(this.Medias[i]);
			}
			this.Medias.Clear();
			GameObject.Destroy(this.vlcScene);
		}

		/// <summary>
		/// Init the Scene
		/// </summary>
		public void InitViewer() {
			this.vlcScene = new GameObject();
			this.vlcScene.name = "VLC Viewer Scene";
			this.Session = new VLCSession();
		}

		/// <summary>
		/// Load a media in the Scene
		/// </summary>
		public void AddMedia(string path) {
			GameObject vlcMedia = GameObject.CreatePrimitive(PrimitiveType.Plane);

			vlcMedia.name = "Media " + path;
			vlcMedia.AddComponent<MediaController>();
			vlcMedia.GetComponent<MediaController>().Init(this.Session, path);
			vlcMedia.transform.parent = this.vlcScene.transform;
			this.Medias.Add(vlcMedia);
		}

		/// <summary>
		/// Set the view to a document specific page
		/// </summary>
		public void ViewMedia(int idx, float timer, float zoom, float yoff) {
			if (Medias.Count > 0) {
				GameObject media = Medias[idx % Medias.Count];
				MediaController behavior = media.GetComponent<MediaController>();

				media.transform.position = new Vector3(0, 0, 0);
				media.transform.rotation = Quaternion.Euler(90, 0, 0);

				float iratio = behavior.Ratio();
				this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, -1));
				this.transform.position = new Vector3(0, yoff / 2.0f, zoom);

				// TODO set position and states of non-focused medias
			}
		}

		/// <summary>
		/// Sample View
		/// </summary>
		private int vlcMediaIdx = 0;
		private float vlcZoom = 0.865f;
		private float vlcYoff = 0.0f;

		public void Update() {
			/*if (Medias.Count > 0) {
				GameObject document = Medias[0];
				VLCMedia behavior = document.GetComponent<VLCMedia>();

				this.ViewMedia(vlcMediaIdx, 0.0f, vlcZoom, vlcYoff);
			}*/
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					default:
						break;
				}
			}
		}
	}
}
