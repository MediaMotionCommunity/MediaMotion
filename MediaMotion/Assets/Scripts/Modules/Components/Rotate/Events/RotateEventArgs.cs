using System;

namespace MediaMotion.Modules.Components.Rotate.Events {
	/// <summary>
	/// Event Handler Rotate Event
	/// </summary>
	public class RotateEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="RotateEventArgs"/> class.
		/// </summary>
		/// <param name="Angle">The angle.</param>
		public RotateEventArgs(int Angle) {
			this.Angle = Angle;
		}

		/// <summary>
		/// Gets the angle.
		/// </summary>
		/// <value>
		/// The angle.
		/// </value>
		public int Angle { get; private set; }
	}
}
