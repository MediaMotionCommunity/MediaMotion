using System;
using MediaMotion.Core.Models.FileManager;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	/// <summary>
	/// Abstract File
	/// </summary>
	public abstract class AFile : AElement, IFile {
		/// <summary>
		/// The file type
		/// </summary>
		protected FileType FileType;

		/// <summary>
		/// The extension
		/// </summary>
		private string Extension;

		/// <summary>
		/// Initializes a new instance of the <see cref="AFile"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		/// <param name="Extension">The extension.</param>
		public AFile(string Path, string Name, string Extension)
			: base(ElementType.File, Path, Name) {
			this.Extension = Extension;
		}

		/// <summary>
		/// Gets the type of the file.
		/// </summary>
		/// <returns>The element Type</returns>
		public FileType GetFileType() {
			return (this.FileType);
		}

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <returns>The extension of the element</returns>
		public string GetExtension() {
			return (this.Extension);
		}
	}
}