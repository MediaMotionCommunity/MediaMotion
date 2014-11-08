using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class PDF : AFile {
		public PDF(string Path)
			: base(Path) {
			this.FileType = FileType.PDF;
		}
	}
}