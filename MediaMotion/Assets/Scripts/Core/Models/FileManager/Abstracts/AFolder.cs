using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	abstract public class AFolder : AElement, IFolder {
		public AFolder(string Path, string Name)
			: base(ElementType.Folder, Path, Name) {

		}
	}
}
