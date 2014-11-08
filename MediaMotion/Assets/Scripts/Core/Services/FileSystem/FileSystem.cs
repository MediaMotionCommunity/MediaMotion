using System.Collections.Generic;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	public class FileSystem : IFileSystem {
		private IFolder WorkingDirectory;

		public FileSystem() {

		}

		public string getWorkingDirectory() {
			return (this.WorkingDirectory.getPath());
		}

		public void ChangeDirectory(Models.Interfaces.IFolder Folder) {
			throw new System.NotImplementedException();
		}

		public List<IElement> getDirectoryContent(Models.Interfaces.IFolder Folder = null) {
			throw new System.NotImplementedException();
		}

		public void Copy(IElement Element, IFolder Destination) {
			throw new System.NotImplementedException();
		}

		public void Move(IElement Element, IFolder Destination) {
			throw new System.NotImplementedException();
		}

		public void Remove(IElement Element) {
			throw new System.NotImplementedException();
		}
	}
}
