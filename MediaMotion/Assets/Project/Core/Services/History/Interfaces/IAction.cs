namespace MediaMotion.Core.Services.History.Interfaces {
	/// <summary>
	/// Action Interface
	/// </summary>
	public interface IAction {
		/// <summary>
		/// Do the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Do();

		/// <summary>
		/// Undo the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Undo();
	}
}
