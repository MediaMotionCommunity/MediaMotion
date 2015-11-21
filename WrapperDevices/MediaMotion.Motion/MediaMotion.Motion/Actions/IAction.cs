namespace MediaMotion.Motion.Actions {
	/// <summary>
	/// The action type.
	/// </summary>
	public enum ActionType {
		None,
		Up,
		Down,
		Right,
		Left,
		ScrollIn,
		ScrollOut,
		Select,
		StartBack,
		CancelBack,
		Back,
        StartLeave,
        CancelLeave,
		Leave,
		BrowsingHighlight,
		BrowsingScroll,
		BrowsingCursor,
		GrabStart,
		GrabPerform,
		GrabStop,
		ZoomIn,
		ZoomOut,
		RotateRight,
		RotateLeft,
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
