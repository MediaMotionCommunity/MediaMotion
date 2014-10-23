using System;

namespace MediaMotion.Modules.Components.Zoom.Events {
	public class ZoomEvent : EventArgs {
		public float Zoom { get; private set; }

		ZoomEvent(float Zoom) {
			this.Zoom = Zoom;
		}
	}
}
