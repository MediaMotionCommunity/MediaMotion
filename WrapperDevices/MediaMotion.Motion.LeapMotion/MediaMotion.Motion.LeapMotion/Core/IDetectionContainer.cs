using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	public interface IDetectionContainer {
		void Register(ICustomDetection customDetection);

		void Register(ILeapDetection leapDetection);

		void Register<T>(params ActionType[] actions) where T : class, IMouvementDetection;

		void DetectMouvement(Frame frame, IActionCollection actionCollection);

		void Clear();

		void Enable(ActionType action);

		bool IsEmpty();

		void Build();
	}
}