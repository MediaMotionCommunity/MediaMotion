using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Cursor controller
	/// </summary>
	public class CursorController : BaseUnityScript<CursorController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the manager.
		/// </summary>
		/// <value>
		/// The manager.
		/// </value>
		public CursorsManagerController Manager { get; set; }

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
			IAction action = null;

			if ((action = this.inputService.GetCursorMovement(this.Id)) != null) {
				Vector3 parameter = action.Parameter as Vector3;

				this.gameObject.transform.localPosition = new UnityEngine.Vector3((float)(parameter.X) / 10, (float)(parameter.Y) / 10, -(float)parameter.Z / 25);
			}
		}

		/// <summary>
		/// Function that is called after collision with a file.
		/// </summary>
		/// <param name="collider">The collider.</param>
		public void OnTriggerEnter(UnityEngine.Collider collider) {
			if (string.Compare("Element", 0, collider.gameObject.name, 0, 7) == 0) {
				this.Manager.Select(this.gameObject, collider.gameObject);
			}
		}

		/// <summary>
		/// Called when [trigger exit].
		/// </summary>
		/// <param name="collider">The collider.</param>
		public void OnTriggerExit(UnityEngine.Collider collider) {
			if (string.Compare("Element", 0, collider.gameObject.name, 0, 7) == 0) {
				this.Manager.Deselect(this.gameObject, collider.gameObject);
			}
		}
	}
}
