using System;
using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;


namespace MediaMotion.Motion.LeapMotion.MovementsDetection {

    /// <summary>
    /// Class for browsing prototype action detection
    /// </summary>
    public class EasyFileBrowsingDetection : ICustomDetection {

        /// <summary>
        /// Previous frame saved
        /// </summary>
        private Frame last_frame = null;

        /// <summary>
        /// Height of the virtual browser plan (in mm)
        /// </summary>
        private readonly double browserPlanHeight = 150;

        /// <summary>
        /// Height of the virtual browser plan (in degrees)
        /// </summary>
        private readonly double browserPlanAngle = 0; // TMP Ignore

        /// <summary>
        /// Calculate the position relative to the corrected 3D plan
        /// </summary>
        /// <param name="pos">Position to correct</param>
        /// <returns>Corrected position</returns>
        private Vector3 BrowserPlanPosition(Vector3 pos)
        {
            var rotated = new Vector3(pos);
            var height = this.browserPlanHeight;
            var angle = -this.browserPlanAngle * 0.0174532925; // to radians
            // Simply rotate the position using the plan origin as the rotation origin
            var y = rotated.y - height;
            var z = rotated.z;
            var ry = y * Math.Cos(angle) - z * Math.Sin(angle);
            var rz = y * Math.Sin(angle) + z * Math.Cos(angle);
            rotated.y = ry;
            rotated.z = rz;
            return rotated;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EasyFileBrowsingDetection" /> class.
        /// </summary>
        public EasyFileBrowsingDetection() {
        }

        /// <summary>
        /// Actual computation using the current frame
        /// </summary>
        /// <param name="frame">Leap current Frame</param>
        /// <returns>List of IAction</returns>
        public IEnumerable<IAction> Detection(Frame frame) {
            List<IAction> list = new List<IAction>();
            // Every hand in independantly generating events
            foreach (var hand in frame.Hands) {
                // Get the true hand position
                var id = hand.Id;
                var palm = hand.PalmPosition;
                var pos = new Vector3(palm.x, palm.y, palm.z);
                // Correct the position
                var ppos = this.BrowserPlanPosition(pos);
                // Create the cursor action (every frame, every hand)
                list.Add(new Actions.Action(ActionType.BrowsingCursor, ppos));
                // Possible highlight event (highlight elements if hand is over the plan)
                if (ppos.y > 0) {
                    list.Add(new Actions.Action(ActionType.BrowsingHighlight, ppos));
                }
                // Check for possible scroll event (if hand under the plan, scroll)
                if (ppos.y <= 0) {
                    // Get previous frame hand position
                    Vector prev_palm = null;
                    if (last_frame != null) {
                        foreach (var prev_hand in last_frame.Hands) {
                            if (prev_hand.Id == id) {
                                prev_palm = prev_hand.PalmPosition;
                            }
                        }
                    }
                    // If there was a previous frame for this hand
                    if (prev_palm != null) {
                        // Calculate true corrected palm position
                        var prev_pos = new Vector3(prev_palm.x, prev_palm.y, prev_palm.z);
                        var prev_ppos = this.BrowserPlanPosition(prev_pos);
                        // Scrolling events if under plan
                        if (prev_ppos.y <= this.browserPlanHeight) {
                            list.Add(new Actions.Action(ActionType.BrowsingScroll, ppos - prev_ppos));
                        }
                    }
                }
            }
            // Prepare for next frame
            last_frame = frame;
            return list;
        }
    }
}
