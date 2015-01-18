using MediaMotion.Core.Models.Wrapper.Events;

namespace MediaMotion.Core.Models.Module.Interfaces {
	/// <summary>
	/// Module Interface
	/// </summary>
	public interface IModule {
		/// <summary>
		/// Actions the handle.
		/// </summary>
		/// <param name="Sender">The sender.</param>
		/// <param name="Action">The <see cref="ActionDetectedEventArgs"/> instance containing the event data.</param>
		void ActionHandle(object Sender, ActionDetectedEventArgs Action);

		/// <summary>
		/// Registers this instance.
		/// </summary>
		void Register();

		/// <summary>
		/// Unregisters this instance.
		/// </summary>
		void Unregister();

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