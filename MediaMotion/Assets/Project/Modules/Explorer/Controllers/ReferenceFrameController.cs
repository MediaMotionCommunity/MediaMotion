using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Reference Frame Controller
	/// </summary>
	public class ReferenceFrameController : AScript<ExplorerModule, ReferenceFrameController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The cursor manager service
		/// </summary>
		private ICursorManagerService cursorManagerService;

		/// <summary>
		/// The explorer
		/// </summary>
		public GameObject explorer;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		/// <param name="cursorManagerService">The cursor manager service.</param>
		public void Init(IInputService inputService, ICursorManagerService cursorManagerService) {
			this.inputService = inputService;
			this.cursorManagerService = cursorManagerService;
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
			if (this.cursorManagerService.IsEnabled) {
				foreach (IAction action in this.inputService.GetMovements(ActionType.BrowsingScroll)) {
					Object3 parameter = (action.Parameter as Object3);

					if ((this.gameObject.transform.localPosition.z + (parameter.Pos.Z / 8)) > -1 && (this.gameObject.transform.localPosition.z + (parameter.Pos.Z / 8)) < ((int)((float)((this.explorer.GetComponent<ExplorerController>().Count / ExplorerController.FilePerLine) + 1) * ExplorerController.FileSpacing) + 1)) {
						this.gameObject.transform.Translate(0, 0, parameter.Pos.Z / 8, UnityEngine.Space.World);
					}
				}
			}
		}
	}
}
