using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	public class FileSystem : IFileSystem {
		private IFolder WorkingDirectory;

		public FileSystem() {

		}

		/// <summary>
		/// Get the working directory
		/// </summary>
		/// <returns>
		/// Path of working directory
		/// </returns>
		public string GetWorkingDirectory() {
			return (this.WorkingDirectory.GetPath());
		}

		/// <summary>
		/// Change directory to the Home or the specific Folder
		/// </summary>
		/// <param name="Folder">
		/// New working directory
		/// </param>
		public void ChangeDirectory(IFolder Folder = null) {
			string Path = ((Folder != null) ? (Folder.GetPath()) : (Environment.GetFolderPath(Environment.SpecialFolder.Personal)));

			Directory.SetCurrentDirectory(Path);
		}

		/// <summary>
		/// Get the content of the current directory or the directory provide in parameter
		/// </summary>
		/// <param name="Folder">
		/// A specific folder to use
		/// </param>
		/// <returns>
		/// List of elements
		/// </returns>
		public List<IElement> GetDirectoryContent(IFolder Folder = null) {
			List<IElement> DirectoryContent = new List<IElement>();
			string Path = ((Folder != null) ? (Folder.GetPath()) : (this.GetWorkingDirectory()));

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
			// FIXME Handle Directory move
			File.Move(Element.GetPath(), Path.Combine(Destination.GetPath(), Element.GetName()));
		}

		public void Remove(IElement Element) {
			// FIXME Handle Directory remove
			File.Delete(Element.GetPath());
		}
	}
}
