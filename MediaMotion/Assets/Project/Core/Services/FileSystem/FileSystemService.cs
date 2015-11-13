using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes;
using MediaMotion.Core.Services.FileSystem.Extensions;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using Buffer = MediaMotion.Core.Services.FileSystem.Models.Buffer;

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
		/// The buffer access
		/// </summary>
		private object bufferAccess;

		/// <summary>
		/// The root path
		/// </summary>
		private string rootPath;

		/// <summary>
		/// Initializes a new instance of the <see cref="FileSystemService" /> class.
		/// </summary>
		/// <param name="elementFactory">The element factory.</param>
		public FileSystemService(IElementFactory elementFactory, [Parameter("CommandLineOptionsRoot")] string rootPath = null) {
			this.elementFactory = elementFactory;
			this.rootPath = rootPath;
			this.bufferAccess = new object();
			this.InitialFolder = this.elementFactory.CreateFolder(Directory.GetCurrentDirectory());
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
		/// <returns>
		///   The path of the new element
		/// </returns>
		private delegate string ElementAction(string source, string destination);

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
		/// Determines whether this instance has chrooted.
		/// </summary>
		/// <returns>true if the programm is chrooted, false otherwise.</returns>
		public bool IsChrooted() {
			return (this.rootPath != null);
		}

		/// <summary>
		/// Gets the root.
		/// </summary>
		/// <returns>The root path</returns>
		public string GetRoot() {
			return (this.rootPath);
		}

		/// <summary>
		/// Determines whether the specified path is accessible.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>true if the path is accessible, false otherwise.</returns>
		public bool IsAccessible(string path) {
			return (path != null && (!this.IsChrooted() || (!path.Contains("..") && path.StartsWith(this.GetRoot()))));
		}

		/// <summary>
		/// Get the home path
		/// </summary>
		/// <returns>
		///   Home path
		/// </returns>
		public string GetHome() {
			string homePath = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) != string.Empty) ? (Environment.GetFolderPath(Environment.SpecialFolder.Personal)) : (Environment.GetFolderPath(Environment.SpecialFolder.System));

			if (!this.IsAccessible(homePath)) {
				homePath = this.GetRoot();
			}
			return (homePath);
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
			if (!this.IsAccessible(folder)) {
				return (false);
			}
			this.CurrentFolder = this.elementFactory.CreateFolder(folder);
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
				if (filterExtension != null && !filterExtension.All(extension => extension != null && extension.StartsWith("."))) {
					throw new ArgumentException("filterExtension must contain valid extensions only.");
				}
				int i = 0;
				path = path ?? this.CurrentFolder.GetPath();
				if (!this.IsAccessible(path)) {
					throw new Exception("Not accessible");
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileSystemInfo[] directoryElementsInfo = directoryInfo.GetFileSystemInfos();
				IEnumerable<FileSystemInfo> relevantDirectoryElementsInfo = directoryElementsInfo.Where(element => this.IsElementBrowsable(element, filterExtension, onlyFiles));
				IElement[] directoryElements = new IElement[relevantDirectoryElementsInfo.Count()];

				foreach (FileSystemInfo elementInfo in relevantDirectoryElementsInfo) {
					directoryElements[i] = this.elementFactory.Create(elementInfo.FullName);
					++i;
				}
				return (directoryElements.ToArray());
			} catch (ArgumentException) {
				// TODO Log e.Message
			} catch (DirectoryNotFoundException) {
				// TODO Log e.Message
			} catch (Exception e) {
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
			lock (this.bufferAccess) {
				try {
					this.BufferizedElements = new Buffer(elements, false, false);
					return (true);
				} catch (ArgumentNullException) {
					// TODO Log
				}
				return (false);
			}
		}

		/// <summary>
		/// Bufferizes the element for copy
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Copy(IElement element) {
			return (this.Copy(new IElement[] { element }));
		}

		/// <summary>
		/// Bufferizes the elements for movement
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		public bool Cut(IElement[] elements) {
			lock (this.bufferAccess) {
				try {
					this.BufferizedElements = new Buffer(elements, true, true);
					return (true);
				} catch (ArgumentNullException) {
					// TODO Log
				}
				return (false);
			}
		}

		/// <summary>
		/// Bufferizes the element for movement
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
		///   <c>true</c> if the action succeed, <c>false</c> if the destination is null, the buffer is null or empty, or if an error occurred
		/// </returns>
		public bool Paste(IFolder destination) {
			lock (this.bufferAccess) {
				try {
					if (destination == null) {
						throw new ArgumentNullException("The destination must not be null");
					}
					if (this.IsBufferEmpty()) {
						throw new ArgumentNullException("The buffer must not be empty");
					}
					ElementAction elementActionFunc = this.GetRelevantElementAction(this.BufferizedElements);

					for (int i = 0; i < this.BufferizedElements.Elements.Length; ++i) {
						IElement newElement = this.elementFactory.Create(elementActionFunc(this.BufferizedElements.Elements[i].GetPath(), destination.GetPath()));

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
		/// <param name="element">The element.</param>
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
		/// Determines whether [is buffer empty].
		/// </summary>
		/// <returns>
		///   <c>true</c> if the buffer is empty, <c>false</c> otherwise
		/// </returns>
		public bool IsBufferEmpty() {
			return (this.BufferizedElements == null || this.BufferizedElements.Elements == null || this.BufferizedElements.Elements.Length == 0);
		}

		/// <summary>
		/// Determines whether [is element browsable] [the specified element information].
		/// </summary>
		/// <param name="elementInfo">The element information.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <param name="onlyFiles">if set to <c>true</c> [only files].</param>
		/// <returns>
		///   <c>true</c> if the element is browsable, <c>false</c> otherwise
		/// </returns>
		private bool IsElementBrowsable(FileSystemInfo elementInfo, string[] filterExtension, bool onlyFiles) {
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
		private ElementAction GetRelevantElementAction(IBuffer buffer) {
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
		/// <returns>
		///   The path of the new element
		/// </returns>
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
		/// <returns>
		///   The path of the new element
		/// </returns>
		private string MoveElement(string source, string destination) {
			if (Directory.Exists(source)) {
				DirectoryInfo directoryInfo = new DirectoryInfo(source);

				destination = Path.Combine(destination, directoryInfo.Name);
				if (!Directory.Exists(destination)) {
					directoryInfo.MoveTo(destination);
				} else {
					// TODO Change exception
					throw new Exception("Already exist");
				}
			} else if (File.Exists(source)) {
				FileInfo fileInfo = new FileInfo(source);

				destination = Path.Combine(destination, fileInfo.Name);
				if (!Directory.Exists(destination)) {
					fileInfo.MoveTo(destination);
				} else {
					// TODO Change exception
					throw new Exception("Already exist");
				}
			}
			return (destination);
		}
	}
}
