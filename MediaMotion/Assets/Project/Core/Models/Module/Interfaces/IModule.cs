using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;

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
		/// Load the module with specified parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		void Load(IElement[] parameters = null);

		/// <summary>
		/// Load another module.
		/// </summary>
		void Sleep();

		/// <summary>
		/// Back to the module.
		/// </summary>
		void WakeUp();

		/// <summary>
		/// Unloads the module.
		/// </summary>
		void Unload();
	}
}