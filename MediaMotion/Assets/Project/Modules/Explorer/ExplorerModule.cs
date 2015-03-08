using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Modules.Explorer.Services.CursorManager;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ExplorerModule : AModule {
		/// <summary>
		/// The builder
		/// </summary>
		private IContainerBuilder builder;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExplorerModule"/> class.
		/// </summary>
		public ExplorerModule(IContainerBuilder builder)
			: base() {
			UnityEngine.Debug.Log(builder);
			this.builder = builder;
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure() {
			this.Configuration.Name = "File browser";
			this.Configuration.Scene = "Explorer";
			this.Configuration.Description = "File browser using the MediaMotion Core API";
			this.Configuration.ServicesContainer = this.builder;

			this.builder.Register<CursorManagerService>().As<ICursorManagerService>().SingleInstance();
		}
	}
}
