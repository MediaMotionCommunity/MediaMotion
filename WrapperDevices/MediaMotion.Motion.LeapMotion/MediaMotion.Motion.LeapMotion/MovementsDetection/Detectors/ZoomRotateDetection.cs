using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class ZoomRotateDetection : ICustomDetection {
		private readonly TimeSpan interval = new TimeSpan(0, 0, 0, 0, 500);
		private DateTime? inZomeTime;

		public ZoomRotateDetection() {
			this.inZomeTime = null;
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
				this.inZomeTime = null;
				return;
			}
			var fingers = hand.Fingers.Where(f => f.TouchZone == Pointable.Zone.ZONETOUCHING && f.IsExtended && f.IsValid);
			if (!fingers.Any()) {
				this.inZomeTime = null;
				return;
			}
			bool isDetectAction = false;
			var bones = fingers.Select(f => f.Bone(Bone.BoneType.TYPE_DISTAL));
			var maxZ = bones.Max(o => o.Center.z);
			bones = bones.Where(b => b.Center.z.Equals(maxZ));
			foreach (var bone in bones) {
				if (bone.IsValid) {
					if (bone.Center.x < -40.0f && bone.Center.y > 110.0f && bone.Center.y < 300.0f) {
						this.IsCorrectInterval(actionCollection, ActionType.ZoomOut, bone);
						isDetectAction = true;
					}
					else if (bone.Center.x > 40.0f && bone.Center.y > 110.0f && bone.Center.y < 300.0f) {
						this.IsCorrectInterval(actionCollection, ActionType.ZoomIn, bone);
						isDetectAction = true;
					}
					else if (bone.Center.x > -100.0f && bone.Center.x < 100.0f && bone.Center.y > 250.0f) {
						this.IsCorrectInterval(actionCollection, ActionType.RotateRight, bone);
						isDetectAction = true;
					}
					else if (bone.Center.x > -100.0f && bone.Center.x < 100.0f && bone.Center.y < 100.0f) {
						this.IsCorrectInterval(actionCollection, ActionType.RotationLeft, bone);
						isDetectAction = true;
					}
				}
			}
			if (!isDetectAction) {
				this.inZomeTime = null;
			}
		}

		private void IsCorrectInterval(IActionCollection actionCollection, ActionType action, Bone bone) {
			if (!this.inZomeTime.HasValue) {
				this.inZomeTime = DateTime.Now;
			}
			if (DateTime.Now.Subtract(this.inZomeTime.Value) > this.interval) {
				actionCollection.Add(action, this.CalculateStrength(bone));
			}
		}

		private float CalculateStrength(Bone bone) {
			var absValue = Math.Abs(bone.Center.z);
			return absValue / 100;
		}
	}
}