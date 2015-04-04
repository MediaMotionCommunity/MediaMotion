using System;
using System.Collections.Generic;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;

namespace MediaMotion.Core.Models.Module {
	/// <summary>
	/// Module configuration
	/// </summary>
	public sealed class Configuration {
		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		public int Priority { get; set; }

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		public string Scene { get; set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the element factory observer.
		/// </summary>
		/// <value>
		/// The element factory observer.
		/// </value>
		public IElementFactoryObserver ElementFactoryObserver { get; set; }

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		public IContainerBuilder ServicesContainer { get; set; }
	}
}
