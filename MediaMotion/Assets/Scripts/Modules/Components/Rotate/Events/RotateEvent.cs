using System;

namespace MediaMotion.Modules.Components.Rotate.Events {
	public class RotateEvent : EventArgs {
		public int Angle { get; private set; }

		public RotateEvent(int Angle) {
			this.Angle = Angle;
		}
	}
}
