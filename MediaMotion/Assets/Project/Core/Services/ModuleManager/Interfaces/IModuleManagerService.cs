using MediaMotion.Core.Models.Module.Interfaces;
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
		void RegisterModule<Module>() where Module : class, IModule;

		/// <summary>
		/// Loads the module with element.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns><c>true</c> if the module is load properly, <c>false</c> otherwise</returns>
		bool LoadModule(IElement[] parameters);

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns><c>true</c> if the module is correctly unloaded, <c>false</c> otherwise</returns>
		bool UnloadModule();
	}
}
