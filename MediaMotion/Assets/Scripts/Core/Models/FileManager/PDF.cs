using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager {
	public class PDF : AFile {
		public PDF(string Path)
			: base(Path) {
			this.FileType = FileType.PDF;
		}
	}
}