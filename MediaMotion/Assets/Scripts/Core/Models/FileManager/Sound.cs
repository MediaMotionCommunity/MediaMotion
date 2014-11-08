using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Sound : AFile {
		public Sound(string Path)
			: base(Path) {
			this.FileType = FileType.Sound;
		}
	}
}