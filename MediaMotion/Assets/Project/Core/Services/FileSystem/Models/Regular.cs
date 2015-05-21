using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Abstracts;

namespace MediaMotion.Core.Services.FileSystem.Models {
	/// <summary>
	/// Model Regular File
	/// </summary>
	public class Regular : AFile {
		/// <summary>
		/// Initializes a new instance of the <see cref="Regular" /> class.
		/// </summary>
		/// <param name="fileInfo">The file information.</param>
		public Regular(FileInfo fileInfo)
			: base(fileInfo) {
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public override string GetHumanTypeString() {
			return ("Regular file");
		}
	}
}