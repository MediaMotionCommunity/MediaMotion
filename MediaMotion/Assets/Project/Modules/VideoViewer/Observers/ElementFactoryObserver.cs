using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.VideoViewer.Models;

namespace MediaMotion.Modules.VideoViewer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// The supported extensions
		/// </summary>
		public static readonly string[] SupportedExtensions = new string[] { ".avi", ".mkv", ".mp4", ".wav" };

		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactoryObserver"/> class.
		/// </summary>
		public ElementFactoryObserver() {
		}

		/// <summary>
		/// Does the observer supports this type of element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		///   <c>true</c> if the observer can support this element, <c>false</c> otherwise
		/// </returns>
		public bool Supports(string path) {
			if (File.Exists(path)) {
				FileInfo fileInfo = new FileInfo(path);
				return (ElementFactoryObserver.SupportedExtensions.Contains(fileInfo.Extension.ToLower()));
			}
			return (false);
		}

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>The element</returns>
		public IElement Create(string path) {
			if (this.Supports(path)) {
				FileInfo fileInfo = new FileInfo(path);
				switch (fileInfo.Extension.ToLower()) {
					default:
						return (new Video(fileInfo));
				}
			}
			return (null);
		}
	}
}
