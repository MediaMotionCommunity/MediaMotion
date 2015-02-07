using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Class for swipe detection
	/// </summary>
	public class SwipeDetection : ASecureLeapDetection {
		#region Constant
		/// <summary>
		/// Minimal value for direction detection
		/// </summary>
		private const double MinVal = 0.8;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="SwipeDetection" /> class.
		/// </summary>
		public SwipeDetection() {
			this.Type = Gesture.GestureType.TYPE_SWIPE;
		}
		#endregion

		#region Methods

		/// <summary>
		/// Detection of swipe
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		protected override void SecureDetection(Gesture gesture, IActionCollection actionCollection) {
			var swipe = new SwipeGesture(gesture);

			if (!this.IsStateValid(swipe.State)) {
				return;
			}
			if (swipe.Direction.x > MinVal || swipe.Direction.x < -MinVal) {
				actionCollection.Add(ActionType.Back);
			}
		}

		#endregion
	}
}
