using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public class ExplorerModule : IModule {
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
		public void Register(out Configuration ModuleConfiguration) {
			ModuleConfiguration = new Configuration();

			ModuleConfiguration.Name = "File browser";
			ModuleConfiguration.Scene = "Explorer";
			ModuleConfiguration.Description = "File browser using the MediaMotion Core API";

			// ModuleConfiguration.Movements.Add(ActionType.Cursor, null);
			// ModuleConfiguration.BackgroundMovements.Add(ActionType.Cursor, null);
		}

		/// <summary>
		/// Called when the module is loaded
		/// </summary>
		public void Load() {
		}

		/// <summary>
		/// Called when the module is unloaded
		/// </summary>
		public void Unload() {
		}
	}
}
