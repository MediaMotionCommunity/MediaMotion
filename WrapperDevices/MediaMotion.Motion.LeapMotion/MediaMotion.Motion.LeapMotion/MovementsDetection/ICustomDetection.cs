using Leap;
using MediaMotion.Motion.LeapMotion.Core;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for custom gesture.
	/// Implement public IEnumerable Detection(Frame frame)
	/// </summary>
	public interface ICustomDetection : IMouvementDetection {
		/// <summary>
		/// Method for detect action, return list of IAction
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <param name="actionCollection"></param>
		/// <returns>List of IAction</returns>
		void Detection(Frame frame, IActionCollection actionCollection);
	}
}
