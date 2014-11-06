using System;
using MediaMotion.Core.Models.Enums;
using MediaMotion.Core.Models.Interfaces;

namespace MediaMotion.Core.Models.Abstract {
	public abstract class AFile : IFile {
		private ElementType elementType = ElementType.File;
		protected FileType fileType;

		public ElementType getElementType() {
			return this.elementType;
		}

		public FileType getFileType() {
			return this.fileType;
		}
	}
}