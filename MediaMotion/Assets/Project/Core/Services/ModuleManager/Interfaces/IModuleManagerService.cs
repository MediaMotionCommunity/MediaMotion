using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.ModuleManager.Interfaces {
	/// <summary>
	/// Module Manager Service Interface
	/// </summary>
	public interface IModuleManagerService {
		/// <summary>
		/// Registers the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		void Register<Module>() where Module : IModule, new();

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		/// <returns>
		///   The more recent instance of the module if exist, <c>null</c> otherwise
		/// </returns>
		Module Get<Module>() where Module : class, IModule;

		/// <summary>
		/// Determines whether [has].
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		/// <returns>
		///   <c>true</c> if the Module is registered, <c>false</c> otherwise
		/// </returns>
		bool Has<Module>() where Module : class, IModule;

		/// <summary>
		/// Loads the module with element.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns><c>true</c> if the module is load properly, <c>false</c> otherwise</returns>
		bool Load(IElement[] parameters);

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns><c>true</c> if the module is correctly unloaded, <c>false</c> otherwise</returns>
		bool Unload();
	}
}
