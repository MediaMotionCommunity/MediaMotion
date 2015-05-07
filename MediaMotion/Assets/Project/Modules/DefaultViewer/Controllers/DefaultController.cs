using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.DefaultViewer.Controllers {
	/// <summary>
	/// Default controller
	/// </summary>
	public class DefaultController : AScript<DefaultViewerModule, DefaultController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		/// <param name="input">The input.</param>
		public void Init(IInputService input) {
			this.inputService = input;
			this.gameObject.GetComponent<GUIText>().text = ((this.module.Parameters.Length > 0) ? (this.module.Parameters[0].GetName()) : ("No parameter"));
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.inputService.GetMovements();
		}
	}
}
