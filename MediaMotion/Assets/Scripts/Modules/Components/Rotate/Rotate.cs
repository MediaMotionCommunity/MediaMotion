using MediaMotion.Modules.Components.Rotate.Events;

namespace MediaMotion.Modules.Components.Rotate {
	/// <summary>
	/// Rotate Model
	/// </summary>
	public class Rotate : IRotate {
		/// <summary>
		/// Initializes a new instance of the <see cref="Rotate"/> class.
		/// </summary>
		public Rotate() {
			this.Angle = 0;
		}

		/// <summary>
		/// Event Handler Rotate Event
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RotateEventArgs"/> instance containing the event data.</param>
		public delegate void RotateHandler(object sender, RotateEventArgs e);

		/// <summary>
		/// Occurs when rotate.
		/// </summary>
		public event RotateHandler OnRotate;

		/// <summary>
		/// Gets the angle.
		/// </summary>
		/// <value>
		/// The angle.
		/// </value>
		public int Angle { get; private set; }

		/// <summary>
		/// Rotates the left.
		/// </summary>
		public void RotateLeft() {
			this.Angle += 90;

			this.OnRotate(this, new RotateEventArgs(90));
			this.Normalize();
		}

		/// <summary>
		/// Rotates the right.
		/// </summary>
		public void RotateRight() {
			this.Angle -= 90;

			this.OnRotate(this, new RotateEventArgs(-90));
			this.Normalize();
		}

		/// <summary>
		/// Normalizes this instance.
		/// </summary>
		protected void Normalize() {
			if (this.Angle <= -180) {
				this.Angle += 360;
			} else if (this.Angle > 180) {
				this.Angle -= 360;
			}
		}
	}
}
