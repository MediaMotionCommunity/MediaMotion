using System;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	/// <summary>
	/// Class for pinch selection movement
	/// </summary>
	public class PinchSelectionDetection : APinchDetection {
		#region Fields
		/// <summary>
		/// Last detection of movements evolution
		/// </summary>
		private DateTime[] lastDetections;
		
		/// <summary>
		/// Current states of movements evolution
		/// </summary>
		private bool[] states;
		#endregion

		#region Consturtor
		/// <summary>
		/// Initializes a new instance of the <see cref="PinchSelectionDetection" /> class.
		/// </summary>
		public PinchSelectionDetection() : base(new TimeSpan(0, 0, 0, 0, 500)) {
			this.lastDetections = new DateTime[2];
			this.lastDetections[0] = DateTime.Now;
			this.lastDetections[1] = DateTime.Now;

			this.states = new bool[2];
			this.states[0] = false;
			this.states[1] = false;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Check for valid detection of PinchSelection
		/// </summary>
		/// <param name="handId">int represent the hand use</param>
		/// <param name="strength">float represent the strength of pinch</param>
		/// <param name="actionCollection"></param>
		/// <returns>true if the movment is valid</returns>
		protected override void ValidDetection(int handId, float strength, Hand hand, IActionCollection actionCollection) {
			if (strength < this.ThresholdDetection && this.states[handId]) {
				this.states[handId] = false;
			}
			if (strength >= this.ThresholdDetection && !this.states[handId]) {
				this.states[handId] = true;
				this.lastDetections[handId] = DateTime.Now;
			}
			else if (!this.states[handId] && (DateTime.Now - this.lastDetections[handId] < this.ReleaseTimeMax)) {
				actionCollection.Add(ActionType.Select);
				this.DetectionState = false;
			}
		}
		#endregion
	}
}
