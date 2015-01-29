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
		/// <param name="Path">The path.</param>
		/// <returns>The created element</returns>
		/// <exception cref="System.Exception">File ' + Path + ' doesn't exist</exception>
		public IElement Create(string Path) {
			IElement Element;
			string Name = this.GetName(Path);
			string Extension = this.GetExtension(Name);

			if (!File.Exists(Path)) {
				throw new Exception("File '" + Path + "' doesn't exist");
			}

			switch (Extension) {
				case ".pdf":
					Element = new PDF(Path, Name, Extension);
					break;
				case ".png":
				case ".bmp":
				case ".jpg":
				case ".gif":
				case ".svg":
				case ".tiff":
				case ".jpeg":
					Element = new Image(Path, Name, Extension);
					break;
				case ".mp3":
					Element = new Sound(Path, Name, Extension);
					break;
				case ".avi":
				case ".mp4":
					Element = new Video(Path, Name, Extension);
					break;
				case ".txt":
					Element = new Text(Path, Name, Extension);
					break;
				default:
					Element = new Regular(Path, Name, Extension);
					break;
			}
			return (Element);
		}
	}
}
