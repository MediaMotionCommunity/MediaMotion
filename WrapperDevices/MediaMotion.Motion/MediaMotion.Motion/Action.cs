namespace MediaMotion.Motion {
	/// <summary>
	/// The action type.
	/// </summary>
	public enum ActionType {
        BrowsingCursor,
        BrowsingHighlight,
        BrowsingScroll,
		CommonSelect,
		CommonOpenMenu,
		CommonRightOnce,
		CommonLeftOnce,
		CommonUpOnce,
		CommonDownOnce,
		CommonRight,
		CommonLeft,
		CommonUp,
		CommonDown
	}

	/// <summary>
	/// The action.
	/// </summary>
	public class Action {
		/// <summary>
		/// Initializes a new instance of the <see cref="Action"/> class.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <param name="parameter">
		/// The parameter.
		/// </param>
		public Action(ActionType type, object parameter) {
			this.Type = type;
			this.Parameter = parameter;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Action"/> class.
		/// </summary>
		public Action() {
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		public ActionType Type { get; private set; }

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		public object Parameter { get; private set; }
    }
}