using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Camera controller
	/// </summary>
	public class CameraController : BaseUnityScript<CameraController> {
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
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.ZoomIn:
						this.ZoomIn();
						break;
					case ActionType.ZoomOut:
						this.ZoomOut();
						break;
					case ActionType.Rotate:
						this.Rotate(action.Parameter);
						break;
				}
			}
		}

		/// <summary>
		/// Zooms the in.
		/// </summary>
		/// <param name="distance">The distance.</param>
		public void ZoomIn(float distance = 1.0f) {
			this.Zoom(distance);
		}

		/// <summary>
		/// Zooms the out.
		/// </summary>
		/// <param name="distance">The distance.</param>
		public void ZoomOut(float distance = 1.0f) {
			this.Zoom(-distance);
		}

		/// <summary>
		/// Zooms the specified coefficient.
		/// </summary>
		/// <param name="distance">The distance.</param>
		public void Zoom(float distance) {
			this.transform.Translate(new Vector3(0, 0, distance));
		}

		/// <summary>
		/// Rotates the specified toto.
		/// </summary>
		/// <param name="toto">The toto.</param>
		public void Rotate(object toto) {
			this.transform.Rotate(new Vector3(0, 0, 1), 90);
		}
	}
}
