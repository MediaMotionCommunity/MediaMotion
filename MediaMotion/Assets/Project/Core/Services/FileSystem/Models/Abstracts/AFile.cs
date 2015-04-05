using System;
using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Models.Abstracts {
	/// <summary>
	/// Abstract File
	/// </summary>
	public abstract class AFile : AElement, IFile {
		/// <summary>
		/// The extension
		/// </summary>
		private readonly FileInfo fileInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="AFile" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		/// <param name="resourceId">The resource Id.</param>
		/// <exception cref="System.NullReferenceException">fileInfo must not be null</exception>
		protected AFile(FileInfo fileInfo, string resourceId = null)
			: base(ElementType.File, resourceId) {
			if (fileInfo == null) {
				throw new NullReferenceException("fileInfo must not be null");
			}
			this.fileInfo = fileInfo;
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <returns>The parent folder</returns>
		public sealed override string GetParent() {
			return (this.fileInfo.DirectoryName);
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