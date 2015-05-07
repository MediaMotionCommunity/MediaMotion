using System;
using System.Collections.Generic;
using System.Reflection;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Modules.Default;
using UnityEngine;

namespace MediaMotion.Core.Models.Abstracts {
	/// <summary>
	/// Base unity script
	/// </summary>
	/// <typeparam name="Module">The type of the odule.</typeparam>
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
				MethodInfo method = typeof(Child).GetMethod(MethodName);

				if (method != null && method.IsPublic) {
					method.Invoke((Child)this, resolver.ResolveParameters(method.GetParameters()));
				}
			}
		}
	};

	/// <summary>
	/// Base unity script
	/// </summary>
	/// <typeparam name="Child">Child Class Type (Curiously Recurring Template Pattern)</typeparam>
	public abstract class AScript<Child> : AScript<DefaultModule, Child>
		where Child : AScript<Child> {
	};
}
