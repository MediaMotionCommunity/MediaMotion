using System;
using MediaMotion.Core.Models.Abstract;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models {
	public class Video : AFile {
		public Video(string Path)
			: base(Path) {
			this.FileType = FileType.Video;
		}
	}
}