using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Text : AFile {
		public Text() {
			this.fileType = FileType.Text;
		}
	}
}