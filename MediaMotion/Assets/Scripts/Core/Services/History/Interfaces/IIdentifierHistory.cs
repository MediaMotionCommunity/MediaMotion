namespace Mediamotion.Core.Services.History.Interfaces {
	public interface IIdentifierHistory {
		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <returns></returns>
		IIdentifier GetIdentifier();

		/// <summary>
		/// Go back in the history
		/// </summary>
		/// <returns></returns>
		bool Back();

		/// <summary>
		/// Go forward in the history
		/// </summary>
		/// <returns></returns>
		bool Forward();

		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="Action">The action.</param>
		void AddAction(IAction Action);
	}
}
