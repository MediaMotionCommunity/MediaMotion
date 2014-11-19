using System;
using MediaMotion.Core.Models.Wrapper.Enums;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Core.Models.Wrapper.Events {
	public class ActionDetectedEventArgs : EventArgs {
        public IAction Action { get; private set; }

		public ActionDetectedEventArgs(IAction Action) {
            this.Action = Action;
		}
	}
}
