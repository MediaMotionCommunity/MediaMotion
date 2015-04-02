using System.Security.Cryptography.X509Certificates;
using Leap;
using MediaMotion.Motion.Actions;
using MediaMotion.Motion.LeapMotion.MovementsDetection;

namespace MediaMotion.Motion.LeapMotion.Core {
	public interface IDetectionContainer {
		void Register(ICustomDetection customDetection);

		void Register(ILeapDetection leapDetection);

		void Register<T>(params ActionType[] actions) where T : IMouvementDetection, new();

		void DetectMouvement(Frame frame, IActionCollection actionCollection);

		void Clear();

		void Enable(ActionType action);

		bool IsEmpty();
	}
}