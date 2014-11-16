using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
    class Detections {
		#region Fields
		#region Detection class
		private SwipeDetection swipeDetection;
		private KeyTapDetection keyTapDetection;
		#endregion

		private Dictionary<Gesture.GestureType, Func<Gesture, IEnumerable<IAction>>> leapDetections;
		private List<Func<Frame, IEnumerable<IAction>>> customDetections;
		#endregion

		#region Constructor
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
		public IEnumerable<IAction> MovementsDetection(Frame frame) {
			IEnumerable<IAction> list = new List<IAction>();
			GestureList gestures = frame.Gestures();

			for(int i = 0; i < gestures.Count; i++) {
				if(this.leapDetections.ContainsKey(gestures[i].Type)) {
					list = list.Concat<IAction>(this.leapDetections[gestures[i].Type](gestures[i]));
				}
			}

			foreach(Func<Frame, IEnumerable<IAction>> func in this.customDetections) {
				list = list.Concat<IAction>(func(frame));
			}
			return list;
		}
		#endregion
	}
}
