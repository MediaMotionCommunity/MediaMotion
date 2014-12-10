using System.Collections.Generic;
using Leap;
using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for custom gesture.
	/// Implement public IEnumerable Detection(Frame frame)
	/// </summary>
	public interface ICustomDetection {
		/// <summary>
		/// Method for detect action, return list of IAction
		/// </summary>
		/// <param name="frame">Leap Frame</param>
		/// <returns>List of IAction</returns>
		IEnumerable<IAction> Detection(Frame frame);
	}
}
