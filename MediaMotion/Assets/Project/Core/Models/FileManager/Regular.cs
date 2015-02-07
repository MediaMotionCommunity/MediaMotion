using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Regular File
	/// </summary>
	public class Regular : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Regular" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Regular(FileInfo fileInfo)
			: base(fileInfo, FileType.Regular) {
		}
	}
}