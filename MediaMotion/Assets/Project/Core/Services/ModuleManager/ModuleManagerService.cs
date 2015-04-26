using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Models;
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
		/// The element factory
		/// </summary>
		private readonly IElementFactory elementFactory;

		/// <summary>
		/// The modules
		/// </summary>
		private readonly List<IModule> availableModules;

		/// <summary>
		/// The background modules
		/// </summary>
		private readonly Stack<ModuleInstance> backgroundModules;

		/// <summary>
		/// The loaded modules
		/// </summary>
		private readonly Stack<ModuleInstance> stackedModules;

		/// <summary>
		/// The current
		/// </summary>
		private IModule currentModule;

		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleManagerService" /> class.
		/// </summary>
		/// <param name="core">The core.</param>
		/// <param name="elementFactory">The element factory.</param>
		public ModuleManagerService(ICore core, IElementFactory elementFactory) {
			this.core = core;
			this.elementFactory = elementFactory;
			this.availableModules = new List<IModule>();
			this.backgroundModules = new Stack<ModuleInstance>();
			this.stackedModules = new Stack<ModuleInstance>();
			this.currentModule = null;
		}

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		public void RegisterModule<Module>() where Module : class, IModule {
			IContainerBuilder modulebuilder = this.core.GetServicesContainer().Get<IContainerBuilder>();
			IModule module = null;

			modulebuilder.Register<Module>().SingleInstance();
			this.core.AddServices(modulebuilder);
			module = this.core.GetServicesContainer().Get<Module>();
			module.Configure();
			if (module.Configuration.ElementFactoryObserver != null) {
				this.elementFactory.AddObserver(module.Configuration.ElementFactoryObserver, module.Configuration.Priority);
			}
			if (module.Configuration.ServicesContainer != null) {
				this.core.AddServices(module.Configuration.ServicesContainer);
			}
			this.availableModules.Add(module);
		}

		/// <summary>
		/// Loads the module with element.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns><c>true</c> if the module is load properly, <c>false</c> otherwise</returns>
		public bool LoadModule(IElement[] parameters) {
			lock (this.locker) {
				IModule moduleToLoad = this.availableModules.OrderByDescending(module => module.Configuration.Priority).FirstOrDefault(module => parameters.All(parameter => module.Supports(parameter)));

				if (moduleToLoad != null) {
					if (this.currentModule != null && this.currentModule != moduleToLoad) {
						Debug.Log("Stacked module");
						this.stackedModules.Push(new ModuleInstance(this.currentModule, this.currentModule.Sleep()));
					}
					moduleToLoad.Load(parameters);
					this.LoadModuleScene(moduleToLoad);
					return (true);
				}
				return (false);
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
				if (this.stackedModules.Count > 0) {
					ModuleInstance moduleInstance = this.stackedModules.Pop();

					this.currentModule.Unload();
					moduleInstance.Module.WakeUp(moduleInstance.Parameters);
					this.LoadModuleScene(moduleInstance.Module);
				} else {
					Application.LoadLevel("Loader");
				}
				return (true);
			}
		}

		/// <summary>
		/// Loads the module scene.
		/// </summary>
		/// <param name="module">The module.</param>
		private void LoadModuleScene(IModule module) {
			Application.LoadLevel(module.Configuration.Scene);
			this.currentModule = module;
		}
	}
}
