using MediaMotion.Core.Models.FileManager.Abstract;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models {
	public class Sound : AFile {
		public Sound(string Path)
			: base(Path) {
			this.FileType = FileType.Sound;
		}
	}
}