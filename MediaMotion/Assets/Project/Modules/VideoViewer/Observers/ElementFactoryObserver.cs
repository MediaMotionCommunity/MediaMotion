using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Abstracts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.VideoViewer.Models;

namespace MediaMotion.Modules.VideoViewer.Observers {
	/// <summary>
	/// Element Factory observer
	/// </summary>
	public class ElementFactoryObserver : AElementFactoryObserver {
		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactoryObserver"/> class.
		/// </summary>
		public ElementFactoryObserver()
			: base(new string[] { ".avi", ".mkv", ".mp4" }) {
		}

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>The element</returns>
		public override IElement Create(string path) {
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
