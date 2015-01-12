namespace Mediamotion.Core.Services.History.Interfaces {
	public interface IHistory {
		/// <summary>
		/// Gets the history.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns></returns>
		IIdentifierHistory GetHistory(IIdentifier Identifier);

		/// <summary>
		/// Backs the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns></returns>
		bool Back(IIdentifier Identifier);

		/// <summary>
		/// Forwards the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns></returns>
		bool Forward(IIdentifier Identifier);

		/// <summary>
		/// Adds the action to the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <param name="Action">The action.</param>
		void AddAction(IIdentifier Identifier, IAction Action);
	}
}
