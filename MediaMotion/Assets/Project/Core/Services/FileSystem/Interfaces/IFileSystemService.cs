using System.Collections.Generic;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	/// <summary>
	/// FileSystem Interface
	/// </summary>
	public interface IFileSystemService {
		/// <summary>
		/// Gets the initial folder.
		/// </summary>
		/// <value>
		///   The initial folder.
		/// </value>
		IFolder InitialFolder { get; }

		/// <summary>
		/// Gets the current folder.
		/// </summary>
		/// <value>
		///   The current folder.
		/// </value>
		IFolder CurrentFolder { get; }

		/// <summary>
		/// Gets the bufferized elements.
		/// </summary>
		/// <value>
		///   The bufferized elements.
		/// </value>
		IBuffer BufferizedElements { get; }

		/// <summary>
		/// Gets or sets a value indicating whether [display hidden].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display hidden]; otherwise, <c>false</c>.
		/// </value>
		bool DisplayHiddenElements { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [display system files].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display system files]; otherwise, <c>false</c>.
		/// </value>
		bool DisplaySystemElements { get; set; }

		/// <summary>
		/// Get the home path
		/// </summary>
		/// <returns>the home path</returns>
		string GetHome();

		/// <summary>
		/// Gets the home folder.
		/// </summary>
		/// <returns>the home folder</returns>
		IFolder GetHomeFolder();

		/// <summary>
		/// Changes the directory.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// True if the action succeed, False otherwise
		/// </returns>
		bool ChangeDirectory(string path = null);

		/// <summary>
		/// Gets folder's elements
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <param name="onlyFiles">if set to <c>true</c> [only files].</param>
		/// <returns>
		///   An array with elements of the folder, <c>null</c> if an error occurred
		/// </returns>
		IElement[] GetFolderElements(string path = null, string[] filterExtension = null, bool onlyFiles = false);

		/// <summary>
		/// Gets the folder files.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns>
		///   An array with files of the folder, <c>null</c> if an error occurred
		/// </returns>
		IFile[] GetFolderFiles(string path = null, string[] filterExtension = null);

		/// <summary>
		/// Bufferizes the elements for copy
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		bool Copy(IElement[] elements);

		/// <summary>
		/// Bufferizes the element for copy
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		bool Copy(IElement element);

		/// <summary>
		/// Bufferizes the elements for movement
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		bool Cut(IElement[] elements);

		/// <summary>
		/// Bufferizes the element for movement
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the buffer is correctly initialized, <c>false</c> otherwise
		/// </returns>
		bool Cut(IElement element);

		/// <summary>
		/// Pastes the specified destination.
		/// </summary>
		/// <param name="destination">The destination.</param>
		/// <returns>
		///   <c>true</c> if the action succeed, <c>false</c> if not or if the buffer is empty
		/// </returns>
		bool Paste(IFolder destination);

		/// <summary>
		/// Remove an element
		/// </summary>
		/// <param name="Element">The element to delete</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Remove(IElement Element);

		/// <summary>
		/// Restores an element which was remove earlier
		/// </summary>
		/// <param name="Element">The element.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Restore(IElement Element);

		/// <summary>
		/// Determines whether [is buffer empty].
		/// </summary>
		/// <returns>
		///   <c>true</c> if the buffer is empty, <c>false</c> otherwise
		/// </returns>
		bool IsBufferEmpty();
	}
}
