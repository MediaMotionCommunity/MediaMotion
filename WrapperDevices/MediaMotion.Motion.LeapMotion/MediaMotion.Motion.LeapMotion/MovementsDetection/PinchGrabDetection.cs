﻿using System;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	public class PinchGrabDetection : APinchDetection {
		#region Constants
		private readonly TimeSpan ReleadTimeMin = new TimeSpan(0, 0, 0, 1, 500);
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
		#endregion

		#region Consturtor
		/// <summary>
		/// Initializes a new instance of the <see cref="PinchSelectionDetection" /> class.
		/// </summary>
		public PinchGrabDetection() : base(new TimeSpan(0, 0, 0, 0, 500)) {
			this.lastDetections = new DateTime[2];
			this.lastDetections[0] = DateTime.Now;
			this.lastDetections[1] = DateTime.Now;

			this.states = new bool[2];
			this.states[0] = false;
			this.states[1] = false;

			this.perfomGrab = new bool[2];
			this.perfomGrab[0] = false;
			this.perfomGrab[1] = false;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Check for valid detection of PinchSelection
		/// </summary>
		/// <param name="hand">int represent the hand use</param>
		/// <param name="streng">float represent the streng of pinch</param>
		/// <returns>true if the movment is valid</returns>
		protected override void ValidDetection(int hand, float streng, IActionCollection actionCollection) {
			if (streng < this.ThresholdDetection && this.states[hand]) {
				this.states[hand] = false;
				this.lastDetections[hand] = DateTime.Now;
			}
			else if (streng >= this.ThresholdDetection && !this.states[hand]) {
				this.states[hand] = true;
				this.lastDetections[hand] = DateTime.Now;
			}

			if (this.states[hand] && !this.perfomGrab[hand] && (DateTime.Now - this.lastDetections[hand]) >= this.ReleadTimeMin) {
				actionCollection.Add(ActionType.GrabStart);
				this.perfomGrab[hand] = true;
			}
			else if (!this.states[hand] && this.perfomGrab[hand]) {
				actionCollection.Add(ActionType.GrabStop);
				this.detectionState = false;
				this.perfomGrab[hand] = false;
			}
		}
		#endregion
	}
}