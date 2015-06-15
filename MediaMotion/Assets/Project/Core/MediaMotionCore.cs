using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using MediaMotion.Core.Loader;
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
using MediaMotion.Core.Services.Observers;
using MediaMotion.Core.Services.Observers.Interfaces;
using MediaMotion.Core.Services.Playlist;
using MediaMotion.Core.Services.Playlist.Interfaces;
using MediaMotion.Modules.Explorer;
using MediaMotion.Modules.ImageViewer;
using MediaMotion.Modules.PDFViewer;
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
		/// Initializes static members of the <see cref="MediaMotionCore"/> class.
		/// </summary>
		static MediaMotionCore() {
			Assembly assembly = typeof(MediaMotionCore).Assembly;
			IContainerBuilderService builder = new ContainerBuilderService();

			// Parameters
			builder.Define("Version", MediaMotionCore.GetVersion(assembly));
			builder.Define("BuildMode", MediaMotionCore.GetBuildMode(assembly));
			builder.Define("BuildDate", MediaMotionCore.GetBuildDate(assembly));
			builder.Define("DebuggerAttached", Debugger.IsAttached);

			// Services
			builder.Register<ContainerBuilderService>().As<IContainerBuilderService>();
			builder.Register<ElementFactory>().As<IElementFactory>().SingleInstance = true;
			builder.Register<FileSystemService>().As<IFileSystemService>();
			builder.Register<HistoryService>().As<IHistoryService>().SingleInstance = true;
			builder.Register<InputService>().As<IInputService>().SingleInstance = true;
			builder.Register<ModuleManagerService>().As<IModuleManagerService>().SingleInstance = true;
			builder.Register<PlaylistService>().As<IPlaylistService>();

			// Observers
			builder.Register<ElementDrawObserver>().As<IElementDrawObserver>().SingleInstance = true;

			Container = builder.Build();

			MediaMotionCore.RegisterModules();
		}

		/// <summary>
		/// Registers Modules
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		private static void RegisterModules() {
			IModuleManagerService moduleManager = Container.Get<IModuleManagerService>();

			moduleManager.Register<ModuleLoader>();
			moduleManager.Register<ExplorerModule>();
			moduleManager.Register<ImageViewerModule>();
			moduleManager.Register<VideoViewerModule>();
			moduleManager.Register<PDFViewerModule>();
		}

		/// <summary>
		/// Gets the version.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>
		///   The version
		/// </returns>
		private static Version GetVersion(Assembly assembly) {
			return (assembly.GetName().Version);
		}

		/// <summary>
		/// Gets the build mode.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>
		///   The build mode
		/// </returns>
		private static string GetBuildMode(Assembly assembly) {
			AssemblyNameFlags assemblyNameFlags = assembly.GetName().Flags;

			if ((assemblyNameFlags & AssemblyNameFlags.EnableJITcompileOptimizer) == 0 && (assemblyNameFlags & AssemblyNameFlags.EnableJITcompileTracking) == AssemblyNameFlags.EnableJITcompileTracking) {
				return ("Debug");
			}
			return ("Release");
		}

		/// <summary>
		/// Gets the build date.
		/// </summary>
		/// <param name="assembly">The assembly.</param>
		/// <returns>
		///   The build date
		/// </returns>
		private static DateTime GetBuildDate(Assembly assembly) {
			return (File.GetLastWriteTime(assembly.Location));
		}
	}
}
