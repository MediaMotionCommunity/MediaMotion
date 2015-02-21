namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// The cursor position.
	/// </summary>
	public class Object3 {
		/// <summary>
		/// Initializes a new instance of the <see cref="Object3"/> class.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="pos">The position.</param>
		public Object3(int id, Vector3 pos) {
			this.Id = id;
			this.Pos = pos;
		}

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public int Id { get; private set; }

		/// <summary>
		/// Gets or sets the position.
		/// </summary>
		/// <value>
		/// The position.
		/// </value>
		public Vector3 Pos { get; private set; }
	}
}
