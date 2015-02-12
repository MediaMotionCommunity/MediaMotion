using System.IO;
using MediaMotion.Core.Models.FileManager.Abstracts;
using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Models.FileManager {
	/// <summary>
	/// Model Folder
	/// </summary>
	public class Folder : AFolder, IFolder {
		/// <summary>
		/// Initializes a new instance of the <see cref="Folder" /> class.
		/// </summary>
		/// <param name="directoryInfo">The directory information.</param>
		public Folder(DirectoryInfo directoryInfo)
			: base(directoryInfo, Resources.Load<Texture2D>("Folder-icon")) {
		}
	}
}
