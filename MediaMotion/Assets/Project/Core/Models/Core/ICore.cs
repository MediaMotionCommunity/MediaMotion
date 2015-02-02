using System;
using System.Reflection;
using MediaMotion.Core.Models.Module.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;

namespace MediaMotion.Core.Models.Core {
	/// <summary>
	/// Core Interface
	/// </summary>
	public interface ICore {
		/// <summary>
		/// Gets the services container.
		/// </summary>
		/// <value>
		/// The services container.
		/// </value>
		IContainer ServicesContainer { get; }

		/// <summary>
		/// Loads the module.
		/// </summary>
		/// <typeparam name="Module">The type of the odule.</typeparam>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		bool LoadModule<Module>(string[] parameters) where Module : class, IModule;

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns></returns>
		bool UnloadModule();
	}
}
