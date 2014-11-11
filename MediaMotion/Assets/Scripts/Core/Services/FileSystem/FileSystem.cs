using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	public class FileSystem : IFileSystem {
		public FileSystem() { }

		public IFolder GetWorkingDirectory() {
			return (new Folder(Directory.GetCurrentDirectory()));
		}

		public IFolder GetHomeDirectory() {
			return (new Folder(Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
		}

		public void ChangeDirectory(IFolder Folder = null) {
			Directory.SetCurrentDirectory(((Folder != null) ? (Folder) : (this.GetHomeDirectory())).GetPath());
		}

		public List<IElement> GetDirectoryContent(IFolder Folder = null) {
			List<IElement> DirectoryContent = new List<IElement>();
			string Path = ((Folder != null) ? (Folder) : (this.GetWorkingDirectory())).GetPath();

			foreach (string DirectoryPath in Directory.GetDirectories(Path)) {
				DirectoryContent.Add(new Folder(DirectoryPath));
			}
			foreach (string FilePath in Directory.GetFiles(Path)) {
				// TODO Check extension
				DirectoryContent.Add(new Regular(FilePath));
			}
			return (DirectoryContent);
		}

		public void Copy(IElement Element, IFolder Destination) {
			// FIXME Handle Directory copy
			File.Copy(Element.GetPath(), Path.Combine(Destination.GetPath(), Element.GetName()));
		}

		public void Move(IElement Element, IFolder Destination) {
			string PathDestination = Path.Combine(Destination.GetPath(), Element.GetName());

			if (Element.GetElementType() == ElementType.File) {
				File.Move(Element.GetPath(), PathDestination);
			} else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Move(Element.GetPath(), PathDestination);
			}
		}

		public void Remove(IElement Element) {
			if (Element.GetElementType() == ElementType.File) {
				File.Delete(Element.GetPath());
			} else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Delete(Element.GetPath(), true);
			}
		}
	}
}
