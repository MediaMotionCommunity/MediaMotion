namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// The cursor position.
	/// </summary>
	public class Cursor : Vector3 {
		public Cursor(double x, double y, double z) : base(x, y, z) {
		}

		public Cursor(Vector3 v) : base(v) {
		}
	}
}