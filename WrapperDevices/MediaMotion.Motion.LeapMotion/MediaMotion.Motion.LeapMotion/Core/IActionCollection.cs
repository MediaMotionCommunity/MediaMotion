using MediaMotion.Motion.Actions;

namespace MediaMotion.Motion.LeapMotion.Core {
	public interface IActionCollection {
		void Add(ActionType action, object parameter);

		void Add(ActionType action);
	}
}
