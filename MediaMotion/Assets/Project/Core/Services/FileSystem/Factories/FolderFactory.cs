using System;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	/// <summary>
	/// Folder Factory
	/// </summary>
	public class FolderFactory : AFactory, IFactory {
		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <returns>The created element</returns>
		/// <exception cref="System.Exception">Directory ' + Path + ' doesn't exist</exception>
		public IElement Create(string Path) {
			if (!Directory.Exists(Path)) {
				throw new Exception("Directory '" + Path + "' doesn't exist");
			}
			return (new Folder(Path, this.GetName(Path)));
		}
	}
}
