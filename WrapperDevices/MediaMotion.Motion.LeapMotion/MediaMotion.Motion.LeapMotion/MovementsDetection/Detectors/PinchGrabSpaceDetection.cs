using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class PinchGrabSpaceDetection : APinchDetection {
		#region Constants
		private readonly TimeSpan releadTimeMin = new TimeSpan(0, 0, 0, 0, 750);
		#endregion
		
		#region Fields
		/// <summary>
		/// Last detection of movements evolution
		/// </summary>
		private DateTime[] lastDetections;
		
		/// <summary>
		/// Current states of movements evolution
		/// </summary>
		private bool[] states;

		private bool[] perfomGrab;

		private Vector[] startPosition;

		#endregion

		#region Consturtor
		/// <summary>
		/// Initializes a new instance of the <see cref="PinchSelectionDetection" /> class.
		/// </summary>
		public PinchGrabSpaceDetection()
			: base(new TimeSpan(0, 0, 0, 0, 500)) {
			this.lastDetections = new DateTime[2];
			this.lastDetections[0] = DateTime.Now;
			this.lastDetections[1] = DateTime.Now;

			this.states = new bool[2];
			this.states[0] = false;
			this.states[1] = false;

			this.perfomGrab = new bool[2];
			this.perfomGrab[0] = false;
			this.perfomGrab[1] = false;

			this.startPosition = new Vector[2];
		}
		#endregion

		#region Methods

		/// <summary>
		/// Check for valid detection of PinchSelection
		/// </summary>
		/// <param name="handId">int represent the hand use</param>
		/// <param name="strength">float represent the strength of pinch</param>
		/// <returns>true if the movment is valid</returns>
		protected override void ValidDetection(int handId, float strength, Hand hand, IActionCollection actionCollection) {
			if (strength < this.ThresholdDetection && this.states[handId]) {
				this.states[handId] = false;
				this.lastDetections[handId] = DateTime.Now;
			}
			else if (strength >= this.ThresholdDetection && !this.states[handId]) {
				this.states[handId] = true;
				this.lastDetections[handId] = DateTime.Now;
				this.startPosition[handId] = hand.StabilizedPalmPosition;
			}

			if (this.states[handId] && !this.perfomGrab[handId] && (DateTime.Now - this.lastDetections[handId]) >= this.releadTimeMin) {
				actionCollection.Add(ActionType.GrabStart);
				this.perfomGrab[handId] = true;
			}
			else if (this.states[handId] && this.perfomGrab[handId]) {
				Vector position = hand.StabilizedPalmPosition - this.startPosition[handId];
				int value = (((int)position.x) * 100) / 200; 
				actionCollection.Add(ActionType.GrabPerform, value);
				Console.WriteLine("val: " + value);
			}
			else if (!this.states[handId] && this.perfomGrab[handId]) {
				actionCollection.Add(ActionType.GrabStop);
				this.DetectionState = false;
				this.perfomGrab[handId] = false;
			}
		}
		#endregion
	}
}
