using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Models.Wrapper.Exceptions;

namespace MediaMotion.Core.Models.Wrapper.Interfaces {
	/// <summary>
	/// Action detected delegate
	/// </summary>
	/// <param name="sender">The sender.</param>
	/// <param name="args">The <see cref="ActionDetectedEventArgs"/> instance containing the event data.</param>
	public delegate void ActionDetectedEventHandler(object sender, ActionDetectedEventArgs args);

	/// <summary>
	/// Wrapper Interface
	/// </summary>
	public interface IWrapper {
		/// <summary>
		/// Occurs when an action is detected.
		/// </summary>
		event ActionDetectedEventHandler OnActionDetected;

		/// <summary>
		/// Called on wrapper load
		/// </summary>
		/// <exception cref="WrapperLoadException">
		/// Thrown if any error occurred during loading process
		/// </exception>
		void Load();

		/// <summary>
		/// Called on wrapper unload
		/// </summary>
		/// <exception cref="WrapperUnloadException">
		/// Thrown if any error occurred during unloading process
		/// </exception>
		void Unload();
	}
}