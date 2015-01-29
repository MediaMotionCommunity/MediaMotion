using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	/// <summary>
	/// Factory Interface
	/// </summary>
	public interface IFactory {
		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <returns>The created element</returns>
		IElement Create(string Path);
	}
}
