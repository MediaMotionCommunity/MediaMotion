using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using MediaMotion.Motion.Actions;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Class for pinch selection movement
	/// </summary>
	public class PinchSelectionDetection : ICustomDetection {
		#region Constant
		/// <summary>
		/// Threshold for detection of pinch between 0-1
		/// </summary>
		private readonly float ThresholdDetection = 0.9f;
		
		/// <summary>
		/// Maximun time for do and release pinch for valid detection
		/// </summary>
		private readonly TimeSpan ReleaseTimeMax = new TimeSpan(0, 0, 0, 0, 500);
		#endregion

		#region Fields

		/// <summary>
		/// Time of the last action generate
		/// </summary>
		private DateTime lastAction;
		
		/// <summary>
		/// State of detection global, use for avoid spamming select action
		/// </summary>
		private bool detectionState;

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
		public PinchSelectionDetection() {
			this.lastAction = DateTime.Now;
			this.detectionState = false;

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
		/// Detection of PinchSelection
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <returns>List of IAction</returns>
		public IEnumerable<IAction> Detection(Frame frame) {
			List<IAction> list = new List<IAction>();
			bool detected = false;

			if (this.detectionState) {
				if (!frame.Hands.IsEmpty) {
					foreach (Hand hand in frame.Hands) {
						if (hand.IsValid) {
							detected = this.ValidDetection(hand.IsRight ? 0 : 1, hand.PinchStrength) || detected;
						}
					}
				}
				if (detected) {
					list.Add(new Action(ActionType.Select, null));
					this.detectionState = false;
					this.lastAction = DateTime.Now;
				}
			}
			else if (DateTime.Now - this.lastAction > this.ReleaseTimeMax) {
				this.detectionState = true;
			}
			return list;
		}

		/// <summary>
		/// Check for valid detection of PinchSelection
		/// </summary>
		/// <param name="hand">int represent the hand use</param>
		/// <param name="streng">float represent the streng of pinch</param>
		/// <returns>true if the movment is valid</returns>
		private bool ValidDetection(int hand, float streng) {
			if (streng < this.ThresholdDetection && this.states[hand]) {
				this.states[hand] = false;
			}
			if (streng >= this.ThresholdDetection && !this.states[hand]) {
				this.states[hand] = true;
				this.lastDetections[hand] = DateTime.Now;
			}
			else if (!this.states[hand] && (DateTime.Now - this.lastDetections[hand] < this.ReleaseTimeMax)) {
				return true;
			}
			return false;
		}
		#endregion
	}
}
