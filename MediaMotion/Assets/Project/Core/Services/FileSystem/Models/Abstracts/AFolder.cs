using System.IO;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Models.Abstracts {
	/// <summary>
	/// Abstract Folder
	/// </summary>
	public abstract class AFolder : AElement, IFolder {
		/// <summary>
		/// The directory information
		/// </summary>
		private readonly DirectoryInfo directoryInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="AFolder" /> class.
		/// </summary>
		/// <param name="directoryInfo">The directory information.</param>
		/// <param name="resourceId">The resource Id.</param>
		protected AFolder(DirectoryInfo directoryInfo, string resourceId = null)
			: base(ElementType.Folder, resourceId) {
			this.directoryInfo = directoryInfo;
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <returns>The parent path</returns>
		public override string GetParent() {
			return (this.directoryInfo.Parent.FullName);
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The path of the element</returns>
		public override string GetPath() {
			return (this.directoryInfo.FullName);
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The name of the element</returns>
		public override string GetName() {
			return (this.directoryInfo.Name);
		}
	}
}
