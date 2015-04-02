using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Exceptions;

namespace MediaMotion.Motion
{
	/// <summary>
	/// The WrapperDevice interface.
	/// </summary>
	public interface IWrapperDevice : IDisposable {
		#region Properties
		/// <summary>
		/// Gets the name of device.
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Gets the type of device
		/// </summary>
		string Type { get; }

		/// <summary>
		/// Gets the link of device (http)
		/// </summary>
		string Link { get; }

		/// <summary>
		/// Gets the author.
		/// </summary>
		string Author { get;  }
		#endregion

		#region Methods
		#region Initialization
		/// <summary>
		/// Called on wrapper load.
		/// </summary>
		/// <exception cref="WrapperLoadException">
		/// Thrown if any error occured during loading process
		/// </exception>
		void Load();

		/// <summary>
		/// Called on wrapper unload.
		/// </summary>
		/// <exception cref="WrapperUnloadException">
		/// Thrown if any error occured during unloading process
		/// </exception>
		void Unload();
		#endregion
		/// <summary>
		/// The get actions.
		/// </summary>
		/// <returns>
		/// The list of action detected by the library.
		/// </returns>
		IEnumerable<IAction> GetActions();

		/// <summary>
		/// Enable only one action.
		/// Only enabled action will be return by <see cref="GetActions"/>
		/// </summary>
		/// <param name="action">action to enable</param>
		void EnableAction(ActionType action);

		/// <summary>
		/// Enable list of action
		/// Only enabled actions will be return by <see cref="GetActions"/>
		/// </summary>
		/// <param name="actions">action's list to enable</param>
		void EnableActions(IEnumerable<ActionType> actions);

		#endregion
	}
}