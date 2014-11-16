using System;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	public class FolderFactory : AFactory, IFactory {
		public IElement Create(string Path) {
			if (!Directory.Exists(Path)) {
				throw new Exception("Directory '" + Path + "' doesn't exist");
			}
			return (new Folder(Path, this.GetName(Path)));
		}
	}
}
