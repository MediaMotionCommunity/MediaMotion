using MediaMotion.Modules.Components.Rotate.Events;

namespace MediaMotion.Modules.Components.Rotate {
	abstract public class ARotate : IRotate {
		//
		// Properties
		//
		public int Angle { get; protected set; }

		//
		// Delegates
		//
		public delegate void RotateHandler(object sender, RotateEvent e);

		//
		// Events
		//
		public event RotateHandler OnRotate;

		//
		// Action
		//
		public void RotateLeft() {
			this.Angle += 90;

			this.OnRotate(this, new RotateEvent(90));
			this.Normalize();
		}

		public void RotateRight() {
			this.Angle -= 90;

			this.OnRotate(this, new RotateEvent(-90));
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
