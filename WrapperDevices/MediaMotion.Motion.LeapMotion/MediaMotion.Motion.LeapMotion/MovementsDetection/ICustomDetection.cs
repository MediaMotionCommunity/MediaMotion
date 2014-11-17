using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	/// <summary>
	/// Abstract for custom gesture.
	/// Implement public IEnumerable<IAction> Detection(Frame frame)
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