using System.Collections.Generic;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Interfaces {
	public interface IFileSystem {
		/// <summary>
		/// Gets or sets the initial folder.
		/// </summary>
		/// <value>
		/// The initial folder.
		/// </value>
		IFolder InitialFolder { get; }

		/// <summary>
		/// Gets or sets the current folder.
		/// </summary>
		/// <value>
		/// The current folder.
		/// </value>
		IFolder CurrentFolder { get; }

		/// <summary>
		/// Get the home directory
		/// </summary>
		/// <returns>
		/// Home directory
		/// </returns>
		IFolder GetHomeDirectory();

		/// <summary>
		/// Change directory to the Home or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="Folder">
		/// New working directory
		/// </param>
		void ChangeDirectory(IFolder Folder);

		/// <summary>
		/// Get the content of the current directory or the <see cref="IFolder"/> provide in parameter
		/// </summary>
		/// <param name="Folder">
		/// A specific folder to use
		/// </param>
		/// <returns>
		/// List of elements
		/// </returns>
		List<IElement> GetDirectoryContent(IFolder Folder = null);

		/// <summary>
		/// Copy an <see cref="IElement"/> to the specific <see cref="IFolder"/>
		/// </summary>
		/// <param name="Element"></param>
		/// <param name="Destination"></param>
		void Copy(IElement Element, IFolder Destination);

		/// <summary>
		/// Move an <see cref="IElement"/> in a different <see cref="IFolder"/>
		/// </summary>
		/// <param name="Element">
		/// The element to be move
		/// </param>
		/// <param name="Destination">
		/// The folder destination
		/// </param>
		void Move(IElement Element, IFolder Destination);

		/// <summary>
		/// Remove an element
		/// </summary>
		/// <param name="Element">
		/// The element to delete
		/// </param>
		void Remove(IElement Element);
	}
}
