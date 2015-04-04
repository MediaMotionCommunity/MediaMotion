using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
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
		/// The cursor prefabs
		/// </summary>
		public GameObject BaseCursor;

		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// The cursor manager service
		/// </summary>
		private ICursorManagerService cursorManagerService;

		/// <summary>
		/// The explorer
		/// </summary>
		private ExplorerController explorer;

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		/// <param name="cursorManagerService">The cursor manager service.</param>
		public void Init(IInputService inputService, ICursorManagerService cursorManagerService) {
			this.inputService = inputService;
			this.cursorManagerService = cursorManagerService;
			this.explorer = GameObject.Find("Explorer").GetComponent<ExplorerController>();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			foreach (IAction action in this.inputService.GetMovements(ActionType.BrowsingCursor)) {
				int id = (action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Id;

				if (!this.cursorManagerService.UpdateCursor(id)) {
					GameObject cursorObject = Instantiate(this.BaseCursor) as GameObject;

					// init
					cursorObject.name = "cursor_" + id;
					cursorObject.transform.parent = this.gameObject.transform;
					cursorObject.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

					this.cursorManagerService.AddCursor(cursorObject, id, this.Select, this.Deselect);
				}
			}
			this.cursorManagerService.CleanCursors(DeleteDelay);
		}

		/// <summary>
		/// Selections the specified game object.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <param name="gameObject">The game object.</param>
		public void Select(GameObject cursor, GameObject gameObject) {
			if (this.cursorManagerService.IsMainCursor(cursor) && this.cursorManagerService.IsCursorEnabled(cursor)) {
				this.explorer.Select(gameObject);
			}
		}

		/// <summary>
		/// Releases the specified game object.
		/// </summary>
		/// <param name="cursor">The cursor.</param>
		/// <param name="gameObject">The game object.</param>
		public void Deselect(GameObject cursor, GameObject gameObject) {
			if (this.cursorManagerService.IsMainCursor(cursor) && this.cursorManagerService.IsCursorEnabled(cursor)) {
				this.explorer.Deselect(gameObject);
			}
		}
	}
}
