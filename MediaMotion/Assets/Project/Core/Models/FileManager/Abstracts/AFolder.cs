using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	/// <summary>
	/// Abstract Folder
	/// </summary>
	public abstract class AFolder : AElement, IFolder {
		/// <summary>
		/// Initializes a new instance of the <see cref="AFolder"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		public AFolder(string Path, string Name)
			: base(ElementType.Folder, Path, Name) {
		}
	}
}
