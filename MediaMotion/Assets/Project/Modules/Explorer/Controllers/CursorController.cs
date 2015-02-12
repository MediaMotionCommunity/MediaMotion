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
        /// Reference to the ExplorerController containing the FileSystem.
        /// </summary>
        public ExplorerController Controller = null;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
            this.Controller = UnityEngine.GameObject.Find("Explorer").GetComponent<ExplorerController>();
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

        /// <summary>
        /// Function that is called after collision with a file.
        /// </summary>
        /// <param name="collision"></param>
        public void OnTriggerEnter(UnityEngine.Collider other) {
            UnityEngine.Debug.Log("Hey it's me," + other.GetComponentInParent<ElementController>().Element.GetName() + "!");
        }
	}
}
