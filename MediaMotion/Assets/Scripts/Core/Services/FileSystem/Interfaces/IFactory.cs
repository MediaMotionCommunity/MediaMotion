using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	public interface IFactory {
		IElement Create(string Path);
	}
}
