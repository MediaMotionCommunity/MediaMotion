using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Resolver.Containers.Interfaces;

namespace MediaMotion.Core.Models.Module.Interfaces {
	/// <summary>
	/// Module Interface
	/// </summary>
	public interface IModule {
		/// <summary>
		/// Gets a value indicating whether [keep in background].
		/// </summary>
		/// <value>
		///   <c>true</c> if [keep in background]; otherwise, <c>false</c>.
		/// </value>
		bool KeepInBackground { get; }

		/// <summary>
		/// Registers this module
		/// </summary>
		/// <param name="ModuleConfiguration">The module configuration.</param>
		void Register(IContainer ServiceContainer, out Configuration ModuleConfiguration);

		/// <summary>
		/// Loads the specified files.
		/// </summary>
		/// <param name="Files">The files.</param>
		void Load(string[] Files = null);

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