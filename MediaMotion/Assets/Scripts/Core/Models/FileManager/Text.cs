using System;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Text File
	/// </summary>
	public class Text : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Text"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		/// <param name="Extension">The extension.</param>
		public Text(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.Text;
		}
	}
}