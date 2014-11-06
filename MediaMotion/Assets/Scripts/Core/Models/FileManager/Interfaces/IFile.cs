using System;
using MediaMotion.Core.Models.Enums;

namespace MediaMotion.Core.Models.Interfaces {
	public interface IFile : IElement {
		FileType getFileType();
	}
}