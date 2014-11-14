using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Leap;

namespace MediaMotion.Motion.LeapMotion.Core {
	/// <summary>
	/// The leap motion core.
	/// </summary>
	public class LeapMotionCore {
		#region Fields

		/// <summary>
		/// The controller.
		/// </summary>
		private readonly Controller controller;

#if DEBUG
		[SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1121:UseBuiltInTypeAlias", Justification = "Reviewed. Suppression is OK here."),
		SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")] 
		private Object thisLock = new Object();
#endif
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="LeapMotionCore"/> class.
		/// </summary>
		public LeapMotionCore() {
			this.controller = new Controller();
			this.Configuration();
		}
		#endregion

		#region Methods

		/// <summary>
		/// The frame.
		/// </summary>
		/// <returns>
		/// The <see cref="IEnumerable"/>.
		/// </returns>
		public IEnumerable<Action> Frame() {
            List<Action> actions = new List<Action>();
            Leap.Frame frame = this.controller.Frame();
            Leap.Frame frame_previous = this.controller.Frame(1);
            // Check for events of the browser page
            this.BrowserActions(actions, frame, frame_previous);

			this.SafeWriteLine("Frame id: " + frame.Id
						+ ", timestamp: " + frame.Timestamp
						+ ", hands: " + frame.Hands.Count
						+ ", fingers: " + frame.Fingers.Count
						+ ", tools: " + frame.Tools.Count
						+ ", gestures: " + frame.Gestures().Count);

			foreach (var hand in frame.Hands) {
				this.SafeWriteLine("  Hand id: " + hand.Id
							+ ", palm position: " + hand.PalmPosition);

				// Get the hand's normal vector and direction
				var normal = hand.PalmNormal;
				var direction = hand.Direction;

				// Calculate the hand's pitch, roll, and yaw angles
				this.SafeWriteLine("  Hand pitch: " + direction.Pitch * 180.0f / (float)Math.PI + " degrees, "
							+ "roll: " + normal.Roll * 180.0f / (float)Math.PI + " degrees, "
							+ "yaw: " + direction.Yaw * 180.0f / (float)Math.PI + " degrees");

				// Get the Arm bone
				var arm = hand.Arm;
				this.SafeWriteLine("  Arm direction: " + arm.Direction
							+ ", wrist position: " + arm.WristPosition
							+ ", elbow position: " + arm.ElbowPosition);

				// Get fingers
				foreach (var finger in hand.Fingers) {
					this.SafeWriteLine("    Finger id: " + finger.Id
								+ ", " + finger.Type().ToString()
								+ ", length: " + finger.Length
								+ "mm, width: " + finger.Width + "mm");

					// Get finger bones
					foreach (var boneType in (Bone.BoneType[])Enum.GetValues(typeof(Bone.BoneType))) {
						var bone = finger.Bone(boneType);
						this.SafeWriteLine("      Bone: " + boneType
									+ ", start: " + bone.PrevJoint
									+ ", end: " + bone.NextJoint
									+ ", direction: " + bone.Direction);
					}
				}
			}

			// Get tools
			foreach (var tool in frame.Tools) {
				this.SafeWriteLine("  Tool id: " + tool.Id
							+ ", position: " + tool.TipPosition
							+ ", direction " + tool.Direction);
			}

			// Get gestures
			var gestures = frame.Gestures();
			foreach (var gesture in gestures) {
				switch (gesture.Type) {
					case Gesture.GestureType.TYPE_CIRCLE:
						var circle = new CircleGesture(gesture);

						// Calculate clock direction using the angle between circle normal and pointable
						var clockwiseness = circle.Pointable.Direction.AngleTo(circle.Normal) <= Math.PI / 2 ? "clockwise" : "counterclockwise";

						float sweptAngle = 0;

						// Calculate angle swept since last frame
						if (circle.State != Gesture.GestureState.STATE_START) {
							var previousUpdate = new CircleGesture(this.controller.Frame(1).Gesture(circle.Id));
							sweptAngle = (circle.Progress - previousUpdate.Progress) * 360;
						}

						this.SafeWriteLine("  Circle id: " + circle.Id
						                   + ", " + circle.State
						                   + ", progress: " + circle.Progress
						                   + ", radius: " + circle.Radius
						                   + ", angle: " + sweptAngle
						                   + ", " + clockwiseness);
						break;
					case Gesture.GestureType.TYPE_SWIPE:
						var swipe = new SwipeGesture(gesture);
						this.SafeWriteLine("  Swipe id: " + swipe.Id
						                   + ", " + swipe.State
						                   + ", position: " + swipe.Position
						                   + ", direction: " + swipe.Direction
						                   + ", speed: " + swipe.Speed);
						break;
					case Gesture.GestureType.TYPE_KEY_TAP:
						var keytap = new KeyTapGesture(gesture);
						this.SafeWriteLine("  Tap id: " + keytap.Id
						                   + ", " + keytap.State
						                   + ", position: " + keytap.Position
						                   + ", direction: " + keytap.Direction);
						break;
					case Gesture.GestureType.TYPE_SCREEN_TAP:
						var screentap = new ScreenTapGesture(gesture);
						this.SafeWriteLine("  Tap id: " + screentap.Id
						                   + ", " + screentap.State
						                   + ", position: " + screentap.Position
						                   + ", direction: " + screentap.Direction);
						break;
					default:
						this.SafeWriteLine("  Unknown gesture type.");
						break;
				}
			}

			if (!frame.Hands.IsEmpty || !frame.Gestures().IsEmpty) {
				this.SafeWriteLine(string.Empty);
			}

            return actions;
        }

        /// <summary>
        /// Height of the virtual browser plan (in mm)
        /// </summary>
        private readonly double browserPlanHeight = 150;

        /// <summary>
        /// Height of the virtual browser plan (in degrees)
        /// </summary>
        private readonly double browserPlanAngle = 12;

        /// <summary>
        /// Calculate the position relative to the corrected 3D plan
        /// </summary>
        private ActionParams.Vector3 BrowserPlanPosition(ActionParams.Vector3 pos)
        {
            var rotated = new ActionParams.Vector3(pos);
            var height = this.browserPlanHeight;
            var angle = -this.browserPlanAngle * 0.0174532925; // to radians
            var y = rotated.y - height;
            var z = rotated.z;
            var ry = y * Math.Cos(angle) - z * Math.Sin(angle);
            var rz = y * Math.Sin(angle) + z * Math.Cos(angle);
            rotated.y = ry;
            rotated.z = rz;
            return rotated;
        }

        /// <summary>
        /// Check the frame for browsing page events and fill the action list
        /// </summary>
        private void BrowserActions(List<Action> actions, Leap.Frame frame, Leap.Frame frame_previous)
        {
            foreach (var hand in frame.Hands) {
                // Get the true hand position
                var id = hand.Id;
                var palm = hand.PalmPosition;
                var pos = new ActionParams.Vector3(palm.x, palm.y, palm.z);
                // Correct the position
                var ppos = this.BrowserPlanPosition(pos);
                // Create the cursor action (every frame, every hand)
                actions.Add(new Action(ActionType.BrowsingCursor, ppos));
                // Possible highlight event (highlight elements if hand is over the plan)
                if (ppos.y > 0)
                {
                    actions.Add(new Action(ActionType.BrowsingHighlight, ppos));
                }
                // Check for possible scroll event (if hand under the plan, scroll)
                if (ppos.y <= 0)
                {
                    // Get previous frame hand position
                    Leap.Vector prev_palm = null;
                    foreach (var prev_hand in frame_previous.Hands)
                    {
                        if (prev_hand.Id == id)
                        {
                            prev_palm = prev_hand.PalmPosition;
                        }
                    }
                    // If there was a previous frame for this hand
                    if (prev_palm != null)
                    {
                        // Calculate true corrected palm position
                        var prev_pos = new ActionParams.Vector3(prev_palm.x, prev_palm.y, prev_palm.z);
                        var prev_ppos = this.BrowserPlanPosition(prev_pos);
                        // Scrolling events if under plan
                        if (prev_ppos.y <= this.browserPlanHeight)
                        {
                            actions.Add(new Action(ActionType.BrowsingScroll, ppos - prev_ppos));
                        }
                    }
                }
            }
        }

		/// <summary>
		/// The get cursor position.
		/// </summary>
		/// <returns>
		/// The <see cref="CursorPosition"/>.
		/// </returns>
		public CursorPosition GetCursorPosition() {
			throw new NotImplementedException("NotImplemented");
		}
		#region Private
		/// <summary>
		/// The configuration of controller.
		/// </summary>
		private void Configuration() {
			this.controller.EnableGesture(Gesture.GestureType.TYPE_CIRCLE);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_KEY_TAP);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_SCREEN_TAP);
			this.controller.EnableGesture(Gesture.GestureType.TYPE_SWIPE);
		}
#if DEBUG
		/// <summary>
		/// The safe write line.
		/// </summary>
		/// <param name="line">
		/// The line.
		/// </param>
		private void SafeWriteLine(string line) {
            /*
			lock (this.thisLock) {
				Console.WriteLine(line);
			}
            */
		}
#endif
		#endregion
		#endregion
	}
}
