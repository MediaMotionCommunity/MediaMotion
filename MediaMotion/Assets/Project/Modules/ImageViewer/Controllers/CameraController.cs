using System;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Controllers {
	/// <summary>
	/// Camera controller
	/// </summary>
	public class CameraController : AScript<ImageViewerModule, CameraController> {
		private const float RatioZoom = 0.15f;
		private const float MinPositionZ = 0.0f;
		private const float MaxPositionZ = -25.0f;

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
			foreach (var action in this.inputService.GetMovements()) {
				switch (action.Type) {
					case ActionType.ZoomIn:
						this.ZoomIn((float) action.Parameter);
						break;
					case ActionType.ZoomOut:
						this.ZoomOut((float) action.Parameter);
						break;
				}
			}
		}

		/// <summary>
		/// Zooms the in.
		/// </summary>
		/// <param name="velocity">The distance.</param>
		public void ZoomIn(float velocity) {
			Vector3 position = this.transform.localPosition;

			position.z = Mathf.Min(position.z + GetRatioZoom(position.z) * velocity, MinPositionZ);
			this.transform.localPosition = position;
		}

		/// <summary>
		/// Zooms the out.
		/// </summary>
		/// <param name="velocity">The distance.</param>
		public void ZoomOut(float velocity) {
			Vector3 position = this.transform.localPosition;

			position.z = Mathf.Max(position.z - GetRatioZoom(position.z) * velocity, MaxPositionZ);
			this.transform.localPosition = position;
		}

		private float GetRatioZoom(float positionZ) {
			float multiplier = Mathf.Abs(positionZ) / 10.0f;

			if (Math.Abs(multiplier) < 0.0001) {
				return (RatioZoom);
			}
			return (RatioZoom * multiplier);
		}
	}
}
