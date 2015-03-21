using UnityEngine;

namespace MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces {
	/// <summary>
	/// Cursor event delegate
	/// </summary>
	/// <param name="cursor">The cursor.</param>
	/// <param name="element">The element.</param>
	public delegate void CursorEvent(GameObject cursor, GameObject element);

	/// <summary>
	/// Cursor Manager Service interface
	/// </summary>
	public interface ICursorManagerService {
		/// <summary>
		/// Gets a value indicating whether this instance is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
		/// </value>
		bool IsEnabled { get; }

		/// <summary>
		/// Updates the cursor.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns><c>true</c> if the cursor has been updated, <c>false</c> otherwise</returns>
		bool UpdateCursor(int id);

		/// <summary>
		/// Adds the cursor.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="select">The select.</param>
		/// <param name="unselect">The unselect.</param>
		void AddCursor(GameObject cursorObject, int id, CursorEvent select, CursorEvent unselect);

		/// <summary>
		/// Cleans the cursors.
		/// </summary>
		void CleanCursors(int delay);

		/// <summary>
		/// Gets the main cursor.
		/// </summary>
		/// <returns>The main cursor if it exists, <c>null</c> otherwise</returns>
		GameObject GetMainCursor();

		/// <summary>
		/// Determines whether [is main cursor] [the specified cursor].
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <returns><c>true</c> if cursor is the main cursor, <c>false</c> otherwise</returns>
		bool IsMainCursor(GameObject cursor);

		/// <summary>
		/// Determines whether [is cursor enabled] [the specified cursor].
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <returns><c>true</c> if cursor is enabled, <c>false</c> otherwise</returns>
		bool IsCursorEnabled(GameObject cursor);

		/// <summary>
		/// Enableds the cursors.
		/// </summary>
		void EnabledCursors();

		/// <summary>
		/// Disableds the cursors.
		/// </summary>
		void DisabledCursors();
	}
}
