using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class PDF : AFile {
		public PDF(string Path, string Name, string Extension)
			: base(Path, Name, Extension) {
			this.FileType = FileType.PDF;
		}
	}
}