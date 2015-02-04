using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Controllers.Loader {
	/// <summary>
	/// Camera Controller
	/// </summary>
	public class CameraController : BaseUnityScript<CameraController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.inputService.GetMovements();
		}
	}
}
