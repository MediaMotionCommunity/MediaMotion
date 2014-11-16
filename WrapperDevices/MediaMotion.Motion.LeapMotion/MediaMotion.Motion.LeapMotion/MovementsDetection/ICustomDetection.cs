using System.Collections.Generic;
using MediaMotion.Motion.Actions;
using Leap;

namespace MediaMotion.Motion.LeapMotion.MovementsDetection {
	public interface ICustomDetection {
		IEnumerable<IAction> Detection(Frame frame);
	}
}