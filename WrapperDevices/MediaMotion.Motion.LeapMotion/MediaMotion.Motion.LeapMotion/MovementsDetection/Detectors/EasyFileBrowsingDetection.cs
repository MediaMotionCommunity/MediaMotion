using System;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	/// <summary>
	/// Class for browsing prototype action detection
	/// </summary>
	public class EasyFileBrowsingDetection : ICustomDetection {
		/// <summary>
		/// Physical height of the virtual browser plan (in mm)
		/// </summary>
		private const float BrowserPlanHeight = 140.0f;

		/// <summary>
		/// Height of the virtual browser plan (in degrees)
		/// </summary>
		private const float BrowserPlanAngle = 5.0f;

		/// <summary>
		/// Previous frame saved
		/// </summary>
		private Frame lastFrame;

		/// <summary>
		/// Actual computation using the current frame
		/// </summary>
		/// <param name="frame">Leap current Frame</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		public void Detection(Frame frame, IActionCollection actionCollection) {
			//// Every hand in independantly generating events
			foreach (var hand in frame.Hands) {
				//// Get the true hand position
				var id = hand.Id;
				var palm = hand.PalmPosition;
				var pos = new Vector3(palm.x, palm.y, palm.z);
				//// Correct the position
				var ppos = this.BrowserPlanPosition(pos);
				//// Create the cursor action (every frame, every hand)
				actionCollection.Add(ActionType.BrowsingCursor, new Object3(id, ppos));
				//// Possible highlight event (highlight elements if hand is over the plan)
				if (ppos.Y > 0) {
					actionCollection.Add(ActionType.BrowsingHighlight, new Object3(id, ppos));
				}
				//// Check for possible scroll event (if hand under the plan, scroll)
				if (!(ppos.Y <= 0)) {
					continue;
				}
				//// Get previous frame hand position
				Vector prevPalm = null;
				if (this.lastFrame != null) {
					foreach (var prevHand in this.lastFrame.Hands) {
						if (prevHand.Id == id) {
							prevPalm = prevHand.PalmPosition;
						}
					}
				}
				//// If there was a previous frame for this hand
				if (prevPalm == null) {
					continue;
				}
				//// Calculate true corrected palm position
				var prevPos = new Vector3(prevPalm.x, prevPalm.y, prevPalm.z);
				var prevPpos = this.BrowserPlanPosition(prevPos);
				//// Scrolling events if under plan
				if (prevPpos.Y <= BrowserPlanHeight) {
					actionCollection.Add(ActionType.BrowsingScroll, new Object3(id, ppos - prevPpos));
				}
			}
			//// Prepare for next frame
			this.lastFrame = frame;
		}

		/// <summary>
		/// Calculate the position relative to the corrected 3D plan
		/// </summary>
		/// <param name="pos">Position to correct</param>
		/// <returns>Corrected position</returns>
		private Vector3 BrowserPlanPosition(IVector3 pos) {
			var rotated = new Vector3(pos);
			const float height = BrowserPlanHeight;
			const float angle = -BrowserPlanAngle * 0.0174532925f; // to radians
			// Simply rotate the position using the plan origin as the rotation point
			var y = rotated.Y - height;
			var z = rotated.Z;
			var ry = (float)((y * Math.Cos(angle)) - (z * Math.Sin(angle)));
			var rz = (float)((y * Math.Sin(angle)) + (z * Math.Cos(angle)));

			rotated.Y = ry;
			rotated.Z = rz;
			return rotated;
		}
	}
}
