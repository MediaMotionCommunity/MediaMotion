using System;
using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Leap;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	class SwipeDetection : ASecureLeapDetection {
		#region Constant
		private const Double MIN_VAL = 0.5;
		#endregion

		#region Constructor
		public SwipeDetection() {
			this.type = Gesture.GestureType.TYPE_SWIPE;
		}
		#endregion

		#region Methods
		protected override IEnumerable<IAction> SecureDetection(Gesture gesture) {
			List<IAction> list = new List<IAction>();

			SwipeGesture swipe = new SwipeGesture(gesture);

			if (this.IsStateValid(swipe.State)) {
				if (swipe.Direction.x > MIN_VAL) {
					list.Add(new Action(ActionType.Right, null));
				} else if (swipe.Direction.x < -MIN_VAL) {
					list.Add(new Action(ActionType.Left, null));
				}

				if (swipe.Direction.y > MIN_VAL) {
					list.Add(new Action(ActionType.Up, null));
				} else if (swipe.Direction.y < -MIN_VAL) {
					list.Add(new Action(ActionType.Down, null));
				}

				if (swipe.Direction.z > MIN_VAL) {
					list.Add(new Action(ActionType.ScrollIn, null));
				} else if (swipe.Direction.z < -MIN_VAL) {
					list.Add(new Action(ActionType.ScrollOut, null));
				}
			}

			return list;
		}

		#endregion
	}
}
