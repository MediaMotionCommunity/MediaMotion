using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	/// <summary>
	/// FileSystem Interface
	/// </summary>
	public interface IFileSystem {
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
		/// Get the home directory
		/// </summary>
		/// <returns>Home directory</returns>
		IFolder GetHomeDirectory();

		/// <summary>
		/// Change directory to the Home or the <see cref="IFolder" /> provide in parameter
		/// </summary>
		/// <param name="Folder">New working directory</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool ChangeDirectory(IFolder Folder = null);

		/// <summary>
		/// Changes the directory.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <returns>True if the action succeed, False otherwise</returns>
		bool ChangeDirectory(string Path = null);

		/// <summary>
		/// Get the content of the current directory or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="Folder">A specific folder to use</param>
		/// <returns>List of elements</returns>
		List<IElement> GetDirectoryContent(IFolder Folder);

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
