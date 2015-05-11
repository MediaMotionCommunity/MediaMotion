using System.Collections.Generic;

using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.History.Interfaces;

namespace MediaMotion.Core.Services.History {
	/// <summary>
	/// History Service
	/// </summary>
	public sealed class HistoryService : IHistoryService {
		/// <summary>
		/// The histories
		/// </summary>
		private Dictionary<IIdentifier, IIdentifierHistory> Histories;

		/// <summary>
		/// Initializes a new instance of the <see cref="HistoryService" /> class.
		/// </summary>
		public HistoryService() {
			this.Histories = new Dictionary<IIdentifier, IIdentifierHistory>();
		}

		/// <summary>
		/// Gets the history of specified identifier
		/// </summary>
		/// <param name="Identifier">
		/// The identifier.
		/// </param>
		/// <returns>
		/// The history
		/// </returns>
		public IIdentifierHistory GetHistory(IIdentifier Identifier) {
			IIdentifierHistory History = null;

			if (this.Histories.TryGetValue(Identifier, out History) == false) {
				this.Histories.Add(Identifier, (History = new IdentifierHistory(Identifier)));
			}
			return (History);
		}

		/// <summary>
		/// Backs the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">
		/// The identifier.
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Back(IIdentifier Identifier) {
			return (this.GetHistory(Identifier).Back());
		}

		/// <summary>
		/// Forwards the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">
		/// The identifier.
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Forward(IIdentifier Identifier) {
			return (this.GetHistory(Identifier).Forward());
		}

		/// <summary>
		/// Adds the action to the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">
		/// The identifier.
		/// </param>
		/// <param name="Action">
		/// The action.
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool AddAction(IIdentifier Identifier, IAction Action) {
			return (this.GetHistory(Identifier).AddAction(Action));
		}
	}
}
