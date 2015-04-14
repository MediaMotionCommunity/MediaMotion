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
		/// <returns>
		///   An array with elements of the folder, <c>null</c> if an error occurred
		/// </returns>
		public IElement[] GetFolderElements(string path = null, string[] filterExtension = null) {
			try {
				DirectoryInfo directoryInfo = new DirectoryInfo(path ?? this.CurrentFolder.GetPath());
				FileSystemInfo[] directoryElementsInfo = directoryInfo.GetFileSystemInfos();
				List<IElement> directoryElements = new List<IElement>();

				foreach (FileSystemInfo elementInfo in directoryElementsInfo.Where(element => this.IsElementListable(element, filterExtension))) {
					if (elementInfo.HasAttribute(FileAttributes.Device)) {
						// NOTICE Device not supported
					} else if (elementInfo.HasAttribute(FileAttributes.Directory)) {
						directoryElements.Add(this._elementFactory.CreateFolder(elementInfo.FullName));
					} else {
						directoryElements.Add(this._elementFactory.CreateFile(elementInfo.FullName));
					}
				}
				return (directoryElements.ToArray());
			} catch (DirectoryNotFoundException) {
				// TODO Log e.Message
			}
			return (null);
		}

		/// <summary>
		/// Bufferizes the elements for copy
		/// </summary>
		/// <param name="elements">The elements.</param>
		public void Copy(IElement[] elements) {
			this.BufferizedElements = new MediaMotion.Core.Services.FileSystem.Models.Buffer(elements, false, false);
		}

		/// <summary>
		/// Bufferizes the element for copy
		/// </summary>
		/// <param name="elements">The elements.</param>
		public void Copy(IElement element) {
			this.Copy(new IElement[] { element });
		}

		/// <summary>
		/// Bufferizes the elements for deplacement
		/// </summary>
		/// <param name="elements">The elements.</param>
		public void Cut(IElement[] elements) {
			this.BufferizedElements = new MediaMotion.Core.Services.FileSystem.Models.Buffer(elements, true, true);
		}

		/// <summary>
		/// Bufferizes the element for deplacement
		/// </summary>
		/// <param name="element">The element.</param>
		public void Cut(IElement element) {
			this.Cut(new IElement[] { element });
		}

		/// <summary>
		/// Pastes the specified destination.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <returns>
		///   <c>true</c> if the action succeed, <c>false</c> if not or if the buffer is empty
		/// </returns>
		public bool Paste(IFolder destination) {
			if (this.BufferizedElements == null) {
				return (false);
			}

			int i = 0;
			IElement[] elements = new IElement[this.BufferizedElements.Elements.Length];
			elementAction elementActionFunc = this.CopyElement;

			if (this.BufferizedElements.DeleteElementsAfterPaste) {
				elementActionFunc = this.MoveElement;
			}
			foreach (IElement element in this.BufferizedElements.Elements) {
				elements[i++] = this._elementFactory.Create(elementActionFunc(element.GetPath(), destination.GetPath()));
			}
			switch (this.BufferizedElements.DeleteBufferAfterPaste) {
				case true:
					this.BufferizedElements = null;
					break;
				case !true:
					if (this.BufferizedElements.DeleteElementsAfterPaste) {
						this.BufferizedElements = new MediaMotion.Core.Services.FileSystem.Models.Buffer(elements, this.BufferizedElements.DeleteElementsAfterPaste, this.BufferizedElements.DeleteBufferAfterPaste);
					}
					break;
			}
			return (true);
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
		private bool IsElementListable(FileSystemInfo elementInfo, string[] filterExtension) {
			return ((filterExtension == null || filterExtension.Contains(Path.GetExtension(elementInfo.FullName))) &&
					(this.DisplayHiddenElements || !elementInfo.HasAttribute(FileAttributes.Hidden)) &&
					(this.DisplaySystemElements || !elementInfo.HasAttribute(FileAttributes.System)));
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
