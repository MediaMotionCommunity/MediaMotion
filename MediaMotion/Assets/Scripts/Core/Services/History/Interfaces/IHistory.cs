namespace Mediamotion.Core.Services.History.Interfaces {
	/// <summary>
	/// History Interface
	/// </summary>
	public interface IHistory {
		/// <summary>
		/// Gets the history.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>The identifier</returns>
		IIdentifierHistory GetHistory(IIdentifier Identifier);

		/// <summary>
		/// Backs the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Back(IIdentifier Identifier);

		/// <summary>
		/// Forwards the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Forward(IIdentifier Identifier);

		/// <summary>
		/// Adds the action to the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <param name="Action">The action.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool AddAction(IIdentifier Identifier, IAction Action);
	}
}
