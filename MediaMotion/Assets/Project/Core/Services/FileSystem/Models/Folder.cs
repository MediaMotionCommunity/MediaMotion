using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;

namespace MediaMotion.Core.Services.FileSystem.Models {
	/// <summary>
	/// Model Folder
	/// </summary>
	public class Folder : AFolder {
		/// <summary>
		/// Initializes a new instance of the <see cref="Folder" /> class.
		/// </summary>
		/// <param name="directoryInfo">The directory information.</param>
		public Folder(DirectoryInfo directoryInfo)
			: base(directoryInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Folder");
		}
	}
}
