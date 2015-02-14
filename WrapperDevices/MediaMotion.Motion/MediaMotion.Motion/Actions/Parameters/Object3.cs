namespace MediaMotion.Motion.Actions.Parameters {
	/// <summary>
	/// The cursor position.
	/// </summary>
	public class Object3 {

		public int id { get; set; }
		public Vector3 pos { get; set; }

		public Object3(int h_id, Vector3 h_pos) {
			id = h_id;
			pos = h_pos;
		}

	}
}
