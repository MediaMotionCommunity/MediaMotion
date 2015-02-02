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
		private readonly IElement element;

		/// <summary>
		/// Initializes a new instance of the <see cref="RemoveAction"/> class.
		/// </summary>
		/// <param name="element">The element.</param>
		public RemoveAction(IElement element) {
			this.element = element;
		}

		/// <summary>
		/// Do the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Do() {
			return (MediaMotionCore.Core.ServicesContainer.Get<IFileSystemService>().Remove(this.element));
		}

		/// <summary>
		/// Undo the action
		/// </summary>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool Undo() {
			return (MediaMotionCore.Core.ServicesContainer.Get<IFileSystemService>().Restore(this.element));
		}
	}
}
