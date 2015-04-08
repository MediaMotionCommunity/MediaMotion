using System.IO;
using MediaMotion.Modules.ImageViewer.Models.Abstract;

namespace MediaMotion.Modules.ImageViewer.Models {
	/// <summary>
	/// Image model
	/// </summary>
	public class Image : AImage {
		/// <summary>
		/// Initializes a new instance of the <see cref="Image"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Image(FileInfo fileInfo)
			: base(fileInfo) {
		}
	}
}
