using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Events;
using MediaMotion.Modules.Explorer.Observers;
using MediaMotion.Modules.Explorer.Services.CursorManager;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ExplorerModule : AModule {
		/// <summary>
		/// Configures this instance.
		/// </summary>
		public override void Configure(IContainer container) {
			this.Priority = 0;
			this.Name = "File browser";
			this.Scene = "Explorer";
			this.Description = "File browser using the MediaMotion Core API";
			this.Container = this.BuildContainer(container);
		}

		/// <summary>
		/// Builds the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>
		///   The container
		/// </returns>
		private IContainer BuildContainer(IContainer container) {
			IContainerBuilderService containerBuilderService = container.Get<IContainerBuilderService>();

			containerBuilderService.Register<ElementFactoryObserver>().As<IElementFactoryObserver>().SingleInstance = true;
			containerBuilderService.Register<CursorManagerService>().As<ICursorManagerService>().SingleInstance = true;

			return (containerBuilderService.Build(container));
		}
	}
}
