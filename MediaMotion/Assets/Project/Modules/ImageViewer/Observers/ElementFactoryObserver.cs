using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Abstracts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.ImageViewer.Models;

namespace MediaMotion.Modules.ImageViewer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : AElementFactoryObserver {
		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactoryObserver"/> class.
		/// </summary>
		public ElementFactoryObserver()
			: base(new string[] { ".jpg", ".jpeg", ".gif", ".png", ".svg", ".bmp", ".tiff" }) {
		}

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		///   The element
		/// </returns>
		public override IElement Create(string path) {
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
