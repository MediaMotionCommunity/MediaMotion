using MediaMotion.Core.Events;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Models.Interfaces {
	/// <summary>
	/// Module Interface
	/// </summary>
	public interface IModule {
		/// <summary>
		/// Gets the priority.
		/// </summary>
		/// <value>
		/// The priority.
		/// </value>
		int Priority { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		string Name { get; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		string Scene { get; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		string Description { get; }

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		/// The container.
		/// </value>
		IContainer Container { get; }

		/// <summary>
		/// Gets a value indicating whether [support reload].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support reload]; otherwise, <c>false</c>.
		/// </value>
		bool SupportReload { get; }

		/// <summary>
		/// Gets a value indicating whether [support background].
		/// </summary>
		/// <value>
		///   <c>true</c> if [support background]; otherwise, <c>false</c>.
		/// </value>
		bool SupportBackground { get; }

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		IElement[] Parameters { get; }

		/// <summary>
		/// Configures the module.
		/// </summary>
		/// <param name="container">The container.</param>
		void Configure(IContainer container);

		/// <summary>
		/// Load the module with specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		void Load(IElement[] parameters);

		/// <summary>
		/// Reloads the specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		void Reload(IElement[] parameters);

		/// <summary>
		/// Load another module.
		/// </summary>
		/// <returns>The parameters to restore</returns>
		IElement[] Sleep();

		/// <summary>
		/// Back to the module.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		void WakeUp(IElement[] parameters);

		/// <summary>
		/// Unloads the module.
		/// </summary>
		void Unload();

		/// <summary>
		/// Supports the specified element.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns><c>true</c> if the element is supported, <c>false</c> otherwise</returns>
		bool Supports(IElement element);
	}
}