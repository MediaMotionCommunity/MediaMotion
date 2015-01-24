using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using Action = MediaMotion.Motion.Actions.Action;

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
		/// <returns>List of IAction</returns>
		protected override IEnumerable<IAction> SecureDetection(Gesture gesture) {
			var list = new List<IAction>();

			var swipe = new SwipeGesture(gesture);

			if (!this.IsStateValid(swipe.State)) {
				return list;
			}
			if (swipe.Direction.x > MinVal || swipe.Direction.x < -MinVal) {
				list.Add(new Action(ActionType.Return, null));
			}

			return list;
		}

		#endregion
	}
}
