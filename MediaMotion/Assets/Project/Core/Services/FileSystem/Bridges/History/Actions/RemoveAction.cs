using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.History.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Bridges.History.Actions {
	/// <summary>
	/// Action Remove
	/// </summary>
	public sealed class RemoveAction : IAction {
		/// <summary>
		/// The element
		/// </summary>
		private IElement Element;

		/// <summary>
		/// Initializes a new instance of the <see cref="RemoveAction"/> class.
		/// </summary>
		/// <param name="Element">The element.</param>
		public RemoveAction(IElement Element) {
			this.Element = Element;
		}

		/// <summary>
		/// Do the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Do() {
			return ((MediaMotionCore.Core.GetService("FileSystem") as IFileSystem).Remove(this.Element));
		}

		/// <summary>
		/// Undo the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Undo() {
			return ((MediaMotionCore.Core.GetService("FileSystem") as IFileSystem).Restore(this.Element));
		}
	}
}
