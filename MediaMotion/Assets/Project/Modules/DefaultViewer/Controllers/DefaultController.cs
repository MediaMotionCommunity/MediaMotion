using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;

namespace MediaMotion.Modules.DefaultViewer.Controllers {
	/// <summary>
	/// Default controller
	/// </summary>
	public class DefaultController : BaseUnityScript<DefaultController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		public void Init(DefaultViewerModule module, IInputService inputService) {
			this.inputService = inputService;
			this.gameObject.guiText.text = ((module.Parameters.Length > 0) ? (module.Parameters[0].GetName()) : ("No parameter"));
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.inputService.GetMovements();
		}
	}
}
