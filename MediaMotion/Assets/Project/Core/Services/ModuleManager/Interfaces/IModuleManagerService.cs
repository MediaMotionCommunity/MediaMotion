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

		/// <summary>
		/// Get the module which support the specified <see cref="path"/>
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		///   The module which support the <see cref="path"/> or <c>null</c> if any module support it
		/// </returns>
		IModule Supports(string path);

		/// <summary>
		/// Get the module which support the specified <see cref="parameter" />
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>
		/// The module which support the <see cref="parameter" /> or <c>null</c> if any module support it
		/// </returns>
		IModule Supports(IElement parameter);

		/// <summary>
		/// Get the module which support the specified <see cref="parameters" />
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns>
		/// The module which support the <see cref="parameters" /> or <c>null</c> if any module support it
		/// </returns>
		IModule Supports(IElement[] parameters);
	}
}
