using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ExplorerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplorerModule"/> class.
		/// </summary>
		public ExplorerModule()
			: base() {
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		protected override void Configure() {
			this.Configuration.Name = "File browser";
			this.Configuration.Scene = "Explorer";
			this.Configuration.Description = "File browser using the MediaMotion Core API";
		}
	}
}
