using Leap;
using MediaMotion.Motion.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Detections Class
	/// Run all Detection class known
	/// </summary>
	public class Detections {
		#region Fields
		#region Detection class
		/// <summary>
		/// Detection class for swipe gesture
		/// </summary>
		private SwipeDetection swipeDetection;

		/// <summary>
		/// Detection class for keyTap gesture
		/// </summary>
		private KeyTapDetection keyTapDetection;
		#endregion

		/// <summary>
		/// List of detection class for default leap gesture
		/// </summary>
		private Dictionary<Gesture.GestureType, Func<Gesture, IEnumerable<IAction>>> leapDetections;

		/// <summary>
		/// List of detection class for custom gesture
		/// </summary>
		private List<Func<Frame, IEnumerable<IAction>>> customDetections;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Detections" /> class.
		/// </summary>
		public Detections() {
			this.swipeDetection = new SwipeDetection();
			this.keyTapDetection = new KeyTapDetection();

			this.leapDetections = new Dictionary<Gesture.GestureType, Func<Gesture, IEnumerable<IAction>>>();
			this.customDetections = new List<Func<Frame, IEnumerable<IAction>>>();

			this.leapDetections.Add(this.swipeDetection.GetGestureType(), this.swipeDetection.Detection);
			this.leapDetections.Add(this.keyTapDetection.GetGestureType(), this.keyTapDetection.Detection);
		}
		#endregion

		#region Methods
		/// <summary>
		/// Run all detection class with specified frame
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <returns>List of IAction</returns>
		public IEnumerable<IAction> MovementsDetection(Frame frame) {
			IEnumerable<IAction> list = new List<IAction>();
			GestureList gestures = frame.Gestures();

			for (int i = 0; i < gestures.Count; i++) {
				if (this.leapDetections.ContainsKey(gestures[i].Type)) {
					list = list.Concat<IAction>(this.leapDetections[gestures[i].Type](gestures[i]));
				}
			}

			foreach (Func<Frame, IEnumerable<IAction>> func in this.customDetections) {
				list = list.Concat<IAction>(func(frame));
			}
			return list;
		}
		#endregion
	}
}
