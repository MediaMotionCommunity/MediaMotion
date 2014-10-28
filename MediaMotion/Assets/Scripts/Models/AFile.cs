using System;

public abstract class AFile : IFile
{
		private ElementType elementType = ElementType.File;
		protected FileType fileType;

		public ElementType getElementType ()
		{
				return this.elementType;
		}
	
		public FileType getFileType ()
		{
				return this.fileType;
		}
}


