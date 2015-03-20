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
		/// <param name="path">The path.</param>
		/// <returns>
		/// The created element
		/// </returns>
		public IElement Create(string path) {
			try {
				DirectoryInfo directoryInfo = new DirectoryInfo(path);

				if (directoryInfo == null) {
					throw new ArgumentException("argument 'path' must be a valid path");
				}
				return (new Folder(directoryInfo));
			} catch (ArgumentException) {
			}
			return (null);
		}
	}
}
