using Leap;
using Action = MediaMotion.Motion.Actions.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	class PinchSelectionDetection : ICustomDetection {

		#region Constant

		private readonly float ThresholdDetection = 0.9f;
		private readonly TimeSpan ReleaseTimeMax = new TimeSpan(0, 0, 0, 0, 500);
		#endregion

		#region Fields

		private DateTime lastAction;
		private Boolean detectionState;
		private DateTime[] lastDetections;
		private Boolean[] states;
		#endregion

		#region Consturtor
		public PinchSelectionDetection() {
			this.lastAction = DateTime.Now;
			this.detectionState = false;

			this.lastDetections = new DateTime[2];
			this.lastDetections[0] = DateTime.Now;
			this.lastDetections[1] = DateTime.Now;

			this.states = new Boolean[2];
			this.states[0] = false;
			this.states[1] = false;
		}
		#endregion

		#region Methods
		public IEnumerable<IAction> Detection(Frame frame) {
			List<IAction> list = new List<IAction>();
			Boolean detected = false;

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
			} else if (DateTime.Now - this.lastAction > this.ReleaseTimeMax) {
				this.detectionState = true;
			}
			return list;
		}

		private Boolean ValidDetection(int hand, float streng) {
			if (streng < ThresholdDetection && this.states[hand]) {
				this.states[hand] = false;
			}
			if (streng >= ThresholdDetection && !this.states[hand]) {
				this.states[hand] = true;
				this.lastDetections[hand] = DateTime.Now;
			} else if (!this.states[hand] && (DateTime.Now - this.lastDetections[hand] < this.ReleaseTimeMax)) {
				return true;
			}
			return false;
		}
		#endregion
	}
}
