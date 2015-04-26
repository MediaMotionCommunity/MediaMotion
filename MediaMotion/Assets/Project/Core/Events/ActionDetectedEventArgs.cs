using System;
using MediaMotion.Core.Models.Wrapper.Enums;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Events {
	/// <summary>
	/// Event args for Action Detected Event
	/// </summary>
	public class ActionDetectedEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionDetectedEventArgs"/> class.
		/// </summary>
		/// <param name="Action">The action.</param>
		public ActionDetectedEventArgs(IAction Action) {
			this.Action = Action;
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		/// <value>
		/// The action.
		/// </value>
		public IAction Action { get; private set; }
	}
}
