namespace MediaMotion.Motion.Actions {
	/// <summary>
	/// The action.
	/// </summary>
	public class Action : IAction {
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
		/// Gets the type.
		/// </summary>
		public ActionType Type { get; private set; }

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		public object Parameter { get; private set; }

        /// <summary>
        /// Action string representation
        /// </summary>
        public override string ToString() {
            return "Action: " + this.Type.ToString() + " Params: " + this.Parameter.ToString();
        }
    }
}