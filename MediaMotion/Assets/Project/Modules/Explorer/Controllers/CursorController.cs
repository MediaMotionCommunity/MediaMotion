﻿using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Cursor controller
	/// </summary>
	public class CursorController : AScript<ExplorerModule, CursorController> {
		/// <summary>
		/// The input service
		/// </summary>
		private IInputService inputService;

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the select callback.
		/// </summary>
		/// <value>
		/// The select callback.
		/// </value>
		public CursorEvent SelectCallback { get; set; }

		/// <summary>
		/// Gets or sets the unselect callback.
		/// </summary>
		/// <value>
		/// The unselect callback.
		/// </value>
		public CursorEvent UnselectCallback { get; set; }

		/// <summary>
		/// Initializes the specified input service.
		/// </summary>
		/// <param name="inputService">The input service.</param>
		public void Init(IInputService inputService) {
			this.inputService = inputService;
		}
		
		/// <summary>
		/// Colorates the specified color.
		/// </summary>
		/// <param name="color">The color.</param>
		private void Colorate(Color32 color) {
			iTween.ColorTo(this.gameObject, color, 0.25f);
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			IAction action = null;

			if ((action = this.inputService.GetCursorMovement(this.Id)) != null) {
				Object3 parameter = action.Parameter as Object3;

				this.gameObject.transform.localPosition = new UnityEngine.Vector3(parameter.Pos.X / 10, parameter.Pos.Y / 10, -parameter.Pos.Z / 25);
				if (parameter.Pos.Y > 0.0f) {
					this.Colorate(new Color32(180, 180, 180, 255));
				} else {
					this.Colorate(new Color32(180, 40, 40, 255));
				}
			}
		}

		/// <summary>
		/// Function that is called after collision with a file.
		/// </summary>
		/// <param name="collider">The collider.</param>
		public void OnTriggerEnter(UnityEngine.Collider collider) {
			if (string.Compare("Element", 0, collider.gameObject.name, 0, 7) == 0) {
				this.SelectCallback(this.gameObject, collider.gameObject);
			}
		}

		/// <summary>
		/// Called when [trigger exit].
		/// </summary>
		/// <param name="collider">The collider.</param>
		public void OnTriggerExit(UnityEngine.Collider collider) {
			if (string.Compare("Element", 0, collider.gameObject.name, 0, 7) == 0) {
				this.UnselectCallback(this.gameObject, collider.gameObject);
			}
		}
	}
}
