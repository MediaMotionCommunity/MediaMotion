﻿using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Folder
	/// </summary>
	public class Folder : AFolder, IFolder {
		/// <summary>
		/// Initializes a new instance of the <see cref="Folder"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		public Folder(string Path, string Name)
			: base(Path, Name) {
		}
	}
}