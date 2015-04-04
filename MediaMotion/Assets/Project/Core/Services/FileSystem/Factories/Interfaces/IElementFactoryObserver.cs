using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories.Interfaces {
	/// <summary>
	/// File system element factory observer
	/// </summary>
	public interface IElementFactoryObserver {
		/// <summary>
		/// Does the observer supports this type of element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns><c>true</c> if the observer can support this element, <c>false</c> otherwise</returns>
		bool Supports(string path);

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>The element</returns>
		IElement Create(string path);
	}
}
