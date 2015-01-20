using MediaMotion.Modules.Components.Zoom.Events;

namespace MediaMotion.Modules.Components.Zoom {
	/// <summary>
	/// Zoom Model
	/// </summary>
	public class Zoom : IZoom {
		/// <summary>
		/// Initializes a new instance of the <see cref="Zoom"/> class.
		/// </summary>
		public Zoom() {
			this.Coeff = 1.0f;
		}

		/// <summary>
		/// Event Handler Zoom Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="ZoomEventArgs"/> instance containing the event data.</param>
		public delegate void ZoomHandler(object sender, ZoomEventArgs e);

		/// <summary>
		/// Occurs when zooming.
		/// </summary>
		public event ZoomHandler OnZoom;

		/// <summary>
		/// Gets the coefficient.
		/// </summary>
		/// <value>
		/// The coefficient.
		/// </value>
		public float Coeff { get; private set; }

		/// <summary>
		/// Zoom in.
		/// </summary>
		public void ZoomIn() {
			this.Coeff *= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(1.5f));
		}

		/// <summary>
		/// Zoom out.
		/// </summary>
		public void ZoomOut() {
			this.Coeff /= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(-1.5f));
		}
	}
}
