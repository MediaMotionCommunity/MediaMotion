namespace MediaMotion.Motion.Actions {
	/// <summary>
	/// The action type.
	/// </summary>
	public enum ActionType {
		Up,
		Down,
		Right,
		Left,
		ScrollIn,
		ScrollOut,
		Select,
		Back,
		Leave,
		BrowsingHighlight,
		BrowsingScroll,
		BrowsingCursor,
		GrabStart,
		GrabStop,
		ZoomIn,
		ZoomOut,
		RotateRight,
		RotationLeft,
		SoundUp,
		SoundDown
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
