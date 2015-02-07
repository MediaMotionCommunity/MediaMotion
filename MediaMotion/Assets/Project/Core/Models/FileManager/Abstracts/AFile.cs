using System;
using System.IO;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	/// <summary>
	/// Abstract File
	/// </summary>
	public abstract class AFile : AElement, IFile {
		/// <summary>
		/// The extension
		/// </summary>
		private FileInfo fileInfo;

		/// <summary>
		/// The file type
		/// </summary>
		protected FileType fileType;

		/// <summary>
		/// Initializes a new instance of the <see cref="AFile" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <param name="fileType">Type of the file.</param>
		/// <exception cref="System.NullReferenceException">fileInfo must not be null</exception>
		public AFile(FileInfo fileInfo, FileType fileType = FileType.Regular)
			: base(ElementType.File) {
			if (fileInfo == null) {
				throw new NullReferenceException("fileInfo must not be null");
			}
			this.fileInfo = fileInfo;
			this.fileType = fileType;
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <returns>The parent folder</returns>
		public sealed override string GetParent() {
			return (this.fileInfo.DirectoryName);
		}

		/// <summary>
		/// Gets the type of the file.
		/// </summary>
		/// <returns>The element Type</returns>
		public FileType GetFileType() {
			return (this.fileType);
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The path of the element</returns>
		public sealed override string GetPath() {
			return (this.fileInfo.FullName);
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The name of the element</returns>
		public sealed override string GetName() {
			return (this.fileInfo.Name);
		}

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <returns>The element extension</returns>
		public string GetExtension() {
			return (this.fileInfo.Extension);
		}
	}
}