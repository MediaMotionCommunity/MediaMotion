using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	public class DetectionContainer : IDetectionContainer {
		/// <summary>
		/// List of detection class for default leap gesture
		/// </summary>
		private readonly Dictionary<Gesture.GestureType, ALeapDetection> leapDetections;

		/// <summary>
		/// List of detection class for custom gesture
		/// </summary>
		private readonly List<ICustomDetection> customDetections;

		public DetectionContainer() {
			this.customDetections = new List<ICustomDetection>();
			this.leapDetections = new Dictionary<Gesture.GestureType, ALeapDetection>();
		}

		public void Register(ICustomDetection customDetection) {
			this.customDetections.Add(customDetection);
		}

		public void Register(ALeapDetection leapDetection) {
			this.leapDetections.Add(leapDetection.GetGestureType(), leapDetection);
		}

		public void DetectMouvement(Frame frame, IActionCollection actionCollection) {
			var gestures = frame.Gestures();
			foreach (var gesture in gestures) {
				if (this.leapDetections.ContainsKey(gesture.Type)) {
					this.leapDetections[gesture.Type].Detection(gesture, actionCollection);
				}
			}
			foreach (var customDetection in this.customDetections) {
				customDetection.Detection(frame, actionCollection);
			}
		}
	}
}