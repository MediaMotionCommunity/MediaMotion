using System;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Text : AFile {
		public Text(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.Text;
		}
	}
}