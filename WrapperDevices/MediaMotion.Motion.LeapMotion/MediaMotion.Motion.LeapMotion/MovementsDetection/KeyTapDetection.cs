using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Motion.Actions;
using Leap;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	class KeyTapDetection : ASecureLeapDetection {
		#region Constructor
		public KeyTapDetection() {
			this.type = Gesture.GestureType.TYPE_KEY_TAP;
		}
		#endregion

		#region Methods
		protected override IEnumerable<IAction> SecureDetection(Gesture gesture) {
			List<IAction> list = new List<IAction>();
			KeyTapGesture swipe = new KeyTapGesture(gesture);

			if(this.IsStateValid(swipe.State)) {
				list.Add(new Action(ActionType.Select, null));
			}
			return list;
		}
		#endregion
	}
}
