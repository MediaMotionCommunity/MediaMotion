using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Regular : AFile {
		public Regular(string Path)
			: base(Path) {
			this.FileType = FileType.Regular;
		}
	}
}