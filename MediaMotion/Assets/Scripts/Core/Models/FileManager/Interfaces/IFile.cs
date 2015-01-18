using System;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// File Interface
	/// </summary>
	public interface IFile : IElement {
		/// <summary>
		/// Gets the type of the file.
		/// </summary>
		/// <returns>The element Type</returns>
		FileType GetFileType();

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <returns>The element extension</returns>
		string GetExtension();
	}
}