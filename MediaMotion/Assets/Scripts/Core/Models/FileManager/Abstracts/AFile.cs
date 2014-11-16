using System;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	abstract public class AFile : AElement, IFile {
		protected FileType FileType;
		private string Extension;

		public AFile(string Path, string Name, string Extension)
			: base(ElementType.File, Path, Name) {
			this.Extension = Extension;
		}

		public FileType GetFileType() {
			return (this.FileType);
		}

		public string GetExtension() {
			return (this.Extension);
		}
	}
}