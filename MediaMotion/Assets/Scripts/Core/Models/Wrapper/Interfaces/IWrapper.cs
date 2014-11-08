using MediaMotion.Core.Models.Wrapper.Events;
using MediaMotion.Core.Models.Wrapper.Exceptions;

namespace MediaMotion.Core.Models.Wrapper.Interfaces {
	public delegate void ActionDetectedEventHandler(object sender, ActionDetectedEventArgs args);
	
	public interface IWrapper {
		event ActionDetectedEventHandler OnActionDetected;

		/// <summary>
		/// Called on wrapper load
		/// </summary>
		/// <exception cref="WrapperLoadException">
		/// Thrown if any error occured during loading process
		/// </exception>
		void Load();

		/// <summary>
		/// Called on wrapper unload
		/// </summary>
		/// <exception cref="WrapperUnloadException">
		/// Thrown if any error occured during unloading process
		/// </exception>
		void Unload();
	}
}