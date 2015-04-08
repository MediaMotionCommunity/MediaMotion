using System.IO;
using MediaMotion.Modules.ImageViewer.Models.Abstract;

namespace MediaMotion.Modules.ImageViewer.Models {
	/// <summary>
	/// PNG Image model
	/// </summary>
	public class PNGImage : AImage {
		/// <summary>
		/// Initializes a new instance of the <see cref="PNGImage"/> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public PNGImage(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("PNG Image");
		}
	}
}
