﻿using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MediaMotion.Core.Services.ModuleManager {
	/// <summary>
	/// Module manager service
	/// </summary>
	public class ModuleManagerService : IDisposable, IModuleManagerService {
		/// <summary>
		/// The service lock
		/// </summary>
		private readonly object locker = new object();

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
		public ModuleManagerService() {
			this.availableModules = new Dictionary<Type, IModule>();
			this.backgroundModules = new Stack<ModuleInstance>();
			this.stackedModules = new Stack<ModuleInstance>();
			this.currentModule = null;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			lock (this.locker) {
				foreach (KeyValuePair<Type, IModule> entry in this.availableModules) {
					if (entry.Value is IDisposable) {
						((IDisposable)entry.Value).Dispose();
					}
				}
				this.availableModules.Clear();
				this.backgroundModules.Clear();
				this.stackedModules.Clear();
				this.currentModule = null;
			}
		}

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		public void Register<Module>() where Module : IModule, new() {
			IModule module = new Module();

			module.Configure(MediaMotionCore.Container);
			this.availableModules.Add(typeof(Module), module);
		}

		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		public void RegisterAdvanced<Module>() where Module : IAdvancedModule, new() {
			IAdvancedModule mainModule = new Module();

			mainModule.Configure(MediaMotionCore.Container);
			foreach (IModule subModule in mainModule.Children) {
				subModule.Configure(mainModule.Container);
				this.availableModules.Add(subModule.GetType(), subModule);
			}
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
				IModule moduleToLoad = this.Supports(parameters);

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
					SceneManager.LoadScene("Loader");
				}
				return (true);
			}
		}

		/// <summary>
		/// Get the module which support the specified <see cref="path"/>
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		///   The module which support the <see cref="path"/> or <c>null</c> if any module support it
		/// </returns>
		public IModule Supports(string path) {
			return (this.availableModules.OrderByDescending(module => module.Value.Priority).FirstOrDefault(module => module.Value.Supports(path)).Value);
		}

		/// <summary>
		/// Get the module which support the specified <see cref="parameter" />
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>
		/// The module which support the <see cref="parameter" /> or <c>null</c> if any module support it
		/// </returns>
		public IModule Supports(IElement parameter) {
			return (this.availableModules.OrderByDescending(module => module.Value.Priority).FirstOrDefault(module => module.Value.Supports(parameter.GetPath())).Value);
		}

		/// <summary>
		/// Get the module which support the specified <see cref="parameters" />
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		/// The module which support the <see cref="parameters" /> or <c>null</c> if any module support it
		/// </returns>
		public IModule Supports(IElement[] parameters) {
			return (this.availableModules.OrderByDescending(module => module.Value.Priority).FirstOrDefault(module => parameters.All(parameter => module.Value.Supports(parameter.GetPath()))).Value);
		}

		/// <summary>
		/// Loads the module scene.
		/// </summary>
		/// <param name="module">The module.</param>
		private void LoadModule(IModule module) {
			SceneManager.LoadScene(module.Scene);
			this.currentModule = module;
		}
	}
}
