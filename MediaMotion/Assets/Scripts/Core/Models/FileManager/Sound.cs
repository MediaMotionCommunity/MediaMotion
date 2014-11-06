using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Sound : AFile {
		public Sound() {
			this.fileType = FileType.Sound;
		}
	}
}