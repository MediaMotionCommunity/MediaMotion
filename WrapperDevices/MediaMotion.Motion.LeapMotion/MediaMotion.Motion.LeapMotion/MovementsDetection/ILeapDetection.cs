using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	public interface ILeapDetection : IMouvementDetection {
		/// <summary>
		/// To implement, do the detection and return a list of IAction
		/// </summary>
		/// <param name="gesture">Leap Gesture</param>
		/// <param name="actionCollection"></param>
		/// <returns>list of IAction</returns>
		void Detection(Gesture gesture, IActionCollection actionCollection);

		/// <summary>
		/// The type of gesture used
		/// </summary>
		/// <returns>Leap GestureType</returns>
		Gesture.GestureType GetGestureType();
	}
}