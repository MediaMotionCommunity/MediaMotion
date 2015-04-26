using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;

namespace MediaMotion.Core.Models.Interfaces {
	/// <summary>
	/// Module Configuration Interface
	/// </summary>
	public interface IModuleConfiguration {
		/// <summary>
		/// Gets or sets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		int Priority { get; set; }

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		string Name { get; set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		string Scene { get; set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets the element factory observer.
		/// </summary>
		/// <value>
		/// The element factory observer.
		/// </value>
		IElementFactoryObserver ElementFactoryObserver { get; set; }

		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		IContainerBuilder ServicesContainer { get; set; }
	}
}
