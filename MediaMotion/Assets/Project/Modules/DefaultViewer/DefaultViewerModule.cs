using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.DefaultViewer {
	/// <summary>
	/// Default viewer module
	/// </summary>
	class DefaultViewerModule : IModule {
		/// <summary>
		/// Gets a value indicating whether [keep in background].
		/// </summary>
		/// <value>
		///   <c>true</c> if [keep in background]; otherwise, <c>false</c>.
		/// </value>
		public bool KeepInBackground { get; private set; }

		/// <summary>
		/// Registers this module
		/// </summary>
		/// <param name="ServiceContainer"></param>
		/// <param name="ModuleConfiguration">The module configuration.</param>
		public void Register(IContainer ServiceContainer, out Configuration ModuleConfiguration) {
			ModuleConfiguration = new Configuration();

			ModuleConfiguration.Name = "Default viewer";
			ModuleConfiguration.Scene = "Default";
			ModuleConfiguration.Description = "Default viewer, use for testing only";
		}

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="Files">The files.</param>
		public void Load(string[] Files = null) {
			Debug.Log(Files);
		}

		/// <summary>
		/// Load another module.
		/// </summary>
		public void Sleep() {
		}

		/// <summary>
		/// Back to the module.
		/// </summary>
		public void WakeUp() {
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		public void Unload() {
		}
	}
}
