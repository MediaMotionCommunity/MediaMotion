using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	public class Folder : AFolder, IFolder {
		public Folder(string Path, string Name)
			: base(Path, Name) {
		}
	}
}
