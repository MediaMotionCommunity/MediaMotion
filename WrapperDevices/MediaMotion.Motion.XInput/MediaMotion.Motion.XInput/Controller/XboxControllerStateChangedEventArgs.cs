using System;

namespace MediaMotion.Motion.XInput.Controller {
	/// <summary>
	/// The xbox controller state changed event args.
	/// </summary>
	public class XboxControllerStateChangedEventArgs : EventArgs {
		/// <summary>
		/// Gets or sets the current input state.
		/// </summary>
		public XInputState CurrentInputState { get; set; }

		/// <summary>
		/// Gets or sets the previous input state.
		/// </summary>
		public XInputState PreviousInputState { get; set; }
	}
}
