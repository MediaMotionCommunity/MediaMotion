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

				this.gameObject.transform.localPosition = new UnityEngine.Vector3((float)(parameter.X) / 10, (float)(parameter.Y) / 10, -(float)parameter.Z / 10);
			}
		}
	}
}
