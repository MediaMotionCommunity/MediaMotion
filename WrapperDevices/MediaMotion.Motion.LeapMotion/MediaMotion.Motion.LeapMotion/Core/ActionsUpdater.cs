using MediaMotion.Motion.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaMotion.Motion.LeapMotion.Core {
    class ActionsUpdater {

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
                currentActions[action.Type] = action;
            }
        }

        public IEnumerable<IAction> retrieveCurrentActions() {
            List<IAction> actions = new List<IAction>();
            foreach (var elements in currentActions) {
                actions.Add(elements.Value);
            }
            currentActions = new Dictionary<ActionType, IAction>();
            return actions;
        }
        #endregion
    }
}
