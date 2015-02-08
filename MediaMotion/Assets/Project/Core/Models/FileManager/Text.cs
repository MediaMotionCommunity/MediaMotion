using System;
using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Text File
	/// </summary>
	public class Text : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Text" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Text(FileInfo fileInfo)
			: base(fileInfo, FileType.Text) {
		}
	}
}