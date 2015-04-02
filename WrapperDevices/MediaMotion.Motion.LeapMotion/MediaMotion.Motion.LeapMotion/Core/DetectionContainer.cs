using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	public class DetectionContainer : IDetectionContainer {
		/// <summary>
		/// List of detection class for default leap gesture
		/// </summary>
		private readonly Dictionary<Gesture.GestureType, ILeapDetection> leapDetections;

		/// <summary>
		/// List of detection class for custom gesture
		/// </summary>
		private readonly List<ICustomDetection> customDetections;

		private List<IDetectorDocker> detectionTypes;

		public DetectionContainer() {
			this.customDetections = new List<ICustomDetection>();
			this.leapDetections = new Dictionary<Gesture.GestureType, ILeapDetection>();
			this.detectionTypes = new List<IDetectorDocker>();
		}

		private interface IDetectorDocker {
			bool IsUsing(ActionType action);

			IMouvementDetection Create();
			bool IsCustomDetector();
		}

		public void Register(ICustomDetection customDetection) {
			this.customDetections.Add(customDetection);
		}

		public void Register(ILeapDetection leapDetection) {
			this.leapDetections.Add(leapDetection.GetGestureType(), leapDetection);
		}

		public void Register<T>(params ActionType[] actions) where T : IMouvementDetection, new() {
			this.detectionTypes.Add(new DetectorDocker<T>(actions));
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

		public void Clear() {
			this.leapDetections.Clear();
			this.customDetections.Clear();
		}

		public void Enable(ActionType action) {
			foreach (var detectorDocker in this.detectionTypes) {
				if (detectorDocker.IsUsing(action)) {
					if (detectorDocker.IsCustomDetector()) {
						this.Register(detectorDocker.Create() as ICustomDetection);						
					}
					else {
						this.Register(detectorDocker.Create() as ILeapDetection);						
					}
				}
			}
		}

		public bool IsEmpty() {
			return !(this.customDetections.Any() || this.leapDetections.Any());
		}

		private class DetectorDocker<T> : IDetectorDocker where T : IMouvementDetection, new() {
			private readonly IEnumerable<ActionType> actions;

			public DetectorDocker(IEnumerable<ActionType> actions) {
				this.actions = actions;
			}

			public bool IsUsing(ActionType action) {
				return this.actions.Contains(action);
			}

			public IMouvementDetection Create() {
				return Activator.CreateInstance<T>();
			}

			public bool IsCustomDetector() {
				return typeof(ICustomDetection).IsAssignableFrom(typeof(T));
			}
		}
	}
}