using System;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Services.FileSystem.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Abstracts {
	/// <summary>
	/// Abstract Factory
	/// </summary>
	public abstract class AFactory {
		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <returns>The name</returns>
		public string GetName(string Path) {
			int index;

			return (((index = Math.Max(Path.LastIndexOf('/'), Path.LastIndexOf('\\'))) >= 0) ? (Path.Substring(index + 1)) : (Path));
		}

		/// <summary>
		/// Gets the extension.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <returns>The extension</returns>
		public string GetExtension(string Name) {
			int index;

			return (((index = Name.LastIndexOf('.')) >= 0) ? (Name.Substring(index + 1).ToLower()) : (string.Empty));
		}
	}
}
