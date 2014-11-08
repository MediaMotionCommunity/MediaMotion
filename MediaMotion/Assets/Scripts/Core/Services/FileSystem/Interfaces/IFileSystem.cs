using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;
namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	public interface IFileSystem {
		string GetWorkingDirectory();
		void ChangeDirectory(IFolder Folder);

		List<IElement> GetDirectoryContent(IFolder Folder = null);

		void Copy(IElement Element, IFolder Destination);
		void Move(IElement Element, IFolder Destination);
		void Remove(IElement Element);
	}
}
