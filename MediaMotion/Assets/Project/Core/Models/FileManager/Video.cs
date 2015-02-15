using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Video File
	/// </summary>
	public class Video : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Video" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Video(FileInfo fileInfo)
			: base(fileInfo, FileType.Video, "Movie") {
		}
	}
}