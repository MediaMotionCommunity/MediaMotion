using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Models.Module.Interfaces {
	/// <summary>
	/// Module Interface
	/// </summary>
	public interface IModule {
		/// <summary>
		/// Gets the configuration.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		Configuration Configuration { get; }

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
		void Configure();

		/// <summary>
		/// Load the module with specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		void Load(IElement[] parameters);

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