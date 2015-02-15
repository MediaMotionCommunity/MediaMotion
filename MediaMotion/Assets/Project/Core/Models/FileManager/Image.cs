using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Image File
	/// </summary>
	public class Image : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Image" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Image(FileInfo fileInfo)
			: base(fileInfo, FileType.Image, "Image") {
		}
	}
}