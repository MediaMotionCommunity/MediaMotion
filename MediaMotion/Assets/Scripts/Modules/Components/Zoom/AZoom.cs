using MediaMotion.Modules.Components.Zoom.Events;

namespace MediaMotion.Modules.Components.Zoom {
	abstract public class AZoom {
		//
		// Properties
		//
		float Zoom { get; protected set; }

		//
		// Delegates
		//
		public delegate void ZoomHandler(object sender, ZoomEvent e);

		//
		// Event
		//
		public event ZoomHandler OnZoom;

		AZoom() {
			Zoom = 1.0f;
		}

		//
		// Action
		//
		void ZoomIn() {
			Zoom *= 1.5f;

			this.OnZoom(this, new ZoomEvent(1.5f));
		}

		void ZoomOut() {
			Zoom /= 1.5f;

			this.OnZoom(this, new ZoomEvent(-1.5f));
		}
	};
}
