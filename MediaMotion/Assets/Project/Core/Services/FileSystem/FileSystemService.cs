using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	/// <summary>
	/// FileSystem Service
	/// </summary>
	public sealed class FileSystemService : IFileSystemService {
		/// <summary>
		/// The file factory
		/// </summary>
		private IElementFactory elementFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemService" /> class.
		/// </summary>
		/// <param name="elementFactory">The element factory.</param>
		public FileSystemService(IElementFactory elementFactory) {
			this.elementFactory = elementFactory;
			this.DisplayHidden = false;
			this.InitialFolder = this.CurrentFolder = this.elementFactory.CreateFolder(Directory.GetCurrentDirectory());
		}

		/// <summary>
		/// Gets the initial folder.
		/// </summary>
		/// <value>
		/// The initial folder.
		/// </value>
		public IFolder InitialFolder { get; private set; }

		/// <summary>
		/// Gets the current folder.
		/// </summary>
		/// <value>
		/// The current folder.
		/// </value>
		public IFolder CurrentFolder { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether [display hidden].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display hidden]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplayHidden { get; set; }

		/// <summary>
		/// Get the home path
		/// </summary>
		/// <returns>Home path</returns>
		public string GetHome() {
			return ((Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty) ? (Environment.GetFolderPath(Environment.SpecialFolder.Personal)) : (Environment.GetFolderPath(Environment.SpecialFolder.System)));
		}

		/// <summary>
		/// Gets the home folder.
		/// </summary>
		/// <returns>
		/// the home folder
		/// </returns>
		public IFolder GetHomeFolder() {
			return (this.elementFactory.CreateFolder(this.GetHome()));
		}

		/// <summary>
		/// Change directory to the Home or the <see cref="IFolder" /> provide in parameter
		/// </summary>
		/// <param name="folder">The folder.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool ChangeDirectory(string folder = null) {
			folder = folder ?? this.GetHome();

			if (!Directory.Exists(folder)) {
				return (false);
			}
			this.CurrentFolder = this.elementFactory.CreateFolder(folder);
			return (true);
		}

		/// <summary>
		/// Get the content of the current directory or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="path">
		/// A specific folder to use
		/// </param>
		/// <returns>
		/// List of elements
		/// </returns>
		public List<IElement> GetContent(string path = null) {
			List<IElement> directoryContent = new List<IElement>();
			DirectoryInfo directory = new DirectoryInfo(path ?? this.CurrentFolder.GetPath());

			foreach (DirectoryInfo directoryInfos in directory.GetDirectories().Where(file => (file.Attributes & FileAttributes.Hidden) == 0 || this.DisplayHidden)) {
				directoryContent.Add(this.elementFactory.CreateFolder(directoryInfos.FullName));
			}
			foreach (FileInfo fileInfos in directory.GetFiles().Where(file => (file.Attributes & FileAttributes.Hidden) == 0 || this.DisplayHidden)) {
				directoryContent.Add(this.elementFactory.CreateFile(fileInfos.FullName));
			}
			return (directoryContent);
		}

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <param name="filterExtension">The filter extension.</param>
		/// <param name="path">The path.</param>
		/// <returns>
		/// List of files
		/// </returns>
		public List<IFile> GetContent(string[] filterExtension, string path) {
			List<IFile> directoryContent = new List<IFile>();

			foreach (string filePath in Directory.GetFiles(path ?? this.CurrentFolder.GetPath()).Where(file => filterExtension.Contains(Path.GetExtension(file)))) {
				directoryContent.Add(this.elementFactory.CreateFile(filePath));
			}
			return (directoryContent);
		}

		/// <summary>
		/// Copy an <see cref="IElement"/> to the specific <see cref="IFolder"/>
		/// </summary>
		/// <param name="Element">
		/// The element to be copied
		/// </param>
		/// <param name="Destination">
		/// The folder destination
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Copy(IElement Element, IFolder Destination) {
			// FIXME Handle Directory copy
			File.Copy(Element.GetPath(), Path.Combine(Destination.GetPath(), Element.GetName()));

			return (true);
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
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Move(IElement Element, IFolder Destination) {
			string PathDestination = Path.Combine(Destination.GetPath(), Element.GetName());

			if (Element.GetElementType() == ElementType.File) {
				File.Move(Element.GetPath(), PathDestination);
			}
			else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Move(Element.GetPath(), PathDestination);
			}
			return (true);
		}

		/// <summary>
		/// Remove an element
		/// </summary>
		/// <param name="Element">
		/// The element to delete
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Remove(IElement Element) {
			if (Element.GetElementType() == ElementType.File) {
				File.Delete(Element.GetPath());
			}
			else if (Element.GetElementType() == ElementType.Folder) {
				Directory.Delete(Element.GetPath(), true);
			}
			return (true);
		}

		/// <summary>
		/// Restores an element which was remove earlier
		/// </summary>
		/// <param name="Element">
		/// The element.
		/// </param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		public bool Restore(IElement Element) {
			return (false);
		}
	}
}
