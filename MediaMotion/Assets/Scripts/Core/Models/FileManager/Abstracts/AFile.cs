using System;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager.Abstract {
	public abstract class AFile : IFile {
		protected FileType FileType;
		private string Path;

		public AFile(string Path) {
			this.Path = Path;
		}

		public ElementType GetElementType() {
			return (ElementType.File);
		}

		public FileType GetFileType() {
			return (this.FileType);
		}

		public string GetPath() {
			throw new NotImplementedException();
		}

		public string GetName() {
			throw new NotImplementedException();
		}

		public string GetExtension() {
			throw new NotImplementedException();
		}
	}
}