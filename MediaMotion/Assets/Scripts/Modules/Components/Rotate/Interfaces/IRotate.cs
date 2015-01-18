namespace MediaMotion.Modules.Components.Rotate {
	/// <summary>
	/// Rotate Interface
	/// </summary>
	public interface IRotate {
		/// <summary>
		/// Gets the angle.
		/// </summary>
		/// <value>
		/// The angle.
		/// </value>
		int Angle { get; }

		/// <summary>
		/// Rotates the left.
		/// </summary>
		void RotateLeft();

		/// <summary>
		/// Rotates the right.
		/// </summary>
		void RotateRight();
	}
}
