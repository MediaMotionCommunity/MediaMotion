using System;
using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Video : AFile {
		public Video(string Path)
			: base(Path) {
			this.FileType = FileType.Video;
		}
	}
}