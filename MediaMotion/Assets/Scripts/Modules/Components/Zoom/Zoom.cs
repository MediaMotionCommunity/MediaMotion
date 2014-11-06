using MediaMotion.Modules.Components.Zoom.Events;

namespace MediaMotion.Modules.Components.Zoom {
	public class Zoom {
		public float Coeff { get; private set; }

		public delegate void ZoomHandler(object sender, ZoomEventArgs e);

		public event ZoomHandler OnZoom;

		public Zoom() {
			Coeff = 1.0f;
		}

		public void ZoomIn() {
			Coeff *= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(1.5f));
		}

		public void ZoomOut() {
			Coeff /= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(-1.5f));
		}
	};
}
