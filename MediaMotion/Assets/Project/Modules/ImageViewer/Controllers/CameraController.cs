using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Camera controller
	/// </summary>
	public class CameraController : AScript<ImageViewerModule, CameraController> {
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
						this.ZoomIn((float)action.Parameter);
						break;
					case ActionType.ZoomOut:
						this.ZoomOut((float)action.Parameter);
						break;
				}
			}
		}

		/// <summary>
		/// Zooms the in.
		/// </summary>
		/// <param name="distance">The distance.</param>
		public void ZoomIn(float velocity) {
			Vector3 position = this.transform.localPosition;

			position.z = Mathf.Max(position.z / (1.5f * velocity), -25.0f);
			this.transform.localPosition = position;
		}

		/// <summary>
		/// Zooms the out.
		/// </summary>
		/// <param name="distance">The distance.</param>
		public void ZoomOut(float velocity) {
			Vector3 position = this.transform.localPosition;

			position.z = Mathf.Max(position.z * (1.5f * velocity), -25.0f);
			this.transform.localPosition = position;
		}
	}
}
