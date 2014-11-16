using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class Regular : AFile {
		public Regular(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.Regular;
		}
	}
}