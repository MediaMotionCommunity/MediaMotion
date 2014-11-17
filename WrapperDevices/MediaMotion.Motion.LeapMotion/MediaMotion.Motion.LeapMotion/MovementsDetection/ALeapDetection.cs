using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for default LeapMotion gesture.
	/// Implement public IEnumerable<IAction> Detection(Gesture gesture)
	/// </summary>
	public abstract class ALeapDetection {
		#region Fields
		/// <summary>
		/// Leap Gesture type of checked gesture
		/// </summary>
		protected Gesture.GestureType type;
		#endregion
		#region Methods
		/// <summary>
		/// To implement, do the detection and return a list of IAction
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <returns>list of IAction</returns>
		public abstract IEnumerable<IAction> Detection(Gesture gesture);

		/// <summary>
		/// The type of gesture used
		/// </summary>
		/// <returns>Leap GestureType</returns>
		public Gesture.GestureType GetGestureType() {
			return this.type;
		}

		/// <summary>
		/// Return true if is a valid leap Gesture.GestureState
		/// </summary>
		/// <param name="state">Leap Gesture.GestureState</param>
		/// <returns>Return a boolean</returns>
		protected bool IsStateValid(Gesture.GestureState state) {
			if (state == Gesture.GestureState.STATE_START ||
				state == Gesture.GestureState.STATE_UPDATE ||
				state == Gesture.GestureState.STATE_STOP) {
				return true;
			}
			return false;
		}
		#endregion
	}
}
