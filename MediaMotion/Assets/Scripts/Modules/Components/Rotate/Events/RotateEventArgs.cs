using System;

namespace MediaMotion.Modules.Components.Rotate.Events {
	public class RotateEventArgs : EventArgs {
		public int Angle { get; private set; }

		public RotateEventArgs(int Angle) {
			this.Angle = Angle;
		}
	}
}
