using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
    public class FistBackDetection : ICustomDetection {
        #region Constants
        /// <summary>
        /// Maximun time for do and release fist for valid detection
        /// </summary>
        private readonly TimeSpan releaseTimeMax = new TimeSpan(0, 0, 0, 1, 500);

        /// <summary>
        /// Time detection will be locked when a valid detection found
        /// </summary>
        private readonly TimeSpan lockDetectionTime = new TimeSpan(0, 0, 0, 1, 0);

        private readonly float threasholdFistClosedDetection = 0.2f;
        private readonly float threadsholdFistOpenDetection = 0.0f;
        #endregion

        #region Fields
        /// <summary>
        /// Boolean use for lock detection class
        /// </summary>
        private bool lockDetection = false;

        /// <summary>
        /// Last detection of back action
        /// </summary>
        private DateTime lastValidDetection = DateTime.Now;
        private Dictionary<int, bool> handsFistClosedState;
        private Dictionary<int, DateTime> handsFistOpenedLastDetection;
        #endregion

        #region Constructor
        public FistBackDetection() {
            this.lastValidDetection = DateTime.Now;
            this.handsFistClosedState = new Dictionary<int, bool>();
            this.handsFistOpenedLastDetection = new Dictionary<int, DateTime>();
        }
        #endregion

        #region Methods

        public void Detection(Frame frame, IActionCollection actionCollection) {
            if (!this.lockDetection) {
                if (!frame.Hands.IsEmpty) {
                    foreach (Hand hand in frame.Hands) {
                        if (hand.IsValid) {
                            this.SynchronizeHandsState(hand);
                            if (this.ValidDetection(hand, actionCollection)) {
                                this.lockDetection = true;
                                this.lastValidDetection = DateTime.Now;
                                this.handsFistClosedState.Clear();
                                this.handsFistOpenedLastDetection.Clear();
                            }
                        }
                    }
                }
            }
            else if (this.lockDetection &&
                (DateTime.Now - this.lastValidDetection > this.releaseTimeMax)) {
                    this.lockDetection = false;
            }
        }

        private void SynchronizeHandsState(Hand hand) {
            if (!this.handsFistClosedState.ContainsKey(hand.Id)) {
                this.handsFistClosedState[hand.Id] = true;
            }
        }

        private bool ValidDetection(Hand hand, IActionCollection actionCollection) {
            if (this.handsFistClosedState[hand.Id] &&
                hand.GrabStrength <= this.threadsholdFistOpenDetection) {
                    Console.WriteLine("Detected: Open fist");
                    this.handsFistClosedState[hand.Id] = false;
                    this.handsFistOpenedLastDetection[hand.Id] = DateTime.Now;
            }
            else if (!this.handsFistClosedState[hand.Id] &&
                hand.GrabStrength >= this.threasholdFistClosedDetection) {
                    if (DateTime.Now - this.handsFistOpenedLastDetection[hand.Id] <= this.releaseTimeMax) {
                        actionCollection.Add(Actions.ActionType.Back);
                        return true;
                    }
                this.handsFistClosedState[hand.Id] = true;
            }
            return false;
        }

        #endregion
    }
}
