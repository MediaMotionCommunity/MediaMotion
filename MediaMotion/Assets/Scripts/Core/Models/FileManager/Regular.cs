using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models {
	public class Regular : AFile {
		public Regular(string Path)
			: base(Path) {
			this.FileType = FileType.Regular;
		}
	}
}