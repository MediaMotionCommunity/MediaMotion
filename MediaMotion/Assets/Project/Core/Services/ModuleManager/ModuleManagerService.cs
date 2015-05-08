﻿using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Interfaces;
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
		/// The element factory
		/// </summary>
		private readonly IElementFactory elementFactory;

		/// <summary>
		/// The modules
		/// </summary>
		private readonly Dictionary<Type, IModule> availableModules;

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
		/// <param name="elementFactory">The element factory.</param>
		public ModuleManagerService(IElementFactory elementFactory) {
			this.elementFactory = elementFactory;
			this.availableModules = new Dictionary<Type, IModule>();
			this.backgroundModules = new Stack<ModuleInstance>();
			this.stackedModules = new Stack<ModuleInstance>();
			this.currentModule = null;
		}

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		public void Register<Module>() where Module : IModule, new() {
			IModule module = new Module();

			module.Configure(MediaMotionCore.Container);
			if (module.Container.Has<IElementFactoryObserver>()) {
				this.elementFactory.AddObserver(module.Container.Get<IElementFactoryObserver>(), module.Priority);
			}
			this.availableModules.Add(typeof(Module), module);
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		/// <returns>
		/// The more recent instance of the module if exist, <c>null</c> otherwise
		/// </returns>
		public Module Get<Module>() where Module : class, IModule {
			IModule module;

			if (!this.availableModules.TryGetValue(typeof(Module), out module)) {
				throw new Exception("Error");
			}
			return ((Module)module);
		}

		/// <summary>
		/// Determines whether [has].
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		/// <returns>
		///   <c>true</c> if the Module is registered, <c>false</c> otherwise
		/// </returns>
		public bool Has<Module>() where Module : class, IModule {
			return (this.availableModules.ContainsKey(typeof(Module)));
		}

		/// <summary>
		/// Loads the module with element.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns><c>true</c> if the module is load properly, <c>false</c> otherwise</returns>
		public bool Load(IElement[] parameters) {
			lock (this.locker) {
				IModule moduleToLoad = this.availableModules.OrderByDescending(module => module.Value.Priority).FirstOrDefault(module => parameters.All(parameter => module.Value.Supports(parameter))).Value;

				if (moduleToLoad != null) {
					if (this.currentModule == moduleToLoad && this.currentModule.SupportReload) {
						this.currentModule.Reload(parameters);
						return (true);
					}
					if (this.currentModule != null && this.currentModule != moduleToLoad) {
						this.stackedModules.Push(new ModuleInstance(this.currentModule, this.currentModule.Sleep()));
					}
					moduleToLoad.Load(parameters);
					this.LoadModule(moduleToLoad);
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
		public bool Unload() {
			lock (this.locker) {
				if (this.stackedModules.Count > 0) {
					ModuleInstance moduleInstance = this.stackedModules.Pop();

					this.currentModule.Unload();
					moduleInstance.Module.WakeUp(moduleInstance.Parameters);
					this.LoadModule(moduleInstance.Module);
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
		private void LoadModule(IModule module) {
			Application.LoadLevel(module.Scene);
			this.currentModule = module;
		}
	}
}
