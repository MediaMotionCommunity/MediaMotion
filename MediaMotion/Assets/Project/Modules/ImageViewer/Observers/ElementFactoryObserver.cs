using System.IO;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.ImageViewer.Models;

namespace MediaMotion.Modules.ImageViewer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		/// The element
		/// </returns>
		public IElement Create(string path) {
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
	}
}
