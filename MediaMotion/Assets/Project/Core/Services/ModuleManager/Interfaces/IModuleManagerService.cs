using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Interfaces;

namespace MediaMotion.Core.Services.ModuleManager.Interfaces {
	/// <summary>
	/// Module Manager Service Interface
	/// </summary>
	public interface IModuleManagerService {
		/// <summary>
		/// Loads the module.
		/// </summary>
		/// <typeparam name="Module">The type of the module.</typeparam>
		/// <param name="parameters">The parameters.</param>
		/// <returns><c>true</c> if the module is correctly loaded, <c>false</c> otherwise</returns>
		bool LoadModule<Module>(IElement[] parameters) where Module : class, IModule;

		/// <summary>
		/// Unloads the module.
		/// </summary>
		/// <returns><c>true</c> if the module is correctly unloaded, <c>false</c> otherwise</returns>
		bool UnloadModule();
	}
}
