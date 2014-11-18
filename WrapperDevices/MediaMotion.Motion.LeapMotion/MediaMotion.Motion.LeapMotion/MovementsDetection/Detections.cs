using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using MediaMotion.Motion.Actions;

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
		private readonly Dictionary<Gesture.GestureType, Func<Gesture, IEnumerable<IAction>>> leapDetections;

		/// <summary>
		/// List of detection class for custom gesture
		/// </summary>
		private readonly List<Func<Frame, IEnumerable<IAction>>> customDetections;
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
			var gestures = frame.Gestures();

			list = gestures.Where(t => this.leapDetections.ContainsKey(t.Type)).Aggregate(list, (current, t) => current.Concat(this.leapDetections[t.Type](t)));

			return this.customDetections.Aggregate(list, (current, func) => current.Concat(func(frame)));
		}
		#endregion
	}
}
