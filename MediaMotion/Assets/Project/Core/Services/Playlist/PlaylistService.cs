using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MediaMotion.Core.Models.Abstracts;
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
		private IFile[] filesList;

		/// <summary>
		/// The index
		/// </summary>
		private int index;

		/// <summary>
		/// The is randomized
		/// </summary>
		private bool isRandomized;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistService"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		public PlaylistService(IFileSystemService fileSystem) {
			this.fileSystemService = fileSystem;
			this.IsConfigured = false;
			this.Loop = true;
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
		public bool Random {
			get {
				return (this.isRandomized);
			}
			set {
				this.isRandomized = value;
				this.index = this.SortArray().GetValueOrDefault();
			}
		}

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
		/// <returns>
		///   <c>true</c> if the playlist is correctly configured otherwise, <c>false</c>
		/// </returns>
		/// <exception cref="System.ArgumentNullException">arguments must not be null</exception>
		public bool Configure(IElement element, string[] filterExtension) {
			this.IsConfigured = false;
			try {
				if (element == null || filterExtension == null) {
					throw new ArgumentNullException("arguments must not be null");
				}
				this.filesList = Array.ConvertAll(this.fileSystemService.GetFolderElements((element.GetElementType() == ElementType.File) ? (element.GetParent()) : (element.GetPath()), filterExtension), item => (IFile)item);
				this.Length = this.filesList.Length;
				this.index = 0;
				if (element.GetElementType() == ElementType.File) {
					this.index = this.GetIndex(element as IFile).GetValueOrDefault();
				}
				this.index = this.SortArray().GetValueOrDefault();
				this.IsConfigured = true;
			} catch (ArgumentNullException) {
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
			return (this.filesList[this.index]);
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
			int? index = this.GetIndex(-1);

			if (!index.HasValue) {
				return (null);
			}
			this.index = index.Value;
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
			int? index = this.GetIndex(1);

			if (!index.HasValue) {
				return (null);
			}
			this.index = index.Value;
			return (this.Current());
		}

		/// <summary>
		/// Peeks the specified index.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The file
		/// </returns>
		/// <exception cref="System.InvalidOperationException">The playlist must be initialized</exception>
		public IFile Peek(int offset = 0) {
			if (!this.IsConfigured) {
				throw new InvalidOperationException("The playlist must be initialized");
			}
			int? index = this.GetIndex(offset);

			if (!index.HasValue) {
				return (null);
			}
			return (this.filesList[index.Value]);
		}

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		/// The index
		/// </returns>
		private int? GetIndex(IFile element) {
			if (this.IsConfigured) {
				int index = Array.IndexOf(this.filesList, this.filesList.First(file => file.GetPath().CompareTo(element.GetPath()) == 0));

				if (this.index < 0) {
					return (null);
				}
				return (index);
			}
			return (null);
		}

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The index
		/// </returns>
		private int? GetIndex(int offset = 0) {
			if (this.IsConfigured) {
				int index = this.index + offset;

				if (!this.Loop && (index < 0 || index >= this.Length)) {
					return (null);
				}
				return (((index % this.Length) + this.Length) % this.Length);
			}
			return (null);
		}

		/// <summary>
		/// Sorts the array.
		/// </summary>
		/// <returns>
		/// The new index of the current element
		/// </returns>
		private int? SortArray() {
			if (this.IsConfigured) {
				System.Random prng = new System.Random();
				IFile element = this.Current();

				Array.Sort(this.filesList, delegate(IFile file1, IFile file2) {
					if (this.Random) {
						return (prng.Next(3) - 1);
					} else {
						return (file1.GetName().CompareTo(file2.GetName()));
					}
				});
				return (this.GetIndex(element));
			}
			return (null);
		}
	}
}
