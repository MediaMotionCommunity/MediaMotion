using MediaMotion.Core.Models.Enums;
using MediaMotion.Core.Models.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	public class Folder : IFolder {
		private string Path;

		public Folder(string Path) {
			this.Path = Path;
		}
		
		public ElementType GetElementType() {
			return (ElementType.Folder);
		}

		public string GetPath() {
			return (this.Path);
		}

		public string GetName() {
			throw new System.NotImplementedException();
		}
	}
}
