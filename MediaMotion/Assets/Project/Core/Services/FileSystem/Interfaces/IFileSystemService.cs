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
		/// The initial folder.
		/// </value>
		IFolder InitialFolder { get; }

		/// <summary>
		/// Gets the current folder.
		/// </summary>
		/// <value>
		/// The current folder.
		/// </value>
		IFolder CurrentFolder { get; }

		/// <summary>
		/// Gets or sets a value indicating whether [display hidden].
		/// </summary>
		/// <value>
		///   <c>true</c> if [display hidden]; otherwise, <c>false</c>.
		/// </value>
		bool DisplayHidden { get; set; }

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
		/// Get the content of the current directory or the <see cref="IFolder" /> provide in parameter
		/// </summary>
		/// <param name="folder">The folder.</param>
		/// <returns>
		/// List of elements
		/// </returns>
		List<IElement> GetContent(string folder = null);

		/// <summary>
		/// Gets the content.
		/// </summary>
		/// <param name="filterExtension">The filter extension.</param>
		/// <param name="folder">The folder.</param>
		/// <returns>
		/// List of files
		/// </returns>
		List<IFile> GetContent(string[] filterExtension, string folder);

		/// <summary>
		/// Copy an <see cref="IElement" /> to the specific <see cref="IFolder" />
		/// </summary>
		/// <param name="Element">The element to be copied</param>
		/// <param name="Destination">The folder destination</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Copy(IElement Element, IFolder Destination);

		/// <summary>
		/// Move an <see cref="IElement" /> in a different <see cref="IFolder" />
		/// </summary>
		/// <param name="Element">The element to be move</param>
		/// <param name="Destination">The folder destination</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool Move(IElement Element, IFolder Destination);

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
	}
}
