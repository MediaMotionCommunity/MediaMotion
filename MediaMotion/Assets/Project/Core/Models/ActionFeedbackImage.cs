using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Models {
	public class ActionFeedbackImage : MonoBehaviour {
		/// <summary>
		/// The action
		/// </summary>
		private ActionType action;

		/// <summary>
		/// The images
		/// </summary>
		private Dictionary<ActionType, Material> materials;

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionFeedbackImage"/> is cancel.
		/// </summary>
		/// <value>
		///   <c>true</c> if cancel; otherwise, <c>false</c>.
		/// </value>
		public bool Cancel { get; set; }

		/// <summary>
		/// Initializes the specified action.
		/// </summary>
		/// <param name="action">The action.</param>
		public void Init(ActionType action) {
			this.action = action;
			this.Cancel = false;

			if (this.materials == null) {
				this.ImportMaterials();
			}
			this.GetComponent<Renderer>().material = this.materials[action];
		}

		/// <summary>
		/// Imports the materials.
		/// </summary>
		public void ImportMaterials() {
			this.materials = new Dictionary<ActionType, Material>();

			this.materials.Add(ActionType.StartBack, Resources.Load<Material>("Models/FeedbackMovement-Back"));
			this.materials.Add(ActionType.StartLeave, Resources.Load<Material>("Models/FeedbackMovement-Leave"));
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
		}
	}
}
