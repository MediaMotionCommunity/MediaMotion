using System;

namespace MediaMotion.Modules.Components.Zoom.Events {
	/// <summary>
	/// Zoom Event Args
	/// </summary>
	public class ZoomEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="ZoomEventArgs"/> class.
		/// </summary>
		/// <param name="Zoom">The zoom.</param>
		public ZoomEventArgs(float Zoom) {
			this.Zoom = Zoom;
		}

		/// <summary>
		/// Gets the zoom.
		/// </summary>
		/// <value>
		/// The zoom.
		/// </value>
		public float Zoom { get; private set; }
	}
}
