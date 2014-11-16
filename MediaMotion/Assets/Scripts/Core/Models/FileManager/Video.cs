using System;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Video : AFile {
		public Video(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.Video;
		}
	}
}