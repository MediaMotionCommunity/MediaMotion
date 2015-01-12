namespace Mediamotion.Core.Services.History.Interfaces {
	public interface IAction {
		/// <summary>
		/// Do the action
		/// </summary>
		/// <returns></returns>
		bool Do();

		/// <summary>
		/// Undo the action
		/// </summary>
		/// <returns></returns>
		bool Undo();
	}
}
