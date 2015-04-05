using System.IO;
using MediaMotion.Modules.ImageViewer.Models.Abstract;

namespace MediaMotion.Modules.ImageViewer.Models {
	/// <summary>
	/// JPEF Image model
	/// </summary>
	public class JPEGImage : AImage {
		/// <summary>
		/// Initializes a new instance of the <see cref="JPEGImage"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public JPEGImage(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("JPEG Image");
		}
	}
}
