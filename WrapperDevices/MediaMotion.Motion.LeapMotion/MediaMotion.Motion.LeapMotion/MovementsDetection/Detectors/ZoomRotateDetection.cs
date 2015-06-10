using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class ZoomRotateDetection : ICustomDetection {
		private readonly Dictionary<HandExtension.HandDirection, float> handsData;

		public ZoomRotateDetection() {
			this.handsData = new Dictionary<HandExtension.HandDirection, float>();
		}

		public void Detection(Frame frame, IActionCollection actionCollection) {
			if (frame.Hands.IsEmpty) {
				return;
			}
			foreach (var hand in frame.Hands.Where(hand => hand.IsValid)) {
				this.DetectZoom(hand, actionCollection);
			}
		}

		private void DetectZoom(Hand hand, IActionCollection actionCollection) {
			if (hand.Fingers.Count(f => f.TouchZone == Pointable.Zone.ZONETOUCHING) < 1) {
				return;
			}
			if (!this.handsData.ContainsKey(hand.GetHand())) {
				this.handsData[hand.GetHand()] = hand.PinchStrength;
			}
			else {
				var result = hand.PinchStrength - this.handsData[hand.GetHand()];
				var fingers = hand.Fingers.Where(f => f.TouchZone == Pointable.Zone.ZONETOUCHING && f.IsExtended && f.IsValid);
				if (!fingers.Any()) {
					return;
				}
				var bones = fingers.Select(f => f.Bone(Bone.BoneType.TYPE_DISTAL));
				var maxZ = bones.Max(o => o.Center.z);
				bones = bones.Where(b => b.Center.z.Equals(maxZ));
				foreach (var bone in bones) {
					if (bone.IsValid) {
						if (bone.Center.x < -40.0f && bone.Center.y > 110.0f && bone.Center.y < 300.0f) {
							actionCollection.Add(ActionType.ZoomOut, this.CalculateStrength(bone));
						}
						else if (bone.Center.x > 40.0f && bone.Center.y > 110.0f && bone.Center.y < 300.0f) {
							actionCollection.Add(ActionType.ZoomIn, this.CalculateStrength(bone));
						} 
						else if (bone.Center.x > -100.0f && bone.Center.x < 100.0f && bone.Center.y > 250.0f) {
							actionCollection.Add(ActionType.RotateRight, this.CalculateStrength(bone));
						}
						else if (bone.Center.x > -100.0f && bone.Center.x < 100.0f && bone.Center.y < 100.0f) {
							actionCollection.Add(ActionType.RotationLeft, this.CalculateStrength(bone));
						}
					}
				}
			}
		}

		private float CalculateStrength(Bone bone) {
			var absValue = Math.Abs(bone.Center.z);
			return absValue / 100;
		}
	}
}