namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// The Cursor interface.
	/// </summary>
	public interface ICursor {
		/// <summary>
		/// Gets the position z depth.
		/// </summary>
		/// <value>
		/// The z.
		/// </value>
		int Z { get; }

		/// <summary>
		/// Gets the position x width.
		/// </summary>
		/// <value>
		/// The x.
		/// </value>
		int X { get; }

		/// <summary>
		/// Gets the position y height.
		/// </summary>
		/// <value>
		/// The y.
		/// </value>
		int Y { get; }

		/// <summary>
		/// Gets a value indicating whether is touching the imaginary screen.
		/// </summary>
		/// <value>
		/// The is touching.
		/// </value>
		bool IsTouching { get; } 
	}
}