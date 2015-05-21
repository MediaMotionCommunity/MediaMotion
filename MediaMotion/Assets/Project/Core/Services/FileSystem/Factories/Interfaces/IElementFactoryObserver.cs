using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories.Interfaces {
	/// <summary>
	/// File system element factory observer
	/// </summary>
	public interface IElementFactoryObserver {
		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		/// The element
		/// </returns>
		IElement Create(string path);
	}
}
