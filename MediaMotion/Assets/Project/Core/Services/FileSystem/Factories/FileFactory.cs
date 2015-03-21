using System;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	/// <summary>
	/// File Factory
	/// </summary>
	public class FileFactory : AFactory, IFactory {
		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The created element</returns>
		/// <exception cref="System.Exception">File ' + Path + ' doesn't exist</exception>
		public IElement Create(string path) {
			FileInfo file = new FileInfo(path);

			if (file == null) {
				throw new Exception("File '" + path + "' doesn't exist");
			}
			switch (file.Extension.ToLower()) {
				case ".pdf":
					return (new PDF(file));
				case ".png":
				case ".bmp":
				case ".jpg":
				case ".gif":
				case ".svg":
				case ".tiff":
				case ".jpeg":
					return (new Image(file));
				case ".mp3":
					return (new Sound(file));
				case ".mkv":
				case ".avi":
				case ".mp4":
					return (new Video(file));
				case ".txt":
					return (new Text(file));
			}
			return (new Regular(file));
		}
	}
}
