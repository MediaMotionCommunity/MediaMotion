using System;
using System.Collections.Generic;
using System.IO;
using MediaMotion.Core.Models;
using MediaMotion.Core.Models.Enums;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	public class FileSystem : IFileSystem {
		/// <summary>
		/// Constructor
		/// </summary>
		public FileSystem() {

		}

		/// <summary>
		/// Get the working directory
		/// </summary>
		/// <returns>
		/// Working directory
		/// </returns>
		public IFolder GetWorkingDirectory() {
			return (new Folder(Directory.GetCurrentDirectory()));
		}

		/// <summary>
		/// Get the home directory
		/// </summary>
		/// <returns>
		/// Home directory
		/// </returns>
		public IFolder GetHomeDirectory() {
			return (new Folder(Environment.GetFolderPath(Environment.SpecialFolder.Personal)));
		}

		/// <summary>
		/// Change directory to the Home or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="Folder">
		/// New working directory
		/// </param>
		public void ChangeDirectory(IFolder Folder = null) {
			Directory.SetCurrentDirectory(((Folder != null) ? (Folder) : (this.GetHomeDirectory())).GetPath());
		}

		/// <summary>
		/// Get the content of the current directory or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="Folder">
		/// A specific folder to use
		/// </param>
		/// <returns>
		/// List of elements
		/// </returns>
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

		/// <summary>
		/// Copy an <see cref="IElement"/> to the specific <see cref="IFolder"/>
		/// </summary>
		/// <param name="Element"></param>
		/// <param name="Destination"></param>
		public void Copy(IElement Element, IFolder Destination) {
			// FIXME Handle Directory copy
			File.Copy(Element.GetPath(), Path.Combine(Destination.GetPath(), Element.GetName()));
		}

		/// <summary>
		/// Move an <see cref="IElement"/> in a different <see cref="IFolder"/>
		/// </summary>
		/// <param name="Element">
		/// The element to be move
		/// </param>
		/// <param name="Destination">
		/// The folder destination
		/// </param>
		public void Move(IElement Element, IFolder Destination) {
			string PathDestination = Path.Combine(Destination.GetPath(), Element.GetName());

			if (Element.GetElementType() == ElementType.File) {
				File.Move(Element.GetPath(), PathDestination);
			} else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Move(Element.GetPath(), PathDestination);
			}
		}

		/// <summary>
		/// Remove an element
		/// </summary>
		/// <param name="Element">
		/// The element to delete
		/// </param>
		public void Remove(IElement Element) {
			if (Element.GetElementType() == ElementType.File) {
				File.Delete(Element.GetPath());
			} else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Delete(Element.GetPath(), true);
			}
		}
	}
}
