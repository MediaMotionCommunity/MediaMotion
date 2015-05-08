using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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
using UnityEngine;

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
			builder.Register<ResourceManagerService>().As<IResourceManagerService>().SingleInstance = true;

			Container = builder.Build();

			Register<DefaultModule>();
			Register<DefaultViewerModule>();
			Register<ExplorerModule>();
			Register<ImageViewerModule>();
			Register<VideoViewerModule>();
		}

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

		/// <summary>
		/// Registers Modules
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		private static void Register<Module>() where Module : IModule, new() {
			Container.Get<ModuleManagerService>().Register<Module>();
		}
	}
}
