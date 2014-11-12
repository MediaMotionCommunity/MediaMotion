using System;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	public class FileFactory : IFactory {
		public IElement Create(string Path) {
			IElement Element;
            int index;

			if (!File.Exists(Path)) {
				throw new Exception("File '" + Path + "' doesn't exist");
			}
            index = Path.LastIndexOf('.');
            if (index < 0)
            {
                Element = new Regular(Path);
            }
            else
            {
                switch (Path.Substring(Path.LastIndexOf('.')).ToLower())
                {
                    case ".pdf":
                        Element = new PDF(Path);
                        break;
                    case ".png":
                    case ".bmp":
                    case ".jpg":
                    case ".gif":
                    case ".svg":
                    case ".tiff":
                    case ".jpeg":
                        Element = new Image(Path);
                        break;
                    case ".mp3":
                        Element = new Sound(Path);
                        break;
                    case ".avi":
                    case ".mp4":
                        Element = new Video(Path);
                        break;
                    case ".txt":
                        Element = new Text(Path);
                        break;
                    default:
                        Element = new Regular(Path);
                        break;
                }
            }
			return (Element);
		}
	}
}
