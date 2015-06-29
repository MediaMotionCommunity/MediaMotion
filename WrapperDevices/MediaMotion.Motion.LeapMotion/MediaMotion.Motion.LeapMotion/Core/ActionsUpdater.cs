using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.Core {
    public class ActionsUpdater {
        #region Fields
        private Dictionary<ActionType, IAction> currentActions;
        #endregion

        #region Constructor
        public ActionsUpdater() {
            this.currentActions = new Dictionary<ActionType, IAction>();
        }

        #endregion

        #region Methods
        public void UpdateActions(IEnumerable<IAction> actions) {
            foreach (var action in actions) {
                this.currentActions[action.Type] = action;
            }
        }

        public IEnumerable<IAction> RetrieveCurrentActions() {
            List<IAction> actions = new List<IAction>();
            foreach (var elements in this.currentActions) {
                actions.Add(elements.Value);
            }
            this.currentActions = new Dictionary<ActionType, IAction>();
            return actions;
        }
        #endregion
    }
}
