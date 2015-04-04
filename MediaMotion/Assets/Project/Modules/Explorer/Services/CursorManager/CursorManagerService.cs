using System.Collections.Generic;
using System.Linq;
using MediaMotion.Modules.Explorer.Controllers;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Services.CursorManager {
	/// <summary>
	/// Cursor Manager
	/// </summary>
	public class CursorManagerService : ICursorManagerService {
		/// <summary>
		/// The cursors
		/// </summary>
		private Dictionary<int, CursorData> cursors;

		/// <summary>
		/// Initializes a new instance of the <see cref="CursorManagerService"/> class.
		/// </summary>
		public CursorManagerService() {
			this.IsEnabled = true;
			this.cursors = new Dictionary<int, CursorData>();
		}

		/// <summary>
		/// Gets a value indicating whether this instance is enabled.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnabled { get; private set; }

		/// <summary>
		/// Updates the cursor.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns><c>true</c> if the cursor has been updated, <c>false</c> otherwise</returns>
		public bool UpdateCursor(int id) {
			bool exist;

			if (exist = this.cursors.ContainsKey(id)) {
				if (this.cursors[id].GameObject == null) {
					this.cursors.Remove(id);
					return (false);
				}
				this.cursors[id].Update();
			}
			return (exist);
		}

		/// <summary>
		/// Adds the cursor.
		/// </summary>
		/// <param name="cursorObject">The cursor object.</param>
		/// <param name="id">The identifier.</param>
		/// <param name="selectCallback">The select.</param>
		/// <param name="unselectCallback">The unselect.</param>
		public void AddCursor(GameObject cursorObject, int id, CursorEvent selectCallback, CursorEvent unselectCallback) {
			CursorData cursor = new CursorData(cursorObject);
			CursorController cursorController;

			// script
			cursorController = cursor.GameObject.AddComponent<CursorController>();
			cursorController.Id = id;
			cursorController.SelectCallback = selectCallback;
			cursorController.UnselectCallback = unselectCallback;

			this.SetTag(cursor, this.cursors.Count == 0);
			this.cursors.Add(id, cursor);
		}

		/// <summary>
		/// Cleans the cursors.
		/// </summary>
		/// <param name="delay">The delay</param>
		public void CleanCursors(int delay = -1) {
			List<int> toRemove = new List<int>();

			foreach (KeyValuePair<int, CursorData> cursor in this.cursors.Where(cursorData => (cursorData.Value.LastFrameUpdate + delay) < Time.frameCount)) {
				GameObject.Destroy(cursor.Value.GameObject);
				toRemove.Add(cursor.Key);
			}
			if (toRemove.Count > 0) {
				foreach (int id in toRemove) {
					this.cursors.Remove(id);
				}

				// if no more main cursor
				if (!this.cursors.Any(cursorData => this.IsMainCursor(cursorData.Value.GameObject)) && this.cursors.Count > 0) {
					this.SetTag(this.cursors.First(cursorData => true).Value, true);
				}
			}
		}

		/// <summary>
		/// Gets the main cursor.
		/// </summary>
		/// <returns>The main cursor if it exists, <c>null</c> otherwise</returns>
		public GameObject GetMainCursor() {
			CursorData data = this.cursors.FirstOrDefault(cursorData => this.IsMainCursor(cursorData.Value.GameObject)).Value;

			return ((data != null) ? (data.GameObject) : (null));
		}

		/// <summary>
		/// Determines whether [is main cursor] [the specified cursor].
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <returns>
		///   <c>true</c> if cursor is the main cursor, <c>false</c> otherwise
		/// </returns>
		public bool IsMainCursor(GameObject cursor) {
			return (cursor.tag.StartsWith("Main"));
		}

		/// <summary>
		/// Determines whether [is cursor enabled] [the specified cursor].
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <returns>
		///   <c>true</c> if cursor is enabled, <c>false</c> otherwise
		/// </returns>
		public bool IsCursorEnabled(GameObject cursor) {
			return (!cursor.tag.EndsWith("Disabled"));
		}

		/// <summary>
		/// Enabled the cursors.
		/// </summary>
		public void EnabledCursors() {
			if (this.IsEnabled != true) {
				this.ChangeCursorsState(true);
			}
		}

		/// <summary>
		/// Disabled the cursors.
		/// </summary>
		public void DisabledCursors() {
			if (this.IsEnabled != false) {
				this.ChangeCursorsState(false);
			}
		}

		/// <summary>
		/// Sets the tag.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <param name="main">if set to <c>true</c> [main].</param>
		private void SetTag(CursorData cursor, bool main) {
			string tag = ((main) ? ("Main") : (string.Empty)) + "Cursor" + ((this.IsEnabled) ? (string.Empty) : ("Disabled"));

			cursor.GameObject.tag = tag;
		}

		/// <summary>
		/// Changes the state of the cursors.
		/// </summary>
		/// <param name="state">Change the state of the cursors.</param>
		private void ChangeCursorsState(bool state) {
			this.IsEnabled = state;
			foreach (KeyValuePair<int, CursorData> cursorData in this.cursors) {
				this.SetTag(cursorData.Value, this.IsMainCursor(cursorData.Value.GameObject));
			}
		}

		/// <summary>
		/// Cursor data struct
		/// </summary>
		internal class CursorData {
			/// <summary>
			/// Initializes a new instance of the <see cref="CursorData"/> class.
			/// </summary>
			/// <param name="gameObject">The game object.</param>
			public CursorData(GameObject gameObject) {
				this.GameObject = gameObject;
				this.LastFrameUpdate = Time.frameCount;
			}

			/// <summary>
			/// The last frame update
			/// </summary>
			public int LastFrameUpdate { get; private set; }

			/// <summary>
			/// The game object
			/// </summary>
			public GameObject GameObject { get; private set; }

			/// <summary>
			/// Updates this instance.
			/// </summary>
			public void Update() {
				this.LastFrameUpdate = Time.frameCount;
			}
		}
	}
}
