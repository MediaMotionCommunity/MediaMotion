using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	public class FolderFactory : IFactory {
		public IElement Create(string Path) {
			return (new Folder(Path));
		}
	}
}
