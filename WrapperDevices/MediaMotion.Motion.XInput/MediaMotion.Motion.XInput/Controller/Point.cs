namespace MediaMotion.Motion.XInput.Controller
{
	/// <summary>
	/// The point.
	/// </summary>
	public class Point
    {
		/// <summary>
		/// Gets or sets the x.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// Gets or sets the y.
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// The to string.
		/// </summary>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public override string ToString() {
		    return string.Format("({0}, {1})", this.X, this.Y);
	    }
    }
}
