using System.Collections.Generic;
using MediaMotion.Core.Services.History.Interfaces;

namespace MediaMotion.Core.Services.History {
	/// <summary>
	/// Identifier History
	/// </summary>
	public class IdentifierHistory : IIdentifierHistory {
		/// <summary>
		/// The identifier
		/// </summary>
		private IIdentifier Identifier;

		/// <summary>
		/// The back actions
		/// </summary>
		private LinkedList<IAction> BackActions;

		/// <summary>
		/// The forward actions
		/// </summary>
		private LinkedList<IAction> ForwardActions;

		/// <summary>
		/// Initializes a new instance of the <see cref="IdentifierHistory"/> class.
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		public IdentifierHistory(IIdentifier Identifier) {
			this.Identifier = Identifier;
			this.BackActions = new LinkedList<IAction>();
			this.ForwardActions = new LinkedList<IAction>();
		}

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <returns>The identifier</returns>
		public IIdentifier GetIdentifier() {
			return (this.Identifier);
		}

		/// <summary>
		/// Go back in the history
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Back() {
			bool OperationSuccessed = false;

			if (this.BackActions.Count >= 1) {
				IAction Action = this.BackActions.Last.Value;

				if ((OperationSuccessed = Action.Undo())) {
					this.ForwardActions.AddFirst(Action);
					this.BackActions.RemoveLast();
				}
			}
			return (OperationSuccessed);
		}

		/// <summary>
		/// Go forward in the history
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Forward() {
			bool OperationSuccessed = false;

			if (this.ForwardActions.Count >= 1) {
				IAction Action = this.ForwardActions.First.Value;

				if ((OperationSuccessed = Action.Undo())) {
					this.BackActions.AddLast(Action);
					this.ForwardActions.RemoveFirst();
				}
			}
			return (OperationSuccessed);
		}

		/// <summary>
		/// Adds the action.
		/// </summary>
		/// <param name="Action">The action.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool AddAction(IAction Action) {
			this.BackActions.AddLast(Action);
			this.ForwardActions.Clear();

			return (true);
		}
	}
}
