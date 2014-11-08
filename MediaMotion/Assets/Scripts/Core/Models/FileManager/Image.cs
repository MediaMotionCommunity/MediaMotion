using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Image : AFile {
		public Image(string Path)
			: base(Path) {
			this.FileType = FileType.Image;
		}
	}
}