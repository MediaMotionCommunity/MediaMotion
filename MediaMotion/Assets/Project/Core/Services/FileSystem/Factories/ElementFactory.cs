using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.FileSystem.Factories {
	/// <summary>
	/// File Factory
	/// </summary>
	public class ElementFactory : IElementFactory {
		/// <summary>
		/// The observers priority list
		/// </summary>
		private SortedDictionary<int, List<IElementFactoryObserver>> observersPriorityList;

		/// <summary>
		/// Initializes a new instance of the <see cref="ElementFactory"/> class.
		/// </summary>
		public ElementFactory() {
			this.observersPriorityList = new SortedDictionary<int, List<IElementFactoryObserver>>();
		}

		/// <summary>
		/// Adds the observer.
		/// </summary>
		/// <param name="observer">The observer.</param>
		/// <param name="priority">The priority.</param>
		public void AddObserver(IElementFactoryObserver observer, int priority = 0) {
			if (!this.observersPriorityList.ContainsKey(priority)) {
				this.observersPriorityList.Add(priority, new List<IElementFactoryObserver>());
			}
			this.observersPriorityList[priority].Add(observer);
		}

		/// <summary>
		/// Removes the observer.
		/// </summary>
		/// <param name="observer">The observer.</param>
		public void RemoveObserver(IElementFactoryObserver observer) {
			foreach (KeyValuePair<int, List<IElementFactoryObserver>> observerList in this.observersPriorityList) {
				if (observerList.Value.Contains(observer)) {
					observerList.Value.Remove(observer);
				}
			}
		}

		/// <summary>
		/// Creates the folder.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// The folder or <c>null</c> if path does not point to any element or the pointed is not a folder
		/// </returns>
		public IFolder CreateFolder(string path) {
			return (this.Create(path) as IFolder);
		}

		/// <summary>
		/// Creates the file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The file or <c>null</c> if path does not point to any element or the pointed is not a file</returns>
		public IFile CreateFile(string path) {
			return (this.Create(path) as IFile);
		}

		/// <summary>
		/// Creates the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>The element or <c>null</c> if path does not point to any element</returns>
		public IElement Create(string path) {
			if (Directory.Exists(path) || File.Exists(path)) {
				IElement model = null;

				foreach (IElementFactoryObserver observer in this.observersPriorityList.OrderByDescending(observerList => observerList.Key).SelectMany(observerList => observerList.Value)) {
					if (observer.Supports(path)) {
						model = observer.Create(path);
						if (model != null) {
							return (model);
						}
					}
				}
				if ((File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory) {
					return (new Folder(new DirectoryInfo(path)));
				} else {
					return (new Regular(new FileInfo(path)));
				}
			}
			return (null);
		}
	}
}
