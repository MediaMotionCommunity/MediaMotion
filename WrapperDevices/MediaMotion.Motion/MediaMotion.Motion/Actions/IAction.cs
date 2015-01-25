namespace MediaMotion.Motion.Actions {
	/// <summary>
	/// The action type.
	/// </summary>
	public enum ActionType {
		Cursor,
		Up,
		Down,
		Right,
		Left,
		ScrollIn,
		ScrollOut,
		Select,
		Back
	}

	/// <summary>
	/// The Action interface.
	/// </summary>
	public interface IAction {
		/// <summary>
		/// Gets the type.
		/// </summary>
		ActionType Type { get; }

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		object Parameter { get; }
	}
}
