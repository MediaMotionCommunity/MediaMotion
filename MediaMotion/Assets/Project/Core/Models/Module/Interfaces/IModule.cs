using MediaMotion.Core.Models.Wrapper.Events;

namespace MediaMotion.Core.Models.Module.Interfaces {
	/// <summary>
	/// Module Interface
	/// </summary>
	public interface IModule {
		/// <summary>
		/// Gets or sets a value indicating whether [keep in background].
		/// </summary>
		/// <value><c>true</c> if [keep in background]; otherwise, <c>false</c>.</value>
		bool KeepInBackground { get; }

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <param name="ModuleConfiguration">The module configuration.</param>
		void Register(out Configuration ModuleConfiguration);

		/// <summary>
		/// Loads this instance.
		/// </summary>
		void Load();

		/// <summary>
		/// Unloads this instance.
		/// </summary>
		void Unload();
	}
}