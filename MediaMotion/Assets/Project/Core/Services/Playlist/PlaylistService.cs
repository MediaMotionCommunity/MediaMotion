using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Playlist {
	/// <summary>
	/// Playlist Service
	/// </summary>
	public class PlaylistService : IPlaylistService {
		/// <summary>
		/// The file system service
		/// </summary>
		private IFileSystemService fileSystemService;

		/// <summary>
		/// The file list
		/// </summary>
		private IFile[] filesList;

		/// <summary>
		/// The index
		/// </summary>
		private int index;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistService"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		public PlaylistService(IFileSystemService fileSystem) {
			this.fileSystemService = fileSystem;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService" /> is random.
		/// </summary>
		/// <value>
		///   <c>true</c> if random; otherwise, <c>false</c>.
		/// </value>
		public bool Random { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="IPlaylistService" /> is loop.
		/// </summary>
		/// <value>
		///   <c>true</c> if loop; otherwise, <c>false</c>.
		/// </value>
		public bool Loop { get; set; }

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		public int Length { get; private set; }

		/// <summary>
		/// Configures the directory.
		/// </summary>
		/// <param name="element">The file or the directory.</param>
		/// <param name="filterExtension">The filter extension.</param>
		public void Configure(IElement element, string[] filterExtension) {
			if (element.GetElementType() == ElementType.File) {
				this.filesList = this.fileSystemService.GetContent(filterExtension, element.GetParent()).ToArray();
				this.index = Array.IndexOf(this.filesList, element);
			} else {
				this.filesList = this.fileSystemService.GetContent(filterExtension, element.GetPath()).ToArray();
				this.index = 0;
			}
			if (this.index < 0) {
				this.index = 0;
			}
			this.Length = this.filesList.Length;
		}

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Current() {
			return (this.filesList[this.index]);
		}

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Previous() {
			this.index = ((this.index - 1 < 0) ? (this.Length) : (this.index)) - 1;
			return (this.Current());
		}

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		public IFile Next() {
			this.index = ((this.index + 1 >= this.Length) ? (0) : (this.index + 1));
			return (this.Current());
		}
	}
}
