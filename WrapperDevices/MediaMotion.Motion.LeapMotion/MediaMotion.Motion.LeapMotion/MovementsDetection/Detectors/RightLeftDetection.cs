using System;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class RightLeftDetection : ASecureLeapDetection {
		public RightLeftDetection() {
			this.Type = Gesture.GestureType.TYPE_SWIPE;
		}

		protected override void SecureDetection(Gesture gesture, IActionCollection actionCollection) {
			var swipe = new SwipeGesture(gesture);
			if (!this.IsStateValid(swipe.State)) {
				return;
			}
			var isHorizontal = Math.Abs(swipe.Direction.x) > Math.Abs(swipe.Direction.y);
			if (isHorizontal) {
				actionCollection.Add(swipe.Direction.x > 0 ? ActionType.Left : ActionType.Right);
			}
		}
	}
}