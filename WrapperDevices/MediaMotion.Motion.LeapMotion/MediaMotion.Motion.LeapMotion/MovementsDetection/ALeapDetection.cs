using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for default LeapMotion gesture.
	/// </summary>
	public abstract class ALeapDetection {
		#region Fields
		/// <summary>
		/// Leap Gesture type of checked gesture
		/// </summary>
		protected Gesture.GestureType Type;
		#endregion
		#region Methods

		/// <summary>
		/// To implement, do the detection and return a list of IAction
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>list of IAction</returns>
		public abstract void Detection(Gesture gesture, IActionCollection actionCollection);

		/// <summary>
		/// The type of gesture used
		/// </summary>
		/// <returns>Leap GestureType</returns>
		public Gesture.GestureType GetGestureType() {
			return this.Type;
		}

		/// <summary>
		/// Return true if is a valid leap Gesture.GestureState
		/// </summary>
		/// <param name="state">Leap Gesture.GestureState</param>
		/// <returns>Return a boolean</returns>
		protected bool IsStateValid(Gesture.GestureState state) {
			return state == Gesture.GestureState.STATE_START ||
			       state == Gesture.GestureState.STATE_UPDATE ||
			       state == Gesture.GestureState.STATE_STOP;
		}

		#endregion
	}
}
