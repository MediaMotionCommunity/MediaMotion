using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model PDF File
	/// </summary>
	public class PDF : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="PDF" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public PDF(FileInfo fileInfo)
			: base(fileInfo, FileType.PDF, Resources.Load<Texture2D>("PDF-icon")) {
		}
	}
}