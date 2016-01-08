using System;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class ZoomDetection : ICustomDetection {
		private readonly TimeSpan _interval = new TimeSpan(0, 0, 0, 0, 500);
		private DateTime? _inZomeTime;
		private DateTime? _lastTimeInZone;
		private Vector _previousPosition;

		public void Detection(Frame frame, IActionCollection actionCollection) {
			if (frame.Hands.IsEmpty)
				return;
			foreach (var hand in frame.Hands.Where(hand => hand.IsValid)) {
				if (!this.Detect(hand, actionCollection)) {
					//if (_lastTimeInZone != null && 
					//	_lastTimeInZone + _interval < DateTime.Now) {
					//	_lastTimeInZone = null;
					//	_previousPosition = null;
					//}
					_previousPosition = null;
				}
				else {
					_lastTimeInZone = DateTime.Now;
				}
			}
		}

		private bool Detect(Hand hand, IActionCollection actionCollection) {
			if (hand.Fingers.Count(f => f.TouchZone == Pointable.Zone.ZONETOUCHING) < 1) {
				return false;
			}
			var fingers = hand.Fingers.Where(f => f.TouchZone == Pointable.Zone.ZONETOUCHING && f.IsExtended && f.IsValid)
				.ToArray();
			if (!fingers.Any()) {
				return false;
			}
			var bones = fingers.Select(f => f.Bone(Bone.BoneType.TYPE_DISTAL))
				.Where(b => b.IsValid)
				.ToArray();
			var boneMinDistance = bones.Min(f => f.Center.z);
			var bone = bones.FirstOrDefault(b => b.Center.z.Equals(boneMinDistance));
			if (bone != null && bone.Center.x > 0) {
				if (_previousPosition == null) {
					_previousPosition = bone.Center;
					return true;
				}
				var distance = RemoveNoise(_previousPosition.y - bone.Center.y);
				_previousPosition = bone.Center;
				//Console.WriteLine("distance: " + distance);
				if (Math.Abs(distance) > 0.001)
					actionCollection.Add(distance > 0f ? ActionType.ZoomOut : ActionType.ZoomIn, Math.Abs(distance));
				return true;
			}
			return false;
		}

		private static float RemoveNoise(float distance) {
			return (float) (Math.Round(Convert.ToDouble(distance)*10f)/10f);
		}
	}
}