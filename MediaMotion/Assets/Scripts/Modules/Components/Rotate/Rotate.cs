using MediaMotion.Modules.Components.Rotate.Events;

namespace MediaMotion.Modules.Components.Rotate {
	public class Rotate : IRotate {
		//
		// Properties
		//
		public int Angle { get; private set; }

		//
		// Delegates
		//
		public delegate void RotateHandler(object sender, RotateEventArgs e);

		//
		// Events
		//
		public event RotateHandler OnRotate;

		//
		// Construct
		//
		public Rotate() {
			this.Angle = 0;
		}

		//
		// Action
		//
		public void RotateLeft() {
			this.Angle += 90;

			this.OnRotate(this, new RotateEventArgs(90));
			this.Normalize();
		}

		public void RotateRight() {
			this.Angle -= 90;

			this.OnRotate(this, new RotateEventArgs(-90));
			this.Normalize();
		}

		//
		// Normalize
		//
		protected void Normalize() {
			switch (true) {
			case this.Angle <= -180:
				this.Angle += 360;
				break;
			case this.Angle > 180:
				this.Angle -= 360;
				break;
			}
		}
	};
}
