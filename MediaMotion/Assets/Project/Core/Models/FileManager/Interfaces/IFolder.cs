namespace MediaMotion.Core.Models.FileManager.Interfaces {
	/// <summary>
	/// Folder Interface
	/// </summary>
	public interface IFolder : IElement {
		/// <summary>
		/// Gets the parent path.
		/// </summary>
		/// <returns>The path of the parent or null</returns>
		string GetParentPath();
	}
}