using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Services.FileSystem.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Playlist.Interfaces;

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
		/// The is randomized
		/// </summary>
		private bool isRandomized;

		/// <summary>
		/// The PRNG
		/// </summary>
		private Random prng;

		/// <summary>
		/// Initializes a new instance of the <see cref="PlaylistService"/> class.
		/// </summary>
		/// <param name="fileSystem">The file system.</param>
		public PlaylistService(IFileSystemService fileSystem) {
			this.fileSystemService = fileSystem;
			this.prng = new Random();
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
				this.Index = this.SortArray().GetValueOrDefault();
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
		/// Gets the elements.
		/// </summary>
		/// <value>
		/// The elements.
		/// </value>
		public IComparable[] Elements { get; private set; }

		/// <summary>
		/// Gets the length.
		/// </summary>
		/// <value>
		/// The length.
		/// </value>
		public int Length { get; private set; }

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <value>
		/// The index.
		/// </value>
		public int Index { get; private set; }

		/// <summary>
		/// Configures the directory.
		/// </summary>
		/// <param name="element">The file or the directory.</param>
		/// <param name="filterExtension">The filter extension.</param>
		/// <returns>
		///   <c>true</c> if the playlist is correctly configured otherwise, <c>false</c>
		/// </returns>
		public bool Configure(IElement element, string[] filterExtension) {
			if (element != null) {
				return (this.Configure(this.fileSystemService.GetFolderElements((element.GetElementType() == ElementType.File) ? (element.GetParent()) : (element.GetPath()), filterExtension), element));
			}
			return (false);
		}

		/// <summary>
		/// Configures the specified elements.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <param name="element">The element.</param>
		/// <returns>
		///   <c>true</c> if the playlist is correctly configured otherwise, <c>false</c>
		/// </returns>
		public bool Configure(IComparable[] elements, IComparable element = null) {
			if (elements != null) {
				this.Elements = elements;
				this.Length = this.Elements.Length;
				this.Index = this.GetIndex(element).GetValueOrDefault();
				this.Index = this.SortArray().GetValueOrDefault();
				this.IsConfigured = true;
				return (this.IsConfigured);
			}
			return (false);
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. But leave the instance usable (contrary to Dispose)
		/// </summary>
		public void Reset() {
			foreach (IComparable element in this.Elements) {
				if (element is IDisposable) {
					((IDisposable)element).Dispose();
				}
			}
			this.Index = 0;
			this.Length = 0;
			this.Elements = null;
			this.IsConfigured = false;
		}

		/// <summary>
		/// Current file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		public object Current() {
			if (this.IsConfigured) {
				return (this.Elements[this.Index]);
			}
			return (null);
		}

		/// <summary>
		/// Previous file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		public object Previous() {
			if (this.IsConfigured) {
				int? index = this.GetIndex(-1);

				if (index.HasValue) {
					this.Index = index.Value;
					return (this.Current());
				}
			}
			return (null);
		}

		/// <summary>
		/// Next file in the list.
		/// </summary>
		/// <returns>
		///   The element or <c>null</c>
		/// </returns>
		public object Next() {
			if (this.IsConfigured) {
				int? index = this.GetIndex(1);

				if (index.HasValue) {
					this.Index = index.Value;
					return (this.Current());
				}
			}
			return (null);
		}

		/// <summary>
		/// Peeks the specified index.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <returns>
		/// The file
		/// </returns>
		public object Peek(int offset = 0) {
			if (this.IsConfigured) {
				int? index = this.GetIndex(offset);

				if (index.HasValue) {
					return (this.Elements[index.Value]);
				}
			}
			return (null);
		}

		/// <summary>
		/// Gets the index.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		/// The index
		/// </returns>
		private int? GetIndex(IComparable element) {
			if (this.IsConfigured) {
				int index = Array.IndexOf(this.Elements, this.Elements.First(file => file.CompareTo(element) == 0));

				if (index >= 0) {
					return (index);
				}
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
				int index = this.Index + offset;

				if (this.Loop || (index >= 0 && index < this.Length)) {
					return (((index % this.Length) + this.Length) % this.Length);
				}
			}
			return (null);
		}

		/// <summary>
		/// Sorts the callback.
		/// </summary>
		/// <param name="element1">The element1.</param>
		/// <param name="element2">The element2.</param>
		/// <returns>
		/// <c>-1</c> if the element1 is inferior to the element2, <c>1</c> if the element1 is superior to the element2, <c>0</c> otherwise
		/// </returns>
		private int SortCallback(IComparable element1, IComparable element2) {
			switch (this.Random) {
				case true:
					return (this.prng.Next(3) - 1);
				case false:
					return (element1.CompareTo(element2));
			}
			return (0);
		}

		/// <summary>
		/// Sorts the array.
		/// </summary>
		/// <returns>
		/// The new index of the current element
		/// </returns>
		private int? SortArray() {
			if (this.IsConfigured) {
				object element = this.Current();

				Array.Sort(this.Elements, this.SortCallback);
				return (this.GetIndex((IComparable)element));
			}
			return (null);
		}
	}
}
