using System;
using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
    public class OpenHandDetection : ICustomDetection {
        #region Constants

        private readonly TimeSpan timeToValidate = new TimeSpan(0, 0, 0, 1, 0);

        private readonly TimeSpan lockDetectionTime = new TimeSpan(0, 0, 0, 1, 0);

        private readonly float threadsholdHandOpen = 0.0f;
        #endregion

        #region Fields
        private bool lockDetection = false;

        private DateTime lockStart = DateTime.Now;

        private Dictionary<int, bool> handsOpenState;

        private Dictionary<int, DateTime> handsActionLastDetection;
        #endregion

        #region Constructor
        public OpenHandDetection() {
            this.handsOpenState = new Dictionary<int, bool>();
            this.handsActionLastDetection = new Dictionary<int, DateTime>();
        }
        #endregion

        #region Methods

        public void Detection(Frame frame, IActionCollection actionCollection) {
            if (!this.lockDetection) {
                if (!frame.Hands.IsEmpty) {
                    foreach (Hand hand in frame.Hands) {
                        if (hand.IsValid) {
                            this.SynchronizeHandState(hand);
                            if (this.ValidDetection(hand, actionCollection)) {
                                this.lockDetection = true;
                                this.lockStart = DateTime.Now;
                                this.handsOpenState.Clear();
                                this.handsActionLastDetection.Clear();
                            }
                        }
                    }
                }
            }
            else if (this.lockDetection &&
                (DateTime.Now - this.lockStart) > this.lockDetectionTime) {
                    this.lockDetection = false;
            }
        }

        private void SynchronizeHandState(Hand hand) {
            if (!this.handsOpenState.ContainsKey(hand.Id)) {
                this.handsOpenState[hand.Id] = false;
            }
        }

        private bool ValidDetection(Hand hand, IActionCollection actionCollection) {
            if (!this.handsOpenState[hand.Id] &&
                hand.GrabStrength <= this.threadsholdHandOpen) {
                this.handsOpenState[hand.Id] = true;
                this.handsActionLastDetection[hand.Id] = DateTime.Now;
            }
            else if (this.handsOpenState[hand.Id] &&
                hand.GrabStrength > this.threadsholdHandOpen) {
                    return true;
            }
            else if (this.handsOpenState[hand.Id] &&
                (DateTime.Now - this.handsActionLastDetection[hand.Id]) >= this.timeToValidate) {
                this.handsActionLastDetection[hand.Id] = DateTime.Now;
                actionCollection.Add(Actions.ActionType.Back);
            }
            
            return false;
        }
        #endregion
    }
}
