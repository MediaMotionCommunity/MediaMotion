using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Extensions;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models;
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
		private IElementFactory _elementFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemService" /> class.
		/// </summary>
		/// <param name="elementFactory">The element factory.</param>
		public FileSystemService(IElementFactory elementFactory) {
			this._elementFactory = elementFactory;
			this.InitialFolder = this._elementFactory.CreateFolder(Directory.GetCurrentDirectory());
			this.CurrentFolder = this.InitialFolder;
			this.BufferizedElements = null;
			this.DisplayHiddenElements = false;
			this.DisplaySystemElements = false;
		}

		/// <summary>
		/// Element action delegate
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="destination">The destination.</param>
		private delegate string elementAction(string source, string destination);

		/// <summary>
		/// Gets the initial folder.
		/// </summary>
		/// <value>
		///   The initial folder.
		/// </value>
		public IFolder InitialFolder { get; private set; }

		/// <summary>
		/// Gets the current folder.
		/// </summary>
		/// <value>
		///   The current folder.
		/// </value>
		public IFolder CurrentFolder { get; private set; }

		/// <summary>
		/// Gets the bufferized elements.
		/// </summary>
		/// <value>
		///   The bufferized elements.
		/// </value>
		public IBuffer BufferizedElements { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether [display hidden].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display hidden]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplayHiddenElements { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [display system files].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display system files]; otherwise, <c>false</c>.
		/// </value>
		public bool DisplaySystemElements { get; set; }

		/// <summary>
		/// Get the home path
		/// </summary>
		/// <returns>
		///   Home path
		/// </returns>
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
			return (this._elementFactory.CreateFolder(this.GetHome()));
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
			this.CurrentFolder = this._elementFactory.CreateFolder(folder);
			return (true);
		}

		/// <summary>
		/// Gets folder's elements
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <param name="onlyFiles">if set to <c>true</c> [only files].</param>
		/// <returns>
		///   An array with elements of the folder, <c>null</c> if an error occurred
		/// </returns>
		public IElement[] GetFolderElements(string path = null, string[] filterExtension = null, bool onlyFiles = false) {
			try {
				int i = 0;
				DirectoryInfo directoryInfo = new DirectoryInfo(path ?? this.CurrentFolder.GetPath());
				FileSystemInfo[] directoryElementsInfo = directoryInfo.GetFileSystemInfos();
				IEnumerable<FileSystemInfo> relevantDirectoryElementsInfo = directoryElementsInfo.Where(element => this.IsElementListable(element, filterExtension, onlyFiles));
				IElement[] directoryElements = new IElement[relevantDirectoryElementsInfo.Count()];

				foreach (FileSystemInfo elementInfo in relevantDirectoryElementsInfo) {
					directoryElements[i] = this._elementFactory.Create(elementInfo.FullName);
					++i;
				}
				return (directoryElements.ToArray());
			} catch (DirectoryNotFoundException) {
				// TODO Log e.Message
			}
			return (null);
		}

		/// <summary>
		/// Gets the folder files.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns>
		///   An array with files of the folder, <c>null</c> if an error occurred
		/// </returns>
		public IFile[] GetFolderFiles(string path = null, string[] filterExtension = null) {
			IElement[] elements = this.GetFolderElements(path, filterExtension);
			IFile[] files = null;

			if (elements != null) {
				files = new IFile[elements.Length];
				for (int i = 0; i < elements.Length; ++i) {
					files[i] = elements[i] as IFile;
				}
			}
			return (files);
		}

		/// <summary>
		/// Bufferizes the elements for copy
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Copy(IElement[] elements) {
			try {
				this.BufferizedElements = new MediaMotion.Core.Services.FileSystem.Models.Buffer(elements, false, false);
				return (true);
			} catch (ArgumentNullException) {
				// TODO Log
			}
			return (false);
		}

		/// <summary>
		/// Bufferizes the element for copy
		/// </summary>
		/// <param name="element"></param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Copy(IElement element) {
			return (this.Copy(new IElement[] { element }));
		}

		/// <summary>
		/// Bufferizes the elements for deplacement
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Cut(IElement[] elements) {
			try {
				this.BufferizedElements = new MediaMotion.Core.Services.FileSystem.Models.Buffer(elements, true, true);
				return (true);
			} catch (ArgumentNullException) {
				// TODO Log
			}
			return (false);
		}

		/// <summary>
		/// Bufferizes the element for deplacement
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Cut(IElement element) {
			return (this.Cut(new IElement[] { element }));
		}

		/// <summary>
		/// Pastes the specified destination.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <returns>
		///   <c>true</c> if the action succeed, <c>false</c> if not or if the buffer is empty
		/// </returns>
		public bool Paste(IFolder destination) {
			try {
				if (destination == null) {
					throw new ArgumentNullException("The destination must not be null");
				}
				if (this.BufferizedElements == null || this.BufferizedElements.Elements == null || this.BufferizedElements.Elements.Length == 0) {
					throw new ArgumentNullException("The buffer must not be null or empty");
				}
				elementAction elementActionFunc = this.GetRelevantElementAction(this.BufferizedElements);

				for (int i = 0; i < this.BufferizedElements.Elements.Length; ++i) {
					IElement newElement = this._elementFactory.Create(elementActionFunc(this.BufferizedElements.Elements[i].GetPath(), destination.GetPath()));

					if (this.BufferizedElements.DeleteElementsAfterPaste) {
						this.BufferizedElements.Elements[i] = newElement;
					}
				}
				if (this.BufferizedElements.DeleteBufferAfterPaste) {
					this.BufferizedElements = null;
				}
				return (true);
			} catch (ArgumentNullException) {
				// TODO Log
			}
			return (false);
		}

		/// <summary>
		/// Removes the specified element.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the deletion entirely succeed, <c>false</c> otherwise
		/// </returns>
		public bool Remove(IElement[] elements) {
			bool success = true;

			foreach (IElement element in elements) {
				switch (element.GetElementType()) {
					case ElementType.File:
						File.Delete(element.GetPath());
						break;
					case ElementType.Folder:
						Directory.Delete(element.GetPath(), true);
						break;
					default:
						// TODO Log
						success = false;
						break;
				}
			}
			return (success);
		}

		/// <summary>
		/// Removes the specified element.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the deletion succeed, <c>false</c> otherwise
		/// </returns>
		public bool Remove(IElement element) {
			return (this.Remove(new IElement[] { element }));
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

		/// <summary>
		/// Determines whether [is element listable] [the specified element infos].
		/// </summary>
		/// <param name="elementInfo">The element infos.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns><c>true</c> if the element is listable, <c>false</c> otherwise</returns>
		private bool IsElementListable(FileSystemInfo elementInfo, string[] filterExtension, bool onlyFiles) {
			return ((!onlyFiles || !elementInfo.HasAttribute(FileAttributes.Directory)) &&
					(filterExtension == null || filterExtension.Contains(Path.GetExtension(elementInfo.FullName))) &&
					(this.DisplayHiddenElements || !elementInfo.HasAttribute(FileAttributes.Hidden)) &&
					(this.DisplaySystemElements || !elementInfo.HasAttribute(FileAttributes.System)));
		}

		/// <summary>
		/// Gets the relevant element action.
		/// </summary>
		/// <param name="buffer">The buffer.</param>
		/// <returns>
		///   The relevant elementAction for buffer treatment
		/// </returns>
		private elementAction GetRelevantElementAction(IBuffer buffer) {
			if (buffer.DeleteElementsAfterPaste) {
				return (this.MoveElement);
			} else {
				return (this.CopyElement);
			}
		}

		/// <summary>
		/// Copies the element.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="destination">The destination.</param>
		private string CopyElement(string source, string destination) {
			if (Directory.Exists(source)) {
				DirectoryInfo directoryInfo = new DirectoryInfo(source);

				destination = Path.Combine(destination, directoryInfo.Name);
				Directory.CreateDirectory(destination);
				foreach (FileSystemInfo fileSystemInfo in directoryInfo.GetFileSystemInfos()) {
					this.CopyElement(fileSystemInfo.FullName, destination);
				}
			} else if (File.Exists(source)) {
				FileInfo fileInfo = new FileInfo(source);

				destination = Path.Combine(destination, fileInfo.Name);
				File.Copy(fileInfo.FullName, destination, true);
			}
			return (destination);
		}

		/// <summary>
		/// Moves the element.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="destination">The destination.</param>
		private string MoveElement(string source, string destination) {
			if (Directory.Exists(source)) {
				DirectoryInfo directoryInfo = new DirectoryInfo(source);

				destination = Path.Combine(destination, directoryInfo.Name);
				if (!Directory.Exists(destination)) {
					directoryInfo.MoveTo(destination);
				} else {
					throw new Exception("Already exist");
				}
			} else if (File.Exists(source)) {
				FileInfo fileInfo = new FileInfo(source);

				destination = Path.Combine(destination, fileInfo.Name);
				if (!Directory.Exists(destination)) {
					fileInfo.MoveTo(destination);
				} else {
					throw new Exception("Already exist");
				}
			}
			return (destination);
		}
	}
}
