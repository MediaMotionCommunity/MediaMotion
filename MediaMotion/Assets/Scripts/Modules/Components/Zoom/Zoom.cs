﻿using MediaMotion.Modules.Components.Zoom.Events;

namespace MediaMotion.Modules.Components.Zoom {
	public class Zoom {
		//
		// Properties
		//
		public float Coeff { get; private set; }

		//
		// Delegates
		//
		public delegate void ZoomHandler(object sender, ZoomEventArgs e);

		//
		// Event
		//
		public event ZoomHandler OnZoom;

		Zoom() {
			Coeff = 1.0f;
		}

		//
		// Action
		//
		void ZoomIn() {
			Coeff *= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(1.5f));
		}

		void ZoomOut() {
			Coeff /= 1.5f;

			this.OnZoom(this, new ZoomEventArgs(-1.5f));
		}
	};
}