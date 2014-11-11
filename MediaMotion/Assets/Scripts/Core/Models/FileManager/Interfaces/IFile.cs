using System;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	public interface IFile : IElement {
		FileType GetFileType();

		string GetExtension();
	}
}