using System.IO;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Modules.Explorer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// Does the observer supports this type of element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		///   <c>true</c> if the observer can support this element, <c>false</c> otherwise
		/// </returns>
		public bool Supports(string path) {
			return (Directory.Exists(path));
		}

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>The element</returns>
		public IElement Create(string path) {
			if (this.Supports(path)) {
				return (new Folder(new DirectoryInfo(path)));
			}
			return (null);
		}
	}
}
