using System;
using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Image : AFile {
		public Image(string Path)
			: base(Path) {
			this.FileType = FileType.Image;
		}
	}
}