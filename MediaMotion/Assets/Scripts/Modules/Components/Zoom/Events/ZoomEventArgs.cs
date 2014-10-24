using System;

namespace MediaMotion.Modules.Components.Zoom.Events {
	public class ZoomEventArgs : EventArgs {
		public float Zoom { get; private set; }

		ZoomEventArgs(float Zoom) {
			this.Zoom = Zoom;
		}
	}
}
