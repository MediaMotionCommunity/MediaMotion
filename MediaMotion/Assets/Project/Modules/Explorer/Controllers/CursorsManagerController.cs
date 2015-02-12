using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Cursors Manager Controller
	/// </summary>
	public class CursorsManagerController : BaseUnityScript<CursorsManagerController> {
		/// <summary>
		/// The delete delay
		/// </summary>
		public const int DeleteDelay = 5;

		/// <summary>
		/// The main cursor tag
		/// </summary>
		public const string MainCursorTag = "MainCursor";

		/// <summary>
		/// The cursor prefabs
		/// </summary>
		public GameObject BaseCursor;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The cursors
		/// </summary>
		private Dictionary<int, CursorData> cursors;

		/// <summary>
		/// The explorer
		/// </summary>
		private ExplorerController explorer;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
			this.cursors = new Dictionary<int, CursorData>();
			this.explorer = GameObject.Find("Explorer").GetComponent<ExplorerController>();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements(ActionType.BrowsingCursor)) {
				int id = 1; // (action.Parameter as MediaMotion.Motion.Actions.Parameters.Cursor).Id;

				if (this.cursors.ContainsKey(id)) {
					this.cursors[id].Update();
				} else {
					CursorData cursor = new CursorData(Instantiate(this.BaseCursor) as GameObject);
					CursorController cursorController;

					// init
					cursor.GameObject.name = "cursor_" + id;
					cursor.GameObject.transform.parent = this.gameObject.transform;
					cursor.GameObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

					// script
					cursorController = cursor.GameObject.AddComponent<CursorController>();
					cursorController.Id = id;
					cursorController.Manager = this;

					if (this.cursors.Count == 0) {
						this.SetMainCursor(cursor);
					}
					this.cursors.Add(id, cursor);
				}
			}
			this.CleanCursors();
		}

		/// <summary>
		/// Selections the specified game object.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <param name="gameObject">The game object.</param>
		public void Select(GameObject cursor, GameObject gameObject) {
			if (cursor.tag.CompareTo(MainCursorTag) == 0) {
				this.explorer.Select(gameObject);
			}
		}

		/// <summary>
		/// Releases the specified game object.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <param name="gameObject">The game object.</param>
		public void Deselect(GameObject cursor, GameObject gameObject) {
			if (cursor.tag.CompareTo(MainCursorTag) == 0) {
				this.explorer.Deselect(gameObject);
			}
		}

		/// <summary>
		/// Sets the main cursor.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		private void SetMainCursor(CursorData cursor) {
			cursor.GameObject.tag = MainCursorTag;
		}

		/// <summary>
		/// Cleans this instance.
		/// </summary>
		private void CleanCursors() {
			List<int> toRemove = new List<int>();

			foreach (KeyValuePair<int, CursorData> cursor in this.cursors.Where(cursorData => (cursorData.Value.LastFrameUpdate + DeleteDelay) < Time.frameCount)) {
				UnityEngine.Object.Destroy(cursor.Value.GameObject);
				toRemove.Add(cursor.Key);
			}
			if (toRemove.Count > 0) {
				foreach (int id in toRemove) {
					this.cursors.Remove(id);
				}

				// if no more main cursor
				if (!this.cursors.Any(cursorData => cursorData.Value.GameObject.tag.CompareTo(MainCursorTag) == 0) && this.cursors.Count > 0) {
					this.SetMainCursor(this.cursors.First(cursorData => true).Value);
				}
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
