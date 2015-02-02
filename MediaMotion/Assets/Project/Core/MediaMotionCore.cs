using System;
using System.Collections.Generic;
using System.Reflection;

using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Services.History;
using MediaMotion.Core.Services.History.Interfaces;
using MediaMotion.Core.Services.Input;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.PluginDatabase;
using MediaMotion.Core.Services.PluginDatabase.Interfaces;
using MediaMotion.Modules.Explorer.Controllers;
using MediaMotion.Core.Resolver.Containers;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Modules.Explorer;
using MediaMotion.Core.Models.Module;
using UnityEngine;
using MediaMotion.Modules.DefaultViewer;

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
		/// The service lock
		/// </summary>
		private object moduleLock = new object();

		/// <summary>
		/// The service lock
		/// </summary>
		private object serviceLock = new object();

		/// <summary>
		/// The modules
		/// </summary>
		private Stack<IModule> modules;

		/// <summary>
		/// The background modules
		/// </summary>
		private Stack<IModule> backgroundModules;

		/// <summary>
		/// The modules configuration
		/// </summary>
		private Dictionary<Type, Configuration> modulesConfiguration;

		/// <summary>
		/// The services
		/// </summary>
		private IContainer servicesContainer;

		/// <summary>
		/// The temporary
		/// </summary>
		private FolderContentController Tmp;

		/// <summary>
		/// Prevents a default instance of the <see cref="MediaMotionCore"/> class from being created.
		/// </summary>
		private MediaMotionCore() {
			this.modules = new Stack<IModule>();
			this.backgroundModules = new Stack<IModule>();
			this.modulesConfiguration = new Dictionary<Type, Configuration>();
			this.InitializeServiceContainer();
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
		/// Gets the service container.
		/// </summary>
		/// <value>
		/// The service container.
		/// </value>
		public IContainer ServicesContainer {
			get {
				return (this.servicesContainer);
			}
			private set {
				this.servicesContainer = value;
			}
		}

		/// <summary>
		/// Initializes the service container.
		/// </summary>
		private void InitializeServiceContainer() {
			ContainerBuilder builder = new ContainerBuilder();

			builder.Register<ContainerBuilder>(builder).As<IContainerBuilder>().SingleInstance();
			builder.Register<FileSystemService>().As<IFileSystemService>();
			builder.Register<HistoryService>().As<IHistoryService>().SingleInstance();
			builder.Register<InputService>().As<IInputService>().SingleInstance();
			builder.Register<PluginDatabaseService>().As<IPluginDatabaseService>().SingleInstance();

			builder.Register<ExplorerModule>().SingleInstance();
			builder.Register<DefaultViewerModule>().SingleInstance();

			this.ServicesContainer = builder.Build();
		}

		/// <summary>
		/// Switches to module.
		/// </summary>
		/// <param name="Name">
		/// The name.
		/// </param>
		/// <param name="Root">
		/// The root.
		/// </param>
		/// <param name="Assembly">
		/// The assembly.
		/// </param>
		/// <returns>
		/// <c>true</c> if the module is correctly loaded, <c>false</c> otherwise
		/// </returns>
		public bool LoadModule<Module>(string[] parameters) where Module : class, IModule {
			lock (this.moduleLock) {
				Configuration moduleConfiguration = null;
				Type moduleType = typeof(Module);
				IModule module = this.ServicesContainer.Get<Module>();

				if (!this.modulesConfiguration.TryGetValue(moduleType, out moduleConfiguration)) {
					module.Register(this.ServicesContainer, out moduleConfiguration);
					this.modulesConfiguration.Add(moduleType, moduleConfiguration);
					this.ServicesContainer = this.ServicesContainer.Get<IContainerBuilder>().Build();
				}
				if (this.modules.Count > 0) {
					this.modules.Peek().Sleep();
				}
				module.Load(parameters);
				Application.LoadLevel(moduleConfiguration.Scene);
				this.modules.Push(module);
				return (true);
			}
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns><c>true</c> if the module is correctly unloaded, <c>false</c> otherwise</returns>
		public bool UnloadModule() {
			lock (this.moduleLock) {
				IModule unloadModule = this.modules.Pop();

				if (unloadModule.KeepInBackground) {
					unloadModule.Sleep();
					this.backgroundModules.Push(unloadModule);
				}
				if (this.modules.Count > 0) {
					Configuration moduleConfiguration = null;
					IModule loadModule = this.modules.Peek();
					Type moduleType = loadModule.GetType();

					if (!this.modulesConfiguration.TryGetValue(moduleType, out moduleConfiguration)) {
						throw new Exception("toto");
					}
					loadModule.WakeUp();
					Application.LoadLevel(moduleConfiguration.Scene);
				} else {
					Application.LoadLevel("Loader");
				}
				return (true);
			}
		}
	}
}
