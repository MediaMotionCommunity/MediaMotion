using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;
namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	public interface IFileSystem {
		string getWorkingDirectory();
		void ChangeDirectory(IFolder Folder);

		List<IElement> getDirectoryContent(IFolder Folder = null);

		void Copy(IElement Element, IFolder Destination);
		void Move(IElement Element, IFolder Destination);
		void Remove(IElement Element);
	}
}
