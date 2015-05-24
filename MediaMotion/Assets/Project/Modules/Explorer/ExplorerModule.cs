using System.IO;
using MediaMotion.Core.Events;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using MediaMotion.Modules.Explorer.Observers;
using MediaMotion.Modules.Explorer.Services.CursorManager;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Modules.Explorer {
	/// <summary>
	/// Explorer module
	/// </summary>
	public sealed class ExplorerModule : AModule {
		/// <summary>
		/// Initializes a new instance of the <see cref="ExplorerModule"/> class.
		/// </summary>
		public ExplorerModule() {
			this.Priority = 0;
			this.Name = "File browser";
			this.Scene = "Explorer";
			this.Description = "File browser using the MediaMotion Core API";
			this.SupportedAction = new ActionType[] {
				ActionType.BrowsingCursor,
				ActionType.BrowsingHighlight,
				ActionType.BrowsingScroll,
				ActionType.Back,
				ActionType.Select,
				ActionType.GrabStart,
				ActionType.GrabStop
			};
		}

		/// <summary>
		/// Configures this instance.
		/// </summary>
		/// <param name="container">The container</param>
		public override void Configure(IContainer container) {
			this.Container = this.BuildContainer(container);
		}

		/// <summary>
		/// Support the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		///   <c>true</c> if the element is supported, <c>false</c> otherwise
		/// </returns>
		public override bool Supports(string path) {
			return (Directory.Exists(path));
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

			containerBuilderService.Register<ElementDrawObserver>().As<IElementDrawObserver>().SingleInstance = true;
			containerBuilderService.Register<CursorManagerService>().As<ICursorManagerService>().SingleInstance = true;
			return (containerBuilderService.Build(container));
		}
	}
}
