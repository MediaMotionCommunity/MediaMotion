using MediaMotion.Modules.Components.Rotate.Events;

namespace MediaMotion.Modules.Components.Rotate {
	public class Rotate : IRotate {
		public int Angle { get; private set; }

		public delegate void RotateHandler(object sender, RotateEventArgs e);

		public event RotateHandler OnRotate;

		public Rotate() {
			this.Angle = 0;
		}

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

		protected void Normalize() {
			if (this.Angle <= -180) {
				this.Angle += 360;
			} else if (this.Angle > 180) {
				this.Angle -= 360;
			}
		}
	};
}
