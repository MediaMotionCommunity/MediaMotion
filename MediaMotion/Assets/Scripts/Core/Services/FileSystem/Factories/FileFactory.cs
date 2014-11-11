using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	public class FileFactory : IFactory {
		public IElement Create(string Path) {
			// TODO Check extension
			return (new Regular(Path));
		}
	}
}
