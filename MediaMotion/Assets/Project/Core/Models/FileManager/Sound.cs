using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Sound File
	/// </summary>
	public class Sound : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Sound" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Sound(FileInfo fileInfo)
			: base(fileInfo, FileType.Sound, "Music") {
		}
	}
}