using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Reference Frame Controller
	/// </summary>
	public class ReferenceFrameController : BaseUnityScript<ReferenceFrameController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The reset
		/// </summary>
		public bool Reset { get; set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.Reset = false;
			this.inputService = inputService;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			if (this.Reset) {
				this.gameObject.transform.position = new Vector3(0, 0, 0);
				this.Reset = false;
			}
			foreach (IAction action in this.inputService.GetMovements(ActionType.BrowsingScroll)) {
					MediaMotion.Motion.Actions.Parameters.Vector3 parameter = action.Parameter as MediaMotion.Motion.Actions.Parameters.Vector3;

					this.gameObject.transform.Translate(0, 0, (float)parameter.Z / 20, Space.World);
			}
		}
	}
}
