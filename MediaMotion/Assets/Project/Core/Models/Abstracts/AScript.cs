using System;
using System.Collections.Generic;
using System.Reflection;
using MediaMotion.Core.Loader;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Base unity script
	/// </summary>
	/// <typeparam name="Module">The type of the module.</typeparam>
	/// <typeparam name="Child">Child Class Type (Curiously Recurring Template Pattern)</typeparam>
	public abstract class AScript<Module, Child> : MonoBehaviour
		where Module : class, IModule
		where Child : AScript<Module, Child> {
		/// <summary>
		/// The method name
		/// </summary>
		public const string MethodName = "Init";

		/// <summary>
		/// The module
		/// </summary>
		protected Module module;

		/// <summary>
		/// Initialize the child instance
		/// </summary>
		public void Start() {
			IModuleManagerService moduleManager = MediaMotionCore.Container.Get<IModuleManagerService>();

			this.module = moduleManager.Get<Module>();
			if (this.module != null) {
				IResolverService resolver = this.module.Container.Get<IResolverService>();
				foreach (MethodInfo method in typeof(Child).GetMethods()) {
					if (method != null && method.IsPublic && !method.IsAbstract && method.Name.Equals(MethodName)) {
						method.Invoke((Child)this, resolver.ResolveParameters(method.GetParameters()));
					}
				}
			}
		}
	}

	/// <summary>
	/// Base unity script
	/// </summary>
	/// <typeparam name="Child">Child Class Type (Curiously Recurring Template Pattern)</typeparam>
	public abstract class AScript<Child> : AScript<ModuleLoader, Child>
		where Child : AScript<Child> {
	}
}
