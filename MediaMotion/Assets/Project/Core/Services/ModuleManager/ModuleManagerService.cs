using System;
using System.Collections.Generic;
using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Resolver.Exceptions;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.ModuleManager {
	/// <summary>
	/// Module manager service
	/// </summary>
	public class ModuleManagerService : IModuleManagerService {
		/// <summary>
		/// The service lock
		/// </summary>
		private readonly object locker = new object();

		/// <summary>
		/// The core
		/// </summary>
		private readonly ICore core;

		/// <summary>
		/// The modules
		/// </summary>
		private Stack<IModule> modules;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleManagerService"/> class.
		/// </summary>
		/// <param name="core">The core.</param>
		public ModuleManagerService(ICore core) {
			this.core = core;
			this.modules = new Stack<IModule>();
		}

		/// <summary>
		/// Loads the module.
		/// </summary>
		/// <typeparam name="Module">The type of the odule.</typeparam>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		///   <c>true</c> if the module is correctly loaded, <c>false</c> otherwise
		/// </returns>
		public bool LoadModule<Module>(IElement[] parameters) where Module : class, IModule {
			lock (this.locker) {
				IModule module = null;

				try {
					module = this.core.GetServicesContainer().Get<Module>();
				} catch (TypeNotFoundException) {
					module = this.RegisterAndLoad<Module>();
				}
				if (this.modules.Count > 0) {
					this.modules.Peek().Sleep();
				}
				module.Load(parameters);
				Application.LoadLevel(module.Configuration.Scene);
				this.modules.Push(module);
				return (true);
			}
		}

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the module is correctly unloaded, <c>false</c> otherwise
		/// </returns>
		public bool UnloadModule() {
			lock (this.locker) {
				IModule unloadModule = this.modules.Pop();

				if (this.modules.Count > 0) {
					IModule loadModule = this.modules.Peek();

					loadModule.WakeUp();
					Application.LoadLevel(loadModule.Configuration.Scene);
				} else {
					Application.LoadLevel("Loader");
				}
				return (true);
			}
		}

		/// <summary>
		/// Registers the and load.
		/// </summary>
		/// <typeparam name="Module">The type of the odule.</typeparam>
		/// <returns>The module instance</returns>
		private IModule RegisterAndLoad<Module>() where Module : class, IModule {
			IContainerBuilder modulebuilder = this.core.GetServicesContainer().Get<IContainerBuilder>();
			IContainerBuilder serviceBuilder = this.core.GetServicesContainer().Get<IContainerBuilder>();
			IModule module = null;

			modulebuilder.Register<Module>().SingleInstance();
			this.core.AddServices(modulebuilder);
			module = this.core.GetServicesContainer().Get<Module>();
			if (module.Configuration.ServicesContainer != null) {
				this.core.AddServices(module.Configuration.ServicesContainer);
			}
			return (module);
		}
	}
}
