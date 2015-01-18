namespace Mediamotion.Core.Services.History.Interfaces {
	/// <summary>
	/// IdentifierHistory Interface
	/// </summary>
	public interface IIdentifierHistory {
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <returns>The identifier</returns>
		IIdentifier GetIdentifier();

		/// <summary>
		/// Go back in the history
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Back();

		/// <summary>
		/// Go forward in the history
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Forward();

		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="Action">The action.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool AddAction(IAction Action);
	}
}
