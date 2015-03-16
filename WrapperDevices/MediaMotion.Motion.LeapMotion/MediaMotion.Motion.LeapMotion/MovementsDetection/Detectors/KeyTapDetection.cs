using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection.Detectors {
	/// <summary>
	/// Class for keyTap action detection
	/// </summary>
	public class KeyTapDetection : ASecureLeapDetection {
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="KeyTapDetection" /> class.
		/// </summary>
		public KeyTapDetection() {
			this.Type = Gesture.GestureType.TYPE_KEY_TAP;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Detection of KeyTap
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		protected override void SecureDetection(Gesture gesture, IActionCollection actionCollection) {
			var swipe = new KeyTapGesture(gesture);

			if (this.IsStateValid(swipe.State)) {
				actionCollection.Add(ActionType.Select);
			}
		}
		#endregion
	}
}
