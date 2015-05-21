using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core.Exceptions;
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

			IMouvementDetection Create(ICollection<KeyValuePair<Gesture.GestureType, ILeapDetection>> leapDetections, IEnumerable<ICustomDetection> customDetections);
			bool IsCustomDetector();

			void Build(List<IDetectorDocker> types);
			bool IsTypeOf(Type parameterType);
		}

		public void Register(ICustomDetection customDetection) {
			this.customDetections.Add(customDetection);
		}

		public void Register(ILeapDetection leapDetection) {
			this.leapDetections.Add(leapDetection.GetGestureType(), leapDetection);
		}

		public void Register<T>(params ActionType[] actions) where T : class, IMouvementDetection {
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
					var detector = detectorDocker.Create(this.leapDetections, this.customDetections);
					if (detectorDocker.IsCustomDetector()) {
						this.Register(detector as ICustomDetection);						
					}
					else {
						this.Register(detector as ILeapDetection);
					}
				}
			}
		}

		public bool IsEmpty() {
			return !(this.customDetections.Any() || this.leapDetections.Any());
		}

		public void Build() {
			foreach (var docker in this.detectionTypes) {
				docker.Build(this.detectionTypes);
			}
		}

		private class DetectorDocker<T> : IDetectorDocker where T : class, IMouvementDetection {
			private readonly IEnumerable<ActionType> actions;
			private Type globalType;
			private ConstructorInfo constructorInfo;
			private ParameterInfo[] parametersTypes;

			public DetectorDocker(IEnumerable<ActionType> actions) {
				this.actions = actions;
				this.globalType = typeof(T);
			}

			public bool IsUsing(ActionType action) {
				return this.actions.Contains(action);
			}

			public IMouvementDetection Create(ICollection<KeyValuePair<Gesture.GestureType, ILeapDetection>> leapDetections, IEnumerable<ICustomDetection> customDetections) {
				if (this.parametersTypes == null || !this.parametersTypes.Any()) {
					return Activator.CreateInstance<T>();
				}
				return (T)this.constructorInfo.Invoke(this.GetParameters(leapDetections, customDetections));
			}

			public bool IsCustomDetector() {
				return typeof(ICustomDetection).IsAssignableFrom(typeof(T));
			}

			public void Build(List<IDetectorDocker> types) {
				foreach (var info in this.globalType.GetConstructors()) {
					var parameterInfos = info.GetParameters();

					if (parameterInfos.Any(p => this.IsTypeOf(p.ParameterType))) {
						throw new DetectionResolveException("Cannot inject him self");						
					}
					if (!parameterInfos.All(p => types.Any(t => t.IsTypeOf(p.ParameterType)))) {
						continue;
					}
					this.constructorInfo = info;
					this.parametersTypes = parameterInfos;
					return;
				}
				throw new DetectionResolveException("cannot resolve");
			}

			public bool IsTypeOf(Type parameterType) {
				return parameterType == this.globalType;
			}
			private object[] GetParameters(ICollection<KeyValuePair<Gesture.GestureType, ILeapDetection>> leapDetections, IEnumerable<ICustomDetection> customDetections) {
				var parameters = new object[this.parametersTypes.Count()];

				for (var i = 0; i < this.parametersTypes.Count(); ++i) {
					var type = this.parametersTypes[i].ParameterType;
					var leapKeyValuePair = leapDetections.FirstOrDefault(ld => ld.Value.GetType() == type);
					var @object = leapKeyValuePair.Value ??
												  (IMouvementDetection)customDetections.FirstOrDefault(cd => cd.GetType() == type);
					parameters[i] = @object;
				}
				return parameters;
			}
		}
	}
}