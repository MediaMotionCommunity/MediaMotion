using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class ZoomDetection : ICustomDetection {
		private Dictionary<HandExtension.eHand, float> handsData;

		public ZoomDetection() {
			this.handsData = new Dictionary<HandExtension.eHand, float>();
		}

		public void Detection(Frame frame, IActionCollection actionCollection) {
			if (frame.Hands.IsEmpty) return;
			foreach (var hand in frame.Hands.Where(hand => hand.IsValid)) {
				this.DetectZoom(hand, actionCollection);
			}
		}

		private void DetectZoom(Hand hand, IActionCollection actionCollection) {
			if (hand.Fingers.Count(f => f.TouchZone == Pointable.Zone.ZONETOUCHING) < 2) {
				return;
			}
			if (!this.handsData.ContainsKey(hand.GetHand())) {
				this.handsData[hand.GetHand()] = hand.PinchStrength;
			}
			else {
				var result = hand.PinchStrength - this.handsData[hand.GetHand()];
				this.handsData[hand.GetHand()] = hand.PinchStrength;
				var action = result > 0 ? ActionType.ZoomIn : ActionType.ZoomOut;
				//Console.WriteLine("{0}", Math.Abs(result));
				actionCollection.Add(action, Math.Abs(result));
			}
		}
	}
}