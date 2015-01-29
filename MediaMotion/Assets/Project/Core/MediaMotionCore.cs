using System;
using System.Collections.Generic;

using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Models.Service;
using MediaMotion.Core.Services.FileSystem;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.History;
using MediaMotion.Core.Services.History.Interfaces;
using MediaMotion.Core.Services.Input;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Core.Services.PluginDatabase;
using MediaMotion.Core.Services.PluginDatabase.Interfaces;
using MediaMotion.Modules.Explorer.Controllers;
using MediaMotion.Resolver;

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
		/// The builder.
		/// </summary>
		private readonly ContainerBuilder builder;

		/// <summary>
		/// The service lock
		/// </summary>
		private object ModuleLock = new object();

		/// <summary>
		/// The service lock
		/// </summary>
		private object ServiceLock = new object();

		/// <summary>
		/// The module
		/// </summary>
		private Dictionary<string, IModule> Modules;

		/// <summary>
		/// The services
		/// </summary>
		private Dictionary<string, ServiceBase> Services;

		/// <summary>
		/// The temporary
		/// </summary>
		private FolderContentController Tmp;

		/// <summary>
		/// Prevents a default instance of the <see cref="MediaMotionCore"/> class from being created.
		/// </summary>
		private MediaMotionCore() {
			this.Modules = new Dictionary<string, IModule>();
			this.Services = new Dictionary<string, ServiceBase>();
			this.builder = new ContainerBuilder();
			this.InitialiseInjection();
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
		/// The initialise.
		/// </summary>
		private void InitialiseInjection() {
			this.builder.RegisterType<FileSystemService>().As<IFileSystemService>();
			this.builder.RegisterType<HistoryService>().As<IHistoryService>();
			this.builder.RegisterType<InputService>().As<IInputService>();
			this.builder.RegisterType<PluginDatabaseService>().As<IPluginDatabaseService>();
			this.builder.RegisterType<FolderContentController>();
			this.builder.Build();
		}

		/// <summary>
		/// The get service.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public T Resolve<T>() where T : class {
			lock (this.ServiceLock) {
				return this.builder.Resolve<T>();
			}
		}

		/// <summary>
		/// The get service.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object Resolve(Type type) {
			lock (this.ServiceLock) {
				return this.builder.Resolve(type);
			}
		}

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <param name="Name">
		/// The name.
		/// </param>
		/// <param name="Root">
		/// The root.
		/// </param>
		/// <returns>
		/// The module name
		/// </returns>
		public string GetModuleName(string Name, string Root = "MediaMotion.Modules") {
			return ((((Root ?? string.Empty) == string.Empty) ? (string.Empty) : (Root + ".")) + Name + "." + Name + "Module");
		}

		/// <summary>
		/// Gets the full name of the module.
		/// </summary>
		/// <param name="ModuleName">
		/// Name of the module.
		/// </param>
		/// <param name="Assembly">
		/// The assembly.
		/// </param>
		/// <returns>
		/// The full module name
		/// </returns>
		public string GetFullModuleName(string ModuleName, string Assembly = null) {
			return (Assembly ?? string.Empty + ";" + ModuleName);
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
		public bool LoadModule(string Name = "Explorer", string Root = "MediaMotion.Modules", string Assembly = null) {
			IModule Module = null;
			string ModuleName = this.GetModuleName(Name, Root);

			if (this.Modules.TryGetValue(this.GetFullModuleName(ModuleName, Assembly), out Module)) {
			}
			return (true);
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns><c>true</c> if the module is correctly unloaded, <c>false</c> otherwise</returns>
		public bool UnloadModule() {
			return (true);
		}

		/// <summary>
		/// Register a module
		/// </summary>
		/// <param name="ModuleName">
		/// Name of the module.
		/// </param>
		/// <param name="Assembly">
		/// The assembly.
		/// </param>
		/// <returns>
		/// <c>true</c> if the module is correctly loaded, <c>false</c> otherwise
		/// </returns>
		private IModule RegisterModule(string ModuleName, string Assembly) {
			IModule Module = null;
			string FullModuleName = this.GetFullModuleName(ModuleName, Assembly);

			try {
				Module = (IModule)Activator.CreateInstance(Assembly, ModuleName).Unwrap();
				if (Module == null) {
					return (null);
				}
			} catch (InvalidCastException) {
				return (null);
			}
			this.Modules.Add(FullModuleName, Module);
			return (Module);
		}

		/// <summary>
		/// Unregister the module.
		/// </summary>
		/// <param name="FullModuleName">
		/// Full name of the module.
		/// </param>
		/// <param name="ForceUnload">
		/// if set to <c>true</c> [force unload].
		/// </param>
		/// <returns>
		/// <c>true</c> if the module is correctly unloaded, <c>false</c> otherwise
		/// </returns>
		private bool UnregisterModule(string FullModuleName, bool ForceUnload = false) {
			IModule Module = null;

			if (this.Modules.TryGetValue(FullModuleName, out Module)) {
				if (Module.KeepInBackground || ForceUnload) {
					Module.Unload();
					this.Modules.Remove(FullModuleName);
					return (true);
				}
			}
			return (false);
		}
	}
}
