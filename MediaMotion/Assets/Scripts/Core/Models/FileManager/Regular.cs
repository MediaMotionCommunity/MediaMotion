using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Regular : AFile {
		public Regular() {
			this.fileType = FileType.Regular;
		}
	}
}