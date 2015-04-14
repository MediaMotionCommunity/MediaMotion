using System;
using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Models.Module.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
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
		private IElement[] filesList;

		/// <summary>
		/// The index
		/// </summary>
		private int index;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistService"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		public PlaylistService(IFileSystemService fileSystem) {
			this.IsConfigured = false;
			this.fileSystemService = fileSystem;
		}

		/// <summary>
		/// Gets a value indicating whether this instance is configured.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is configured; otherwise, <c>false</c>.
		/// </value>
		public bool IsConfigured { get; private set; }

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
		/// <returns><c>true</c> if the playlist is correctly configured otherwise, <c>false</c></returns>
		public bool Configure(IElement element, string[] filterExtension) {
			this.IsConfigured = false;
			try {
				if (element == null) {
					throw new ArgumentNullException("element must not be null");
				}
				switch (element.GetElementType()) {
					case ElementType.File:
						this.filesList = this.fileSystemService.GetFolderElements(element.GetParent(), filterExtension);
						this.index = Array.IndexOf(this.filesList, this.filesList.First(file => file.GetPath().CompareTo(element.GetPath()) == 0));
						if (this.index < 0) {
							this.index = 0;
						}
						break;
					case ElementType.Folder:
						this.filesList = this.fileSystemService.GetFolderElements(element.GetPath(), filterExtension);
						this.index = 0;
						break;
					default:
						throw new NotSupportedException("unsupported element type");
				}
				this.Length = this.filesList.Length;
				this.IsConfigured = true;
			} catch (ArgumentNullException) {
				// TODO log message
			} catch (NotSupportedException) {
				// TODO log message
			} catch (Exception) {
				// TODO log message
			}
			return (this.IsConfigured);
		}

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		/// <exception cref="System.InvalidOperationException">The playlist must be initialized</exception>
		public IFile Current() {
			if (!this.IsConfigured) {
				throw new InvalidOperationException("The playlist must be initialized");
			}
			if (this.filesList[this.index] is IFile) {
				return (this.filesList[this.index] as IFile);
			}
			return (null);
		}

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		/// <exception cref="System.InvalidOperationException">The playlist must be initialized</exception>
		public IFile Previous() {
			if (!this.IsConfigured) {
				throw new InvalidOperationException("The playlist must be initialized");
			}
			this.index = ((this.index - 1 < 0) ? (this.Length) : (this.index)) - 1;
			return (this.Current());
		}

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>
		/// The file
		/// </returns>
		/// <exception cref="System.InvalidOperationException">The playlist must be initialized</exception>
		public IFile Next() {
			if (!this.IsConfigured) {
				throw new InvalidOperationException("The playlist must be initialized");
			}
			this.index = ((this.index + 1 >= this.Length) ? (0) : (this.index + 1));
			return (this.Current());
		}
	}
}
