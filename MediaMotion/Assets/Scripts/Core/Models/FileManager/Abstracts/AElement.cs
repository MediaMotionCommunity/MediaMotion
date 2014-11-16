using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	abstract public class AElement : IElement {
		private ElementType ElementType;
		private string Path;
		private string Name;

		public AElement(ElementType ElementType, string Path, string Name) {
			this.ElementType = ElementType;
			this.Path = Path;
			this.Name = Name;
		}

		public ElementType GetElementType() {
			return (this.ElementType);
		}

		public string GetPath() {
			return (this.Path);
		}

		public string GetName() {
			return (this.Name);
		}
	}
}
