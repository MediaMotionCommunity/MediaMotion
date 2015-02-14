using System.Collections.Generic;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.Core {
	public class ActionCollection : List<IAction>, IActionCollection {
		public void Add(ActionType action, object parameter) {
			this.Add(new Action(action, parameter));
		}

		public void Add(ActionType action) {
			this.Add(new Action(action, null));
		}
	}
}