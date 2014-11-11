using System;
using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Text : AFile {
		public Text(string Path)
			: base(Path) {
			this.FileType = FileType.Text;
		}
	}
}