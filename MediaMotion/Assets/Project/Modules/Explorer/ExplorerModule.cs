﻿using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ExplorerModule : IModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplorerModule"/> class.
		/// </summary>
		public ExplorerModule() {
			this.KeepInBackground = true;
		}

		/// <summary>
		/// Gets a value indicating whether [keep in background].
		/// </summary>
		/// <value><c>true</c> if [keep in background]; otherwise, <c>false</c>.</value>
		public bool KeepInBackground { get; private set; }

		/// <summary>
		/// Registers the specified module configuration.
		/// </summary>
		/// <param name="ModuleConfiguration">The module configuration.</param>
		public void Register(IContainer ServiceContainer, out Configuration ModuleConfiguration) {
			ModuleConfiguration = new Configuration();

			ModuleConfiguration.Name = "File browser";
			ModuleConfiguration.Scene = "Explorer";
			ModuleConfiguration.Description = "File browser using the MediaMotion Core API";

			this.RegisterServices(ServiceContainer);
		}

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="Files">The files.</param>
		public void Load(string[] Files = null) {
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
		/// Called when the module is unloaded
		/// </summary>
		public void Unload() {
		}

		/// <summary>
		/// Registers the services.
		/// </summary>
		private void RegisterServices(IContainer ServiceContainer) {
			IContainerBuilder builder = ServiceContainer.Get<IContainerBuilder>();
		}
	}
}
