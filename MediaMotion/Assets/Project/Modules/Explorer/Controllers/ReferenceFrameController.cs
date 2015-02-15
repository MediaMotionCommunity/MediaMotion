using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;

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
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
		}

		/// <summary>
		/// Reset position.
		/// </summary>
		public void ResetPosition() {
			this.gameObject.transform.position = new UnityEngine.Vector3(0, 0, 0);
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements(ActionType.BrowsingScroll)) {
				this.gameObject.transform.Translate(0, 0, (action.Parameter as Object3).Pos.Z / 20, UnityEngine.Space.World);
			}
		}
	}
}
