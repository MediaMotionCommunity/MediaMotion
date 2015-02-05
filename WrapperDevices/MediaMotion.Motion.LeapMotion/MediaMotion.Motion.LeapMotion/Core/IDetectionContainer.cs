using Leap;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	public interface IDetectionContainer {
		void Register(ICustomDetection customDetection);

		void Register(ALeapDetection leapDetection);

		void DetectMouvement(Frame frame, IActionCollection actionCollection);
	}
}