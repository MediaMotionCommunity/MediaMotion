using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Factories;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem {
	/// <summary>
	/// FileSystem Service
	/// </summary>
	public sealed class FileSystemService : IFileSystemService {
		/// <summary>
		/// The folder factory
		/// </summary>
		private IFactory FolderFactoryInstance;

		/// <summary>
		/// The file factory
		/// </summary>
		private IFactory FileFactoryInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemService"/> class.
		/// </summary>
		public FileSystemService() {
			this.DisplayHidden = false;
			this.FileFactory = new FileFactory();
			this.FolderFactory = new FolderFactory();
			this.InitialFolder = this.CurrentFolder = this.FolderFactory.Create(Directory.GetCurrentDirectory()) as IFolder;
		}

		/// <summary>
		/// Gets or sets the folder factory.
		/// </summary>
		/// <value>
		/// The folder factory.
		/// </value>
		public IFactory FolderFactory {
			private get {
				return (this.FolderFactoryInstance);
			}
			set {
				if (value != null) {
					this.FolderFactoryInstance = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the file factory.
		/// </summary>
		/// <value>
		/// The file factory.
		/// </value>
		public IFactory FileFactory {
			private get {
				return (this.FileFactoryInstance);
			}
			set {
				if (value != null) {
					this.FileFactoryInstance = value;
				}
			}
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
		/// Change directory to the Home or the <see cref="IFolder" /> provide in parameter
		/// </summary>
		/// <param name="folder">The folder.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		public bool ChangeDirectory(string folder = null) {
			folder = folder ?? this.GetHome();

			if (!Directory.Exists(folder)) {
				return (false);
			}
			this.CurrentFolder = this.FolderFactory.Create(folder) as IFolder;
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
				directoryContent.Add(this.FolderFactory.Create(directoryInfos.FullName));
			}
			foreach (FileInfo fileInfos in directory.GetFiles().Where(file => (file.Attributes & FileAttributes.Hidden) == 0 || this.DisplayHidden)) {
				directoryContent.Add(this.FileFactory.Create(fileInfos.FullName));
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
				directoryContent.Add(this.FileFactory.Create(filePath) as IFile);
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
