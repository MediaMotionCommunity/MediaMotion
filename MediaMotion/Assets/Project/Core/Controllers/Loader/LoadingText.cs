using System.Collections.Generic;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.Explorer;
using UnityEngine;

namespace MediaMotion.Core.Controllers.Loader {
	/// <summary>
	/// Loading text
	/// </summary>
	public class LoadingText : BaseUnityScript<LoadingText> {
		/// <summary>
		/// The module loaded
		/// </summary>
		private bool moduleLoaded = false;

		/// <summary>
		/// The module manager
		/// </summary>
		private IModuleManagerService moduleManagerService;

		/// <summary>
		/// The cumulative delta time
		/// </summary>
		private float cumulativeDeltaTime = 0.0f;

		/// <summary>
		/// The messages
		/// </summary>
		private List<string> messages;

		/// <summary>
		/// The current
		/// </summary>
		private List<string>.Enumerator currentMessage;

		/// <summary>
		/// Initializes the specified module manager.
		/// </summary>
		/// <param name="moduleManager">The module manager.</param>
		public void Init(IModuleManagerService moduleManager) {
			this.moduleManagerService = moduleManager;
			this.messages = new List<string>();

			this.messages.Add("Loading.");
			this.messages.Add("Loading..");
			this.messages.Add("Loading...");
			this.messages.Add("Loading ...");
			this.messages.Add("Loading  ...");
			this.messages.Add("Loading   ...");
			this.messages.Add("Loading    ..");
			this.messages.Add("Loading     .");

			this.currentMessage = this.messages.GetEnumerator();
			this.currentMessage.MoveNext();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.cumulativeDeltaTime += Time.deltaTime;

			if (this.cumulativeDeltaTime > 0.2f) {
				this.cumulativeDeltaTime = 0;
				this.gameObject.GetComponent<GUIText>().text = this.currentMessage.Current;

				if (!this.currentMessage.MoveNext()) {
					this.currentMessage = this.messages.GetEnumerator();
					this.currentMessage.MoveNext();

					if (!this.moduleLoaded) {
						this.moduleLoaded = this.moduleManagerService.LoadModule<ExplorerModule>(null);
					}
				}
			}
		}
	}
}
