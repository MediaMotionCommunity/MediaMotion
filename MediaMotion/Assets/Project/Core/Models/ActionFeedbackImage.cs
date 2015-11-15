using MediaMotion.Motion.Actions;
using UnityEngine;

namespace MediaMotion.Core.Models {
	public class ActionFeedbackImage : MonoBehaviour {
		/// <summary>
		/// The action
		/// </summary>
		private ActionType action;

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
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
		}
	}
}
