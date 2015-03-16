using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Detections Class
	/// Run all Detection class known
	/// </summary>
	public class Detections {
		#region Fields
		private readonly IDetectionContainer detectionContainer;

		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Detections" /> class.
		/// </summary>
		public Detections() {
			this.detectionContainer = new DetectionContainer();
			this.Configuration();
		}
		#endregion

		#region Methods
		/// <summary>
		/// Run all detection class with specified frame
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <returns>List of IAction</returns>
		public IEnumerable<IAction> MovementsDetection(Frame frame) {
			var actions = new ActionCollection();
			this.detectionContainer.DetectMouvement(frame, actions);
			return actions;
		}
		#endregion

		#region Privates Methods
		private void Configuration() {
			var swipeDetection = new SwipeDetection();
			var cursorDetection = new CursorDetection();
			var pinchSelectionDetection = new PinchSelectionDetection();
			var pinchGrabDetection = new PinchGrabDetection(cursorDetection);

			this.detectionContainer.Register(swipeDetection);

			this.detectionContainer.Register(cursorDetection);
			this.detectionContainer.Register(pinchSelectionDetection);
			this.detectionContainer.Register(pinchGrabDetection);
		}
		#endregion
	}
}
