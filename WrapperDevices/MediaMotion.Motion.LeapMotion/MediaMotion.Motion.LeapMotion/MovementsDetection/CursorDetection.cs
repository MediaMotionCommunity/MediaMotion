using System;
using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.Actions.Parameters;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Class for browsing prototype action detection
	/// </summary>
	public class CursorDetection : ICustomDetection {
		/// <summary>
		/// Physical height of the virtual browser plan (in mm)
		/// </summary>
		private const float BrowserPlanHeight = 135.0f;

		/// <summary>
		/// Height of the virtual browser plan (in degrees)
		/// </summary>
		private const float BrowserPlanAngle = 3.0f;

		/// <summary>
		/// Scale of the virtual browser plan (in cm/unit)
		/// </summary>
		private const float BrowserPlanScale = 0.6f;

		/// <summary>
		/// Previous frame saved
		/// </summary>
		private Frame lastFrame;

		private List<KeyValuePair<ActionType, Object3>> previousActions = new List<KeyValuePair<ActionType, Object3>>();
		private bool detectionInterrupted;

		public void InterruptNextDetection() {
			this.detectionInterrupted = true;
		}

		/// <summary>
		/// Actual computation using the current frame
		/// </summary>
		/// <param name="frame">Leap current Frame</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		public void Detection(Frame frame, IActionCollection actionCollection) {
			if (this.detectionInterrupted) {
				this.detectionInterrupted = false;
				this.StorePreviousAction(actionCollection);
			}
			else {
				this.previousActions.Clear();
				this.AnalyseCurrentFrame(frame, actionCollection);
			}
		}

		private void AnalyseCurrentFrame(Frame frame, IActionCollection actionCollection) {
			//// Every hand in independantly generating events
			foreach (var hand in frame.Hands) {
				//// Get the true hand position
				var id = hand.Id;
				var palm = hand.PalmPosition;
				var pos = new Vector3(palm.x, palm.y, palm.z);
				var ppos = this.BrowserPlanPosition(pos);
				//// Create the cursor action (every frame, every hand)
				this.StoreAction(actionCollection, ActionType.BrowsingCursor, new Object3(id, ppos));
				//// Possible highlight event (highlight elements if hand is over the plan)
				if (ppos.Y > 0) {
					this.StoreAction(actionCollection, ActionType.BrowsingHighlight, new Object3(id, ppos));
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
					this.StoreAction(actionCollection, ActionType.BrowsingScroll, new Object3(id, ppos - prevPpos));
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
		private Vector3 BrowserPlanPosition(Vector3 pos) {
			Vector3 rotated = new Vector3(pos);
			const float Height = BrowserPlanHeight;
			const float Angle = -BrowserPlanAngle * 0.0174532925f; // to radians
			// Simply rotate the position using the plan origin as the rotation point
			float y = rotated.Y - Height;
			float z = rotated.Z;
			float ry = (float)((y * Math.Cos(Angle)) - (z * Math.Sin(Angle)));
			float rz = (float)((y * Math.Sin(Angle)) + (z * Math.Cos(Angle)));
			rotated.Y = ry;
			rotated.Z = rz;
			return rotated * BrowserPlanScale;
		}

		private void StoreAction(IActionCollection collection, ActionType type, Object3 value) {
			this.previousActions.Add(new KeyValuePair<ActionType, Object3>(type, value));
			collection.Add(type, value);
		}

		private void StorePreviousAction(IActionCollection collection) {
			foreach (var action in this.previousActions) {
				collection.Add(action.Key, action.Value);
			}
		}
	}
}
