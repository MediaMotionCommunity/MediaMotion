using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using UnityEngine;
namespace MediaMotion.Core.Services.Input.Interfaces {
	/// <summary>
	/// Input Interface
	/// </summary>
	public interface IInputService {
		/// <summary>
		/// Loads the wrapper.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Path">The path.</param>
		void LoadWrapper(string Name = "MediaMotion.*.dll", string Path = null);

		/// <summary>
		/// Adds the default input.
		/// </summary>
		/// <param name="Key">The key.</param>
		/// <param name="Movement">The movement.</param>
		void AddDefaultInput(KeyCode Key, IAction Movement);

		/// <summary>
		/// Gets the movements.
		/// </summary>
		/// <returns>The movements</returns>
		List<IAction> GetMovements();

		/// <summary>
		/// Gets the specific movements.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>The movements</returns>
		List<IAction> GetMovements(ActionType type);

		/// <summary>
		/// Gets the cursor.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>The action</returns>
		IAction GetCursorMovement(int id);
	}
}
