using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.ImageViewer.Models;

namespace MediaMotion.Modules.ImageViewer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// The supported extensions
		/// </summary>
		private readonly string[] supportedExtensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactoryObserver"/> class.
		/// </summary>
		public ElementFactoryObserver() {
			this.supportedExtensions = new string[] { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".bmp", ".tiff" };
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

				return (this.supportedExtensions.Contains(fileInfo.Extension.ToLower()));
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
					case ".bmp":
						return (new BMPImage(fileInfo));
					case ".jpg":
					case ".jpeg":
						return (new JPEGImage(fileInfo));
					case ".png":
						return (new PNGImage(fileInfo));
					case ".gif":
					case ".svg":
					case ".tiff":
					default:
						return (new Image(fileInfo));
				}
			}
			return (null);
		}
	}
}
