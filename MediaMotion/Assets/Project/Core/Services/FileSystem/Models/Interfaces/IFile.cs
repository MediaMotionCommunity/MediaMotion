using System;
using MediaMotion.Core.Services.FileSystem.Models.Enums;

namespace MediaMotion.Core.Services.FileSystem.Models.Interfaces {
	/// <summary>
	/// File Interface
	/// </summary>
	public interface IFile : IElement {
		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <returns>The element extension</returns>
		string GetExtension();
	}
}