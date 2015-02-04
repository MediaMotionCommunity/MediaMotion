using MediaMotion.Core.Models.Scripts;

namespace MediaMotion.Modules.DefaultViewer.Controllers {
	/// <summary>
	/// Default controller
	/// </summary>
	public class DefaultController : BaseUnityScript<DefaultController> {
		/// <summary>
		/// Initializes the specified module.
		/// </summary>
		/// <param name="module">The module.</param>
		public void Init(DefaultViewerModule module) {
			this.gameObject.guiText.text = ((module.Parameters.Length > 0) ? (module.Parameters[0].GetName()) : ("No parameter"));
		}
	}
}
