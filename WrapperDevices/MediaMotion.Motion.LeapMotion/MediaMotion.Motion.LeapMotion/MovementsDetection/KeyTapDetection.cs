using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;
using Action = MediaMotion.Motion.Actions.Action;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
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
		/// <returns>List of IAction</returns>
		protected override IEnumerable<IAction> SecureDetection(Gesture gesture) {
			List<IAction> list = new List<IAction>();
			KeyTapGesture swipe = new KeyTapGesture(gesture);

			if (this.IsStateValid(swipe.State)) {
				list.Add(new Action(ActionType.Select, null));
			}
			return list;
		}
		#endregion
	}
}
