﻿using System.Collections.Generic;
using Mediamotion.Core.Services;
using Mediamotion.Core.Services.History;
using Mediamotion.Core.Services.History.Interfaces;

namespace MediaMotion.Core.Services.History {
	/// <summary>
	/// History Service
	/// </summary>
	public sealed class HistoryService : IHistory {
		/// <summary>
		/// The instance
		/// </summary>
		private static readonly HistoryService Instance = new HistoryService();

		/// <summary>
		/// The histories
		/// </summary>
		private Dictionary<IIdentifier, IIdentifierHistory> Histories;
		
		/// <summary>
		/// Prevents a default instance of the <see cref="HistoryService"/> class from being created.
		/// </summary>
		private HistoryService() {
			this.Histories = new Dictionary<IIdentifier, IIdentifierHistory>();
		}

		/// <summary>
		/// Gets the instance
		/// </summary>
		/// <returns>The instance</returns>
		public static HistoryService GetInstance() {
			return (HistoryService.Instance);
		}

		/// <summary>
		/// Gets the history of specified identifier
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>The history</returns>
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
		/// <param name="Identifier">The identifier.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Back(IIdentifier Identifier) {
			return (this.GetHistory(Identifier).Back());
		}

		/// <summary>
		/// Forwards the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Forward(IIdentifier Identifier) {
			return (this.GetHistory(Identifier).Forward());
		}

		/// <summary>
		/// Adds the action to the history of specified identifier.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <param name="Action">The action.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool AddAction(IIdentifier Identifier, IAction Action) {
			return (this.GetHistory(Identifier).AddAction(Action));
		}
	}
}
