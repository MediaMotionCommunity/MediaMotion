using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Resolver.Containers;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.History;
using MediaMotion.Core.Services.History.Interfaces;
using MediaMotion.Core.Services.Input;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.ModuleManager;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Core.Services.Playlist;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Core.Services.ResourcesManager;
using MediaMotion.Core.Services.ResourcesManager.Interfaces;
using MediaMotion.Modules.DefaultViewer;
using MediaMotion.Modules.Explorer;
using MediaMotion.Modules.ImageViewer;
using MediaMotion.Modules.VideoViewer;

namespace MediaMotion.Core {
	/// <summary>
	/// The media motion controller.
	/// </summary>
	public class MediaMotionCore : ICore {
		/// <summary>
		/// The core
		/// </summary>
		private static readonly ICore Instance = new MediaMotionCore();

		/// <summary>
		/// The builder
		/// </summary>
		private readonly IContainerBuilder servicesContainerBuilder;

		/// <summary>
		/// The services
		/// </summary>
		private IContainer servicesContainer;

		/// <summary>
		/// Prevents a default instance of the <see cref="MediaMotionCore"/> class from being created.
		/// </summary>
		private MediaMotionCore() {
			this.servicesContainerBuilder = new ContainerBuilder();

			// Core
			this.servicesContainerBuilder.Register<MediaMotionCore>(this).As<ICore>().SingleInstance();

			// Container
			this.servicesContainerBuilder.Register<ContainerBuilder>().As<IContainerBuilder>();
			
			// FileSystem
			this.servicesContainerBuilder.Register<FileSystemService>().As<IFileSystemService>();
			this.servicesContainerBuilder.Register<ElementFactory>().As<IElementFactory>().SingleInstance();
			this.servicesContainerBuilder.Register<ElementFactory>().SingleInstance();

			// Playlist
			this.servicesContainerBuilder.Register<PlaylistService>().As<IPlaylistService>();

			// History
			this.servicesContainerBuilder.Register<HistoryService>().As<IHistoryService>().SingleInstance();

			// Input
			this.servicesContainerBuilder.Register<InputService>().As<IInputService>().SingleInstance();

			// Resources
			this.servicesContainerBuilder.Register<ResourceManagerService>().As<IResourceManagerService>().SingleInstance();

			// Modules
			this.servicesContainerBuilder.Register<ModuleManagerService>().As<IModuleManagerService>().SingleInstance();

			this.servicesContainer = this.servicesContainerBuilder.Build();
			this.servicesContainer.Get<ModuleManagerService>().RegisterModule<DefaultViewerModule>();
			this.servicesContainer.Get<ModuleManagerService>().RegisterModule<ExplorerModule>();
			this.servicesContainer.Get<ModuleManagerService>().RegisterModule<ImageViewerModule>();
			this.servicesContainer.Get<ModuleManagerService>().RegisterModule<VideoViewerModule>();
		}

		/// <summary>
		/// Gets the core.
		/// </summary>
		/// <value>
		/// The core.
		/// </value>
		public static ICore Core {
			get {
				return (Instance);
			}
		}

		/// <summary>
		/// Adds the services container builder.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The container</returns>
		public IContainer AddServices(IContainerBuilder builder) {
			this.servicesContainer = this.servicesContainerBuilder.Add(builder).Build();
			return (this.servicesContainer);
		}

		/// <summary>
		/// Adds the services.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>The container</returns>
		public IContainer AddServices(IContainer container) {
			this.servicesContainer = this.servicesContainerBuilder.Add(container).Build();
			return (this.servicesContainer);
		}

		/// <summary>
		/// Gets the service container.
		/// </summary>
		/// <returns>The service container</returns>
		public IContainer GetServicesContainer() {
			return (this.servicesContainer);
		}
	}
}
