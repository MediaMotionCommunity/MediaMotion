using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services;
using MediaMotion.Core.Services.ContainerBuilder;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
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
using MediaMotion.Modules.Default;
using MediaMotion.Modules.DefaultViewer;
using MediaMotion.Modules.Explorer;
using MediaMotion.Modules.ImageViewer;
using MediaMotion.Modules.VideoViewer;

namespace MediaMotion.Core {
	/// <summary>
	/// The media motion controller.
	/// </summary>
	public static class MediaMotionCore {
		/// <summary>
		/// The services
		/// </summary>
		public static readonly IContainer Container;

		/// <summary>
		/// Initializes the <see cref="MediaMotionCore"/> class.
		/// </summary>
		static MediaMotionCore() {
			IContainerBuilderService builder = new ContainerBuilderService();

			// Container
			builder.Register<ContainerBuilderService>().As<IContainerBuilderService>();

			// FileSystem
			builder.Register<FileSystemService>().As<IFileSystemService>();
			builder.Register<ElementFactory>().As<IElementFactory>().SingleInstance = true;

			// Playlist
			builder.Register<PlaylistService>().As<IPlaylistService>();

			// History
			builder.Register<HistoryService>().As<IHistoryService>().SingleInstance = true;

			// Input
			builder.Register<InputService>().As<IInputService>().SingleInstance = true;

			// Resources
			builder.Register<ResourceManagerService>().As<IResourceManagerService>().SingleInstance = true;

			// Modules
			builder.Register<ModuleManagerService>().As<IModuleManagerService>().SingleInstance = true;

			Container = builder.Build();

			Register<DefaultModule>();
			Register<DefaultViewerModule>();
			Register<ExplorerModule>();
			Register<ImageViewerModule>();
			Register<VideoViewerModule>();
		}

		/// <summary>
		/// Registers Modules
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		private static void Register<Module>() where Module : IModule, new() {
			Container.Get<ModuleManagerService>().Register<Module>();
		}
	}
}
