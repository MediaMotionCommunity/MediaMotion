namespace MediaMotion.Motion {
	/// <summary>
	/// The cursor position.
	/// </summary>
	public class CursorPosition {
		/// <summary>
		/// Initializes a new instance of the <see cref="CursorPosition"/> class.
		/// </summary>
		/// <param name="x">
		/// The position x width.
		/// </param>
		/// <param name="y">
		/// The position y height.
		/// </param>
		/// <param name="z">
		/// The position z depth.
		/// </param>
		/// <param name="isTouching">
		/// The is touching.
		/// </param>
		public CursorPosition(int x, int y, int z, bool isTouching) {
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.IsTouching = isTouching;
		}

		/// <summary>
		/// Gets the position z depth.
		/// </summary>
		/// <value>
		/// The z.
		/// </value>
		public int Z { get; private set; }

		/// <summary>
		/// Gets the position x width.
		/// </summary>
		/// <value>
		/// The x.
		/// </value>
		public int X { get; private set; }

		/// <summary>
		/// Gets the position y height.
		/// </summary>
		/// <value>
		/// The y.
		/// </value>
		public int Y { get; private set; }

		/// <summary>
		/// Gets a value indicating whether is touching the imaginary screen.
		/// </summary>
		/// <value>
		/// The is touching.
		/// </value>
		public bool IsTouching { get; private set; }
	}
}