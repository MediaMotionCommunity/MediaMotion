﻿using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories.Interfaces {
	/// <summary>
	/// Factory Interface
	/// </summary>
	public interface IElementFactory {
		/// <summary>
		/// Creates the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The folder or <c>null</c> if path does not point to any element or the pointed is not a folder</returns>
		IFolder CreateFolder(string path);

		/// <summary>
		/// Creates the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The file or <c>null</c> if path does not point to any element or the pointed is not a file</returns>
		IFile CreateFile(string path);

		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <returns>
		/// The element or <c>null</c> if path does not point to any element
		/// </returns>
		IElement Create(string Path);
	}
}
