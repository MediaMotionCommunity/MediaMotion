using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.FileSystem {
	sealed public class FileSystem : IFileSystem {
		private IFactory FolderFactory;
		private IFactory FileFactory;
		public IFolder InitialFolder { get; private set; }

		public FileSystem() {
			this.FolderFactory = new FolderFactory();
			this.FileFactory = new FileFactory();
			this.InitialFolder = this.GetWorkingDirectory();
		}

		public IFolder GetWorkingDirectory() {
			return (this.FolderFactory.Create(Directory.GetCurrentDirectory()) as IFolder);
		}

		public IFolder GetHomeDirectory() {
			return (this.FolderFactory.Create(Environment.GetFolderPath(Environment.SpecialFolder.Personal)) as IFolder);
		}

		public void ChangeDirectory(IFolder Folder = null) {
			Directory.SetCurrentDirectory(((Folder != null) ? (Folder) : (this.GetHomeDirectory())).GetPath());
		}

		public List<IElement> GetDirectoryContent(IFolder Folder = null) {
			List<IElement> DirectoryContent = new List<IElement>();
			string Path = ((Folder != null) ? (Folder) : (this.GetWorkingDirectory())).GetPath();

			foreach (string DirectoryPath in Directory.GetDirectories(Path)) {
				DirectoryContent.Add(this.FolderFactory.Create(DirectoryPath));
			}
			foreach (string FilePath in Directory.GetFiles(Path)) {
				DirectoryContent.Add(this.FileFactory.Create(FilePath));
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
