using System;
using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	public class OpenHandDetection : ICustomDetection {
		#region Constants

		private readonly TimeSpan timeToValidate = new TimeSpan(0, 0, 0, 2, 0);

		private readonly TimeSpan lockDetectionTime = new TimeSpan(0, 0, 0, 1, 500);

		private readonly float threadsholdHandOpen = 0.0f;

		private float maximunHandDisplacement = 10.0f;
		#endregion

		#region Fields
		private bool lockDetection = false;

		private DateTime lockStart = DateTime.Now;

		private Dictionary<int, bool> handsOpenState;

		private Dictionary<int, DateTime> handsActionLastDetection;

		private Dictionary<int, Vector> handStartPosition;

		private List<int> handsUsed;
		#endregion

		#region Constructor
		public OpenHandDetection() {
			this.handsOpenState = new Dictionary<int, bool>();
			this.handsActionLastDetection = new Dictionary<int, DateTime>();
			this.handStartPosition = new Dictionary<int, Vector>();
			this.handsUsed = new List<int>();
		}
		#endregion

		#region Methods

		public void Detection(Frame frame, IActionCollection actionCollection) {
			List<int> hands = new List<int>(this.handsUsed);
			if (!this.lockDetection) {
				if (!frame.Hands.IsEmpty) {
					foreach (Hand hand in frame.Hands) {
						if (hand.IsValid) {
							this.SynchronizeHandState(hand);
							if (hands.Contains(hand.Id)) {
								hands.Remove(hand.Id);
							}
							if (this.ValidDetection(hand, actionCollection)) {
								this.lockDetection = true;
								this.lockStart = DateTime.Now;
								this.handsOpenState.Clear();
								this.handsActionLastDetection.Clear();
								this.handStartPosition.Clear();
							}
						}
					}
				}
				if (hands.Count != 0) {
					actionCollection.Add(Actions.ActionType.CancelBack);
					foreach (int id in hands) {
						this.handsOpenState.Remove(id);
						this.handsActionLastDetection.Remove(id);
						this.handStartPosition.Remove(id);
						this.handsUsed.Remove(id);
					}
				}
			} else if (this.lockDetection &&
				  (DateTime.Now - this.lockStart) > this.lockDetectionTime) {
				this.lockDetection = false;
			}
		}

		private void SynchronizeHandState(Hand hand) {
			if (!this.handsOpenState.ContainsKey(hand.Id)) {
				this.handsOpenState[hand.Id] = false;
			}
		}

		private bool HandPositionValid(int id, Vector actualPosition) {
			Vector gap = this.handStartPosition[id] - actualPosition;

			if (Math.Abs(gap.x) <= this.maximunHandDisplacement &&
				 Math.Abs(gap.y) <= this.maximunHandDisplacement &&
				 Math.Abs(gap.z) <= this.maximunHandDisplacement) {
				return true;
			}
			return false;
		}

		private bool ValidDetection(Hand hand, IActionCollection actionCollection) {
			if (!this.handsOpenState[hand.Id] &&
				hand.GrabStrength <= this.threadsholdHandOpen) {
				this.handsOpenState[hand.Id] = true;
				this.handsActionLastDetection[hand.Id] = DateTime.Now;
				this.handStartPosition[hand.Id] = hand.PalmPosition;
				if (!this.handsUsed.Contains(hand.Id)) {
					this.handsUsed.Add(hand.Id);
				}
				actionCollection.Add(Actions.ActionType.StartBack, this.timeToValidate);
			} else if (this.handsOpenState[hand.Id] &&
				  (hand.GrabStrength > this.threadsholdHandOpen ||
				  !this.HandPositionValid(hand.Id, hand.PalmPosition))) {
				this.handsUsed.Remove(hand.Id);
				actionCollection.Add(Actions.ActionType.CancelBack);
				return true;
			} else if (this.handsOpenState[hand.Id] &&
				  (DateTime.Now - this.handsActionLastDetection[hand.Id]) >= this.timeToValidate) {
				this.handsActionLastDetection[hand.Id] = DateTime.Now;
				actionCollection.Add(Actions.ActionType.Back);
				actionCollection.Add(Actions.ActionType.StartBack, this.timeToValidate);
			}

			return false;
		}
		#endregion
	}
}
