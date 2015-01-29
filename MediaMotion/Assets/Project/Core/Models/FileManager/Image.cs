using System;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Image File
	/// </summary>
	public class Image : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Image"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		/// <param name="Extension">The extension.</param>
		public Image(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.Image;
		}
	}
}