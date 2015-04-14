using System.IO;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Factories.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Factories.Abstracts {
	/// <summary>
	/// File system element factory observer abstract
	/// </summary>
	public abstract class AElementFactoryObserver : IElementFactoryObserver {
		/// <summary>
		/// The supported extensions
		/// </summary>
		protected readonly string[] SupportedExtensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="AElementFactoryObserver" /> class.
		/// </summary>
		/// <param name="supportedExtensions">The supported extensions.</param>
		protected AElementFactoryObserver(string[] supportedExtensions) {
			this.SupportedExtensions = supportedExtensions;
		}

		/// <summary>
		/// Does the observer supports this type of element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		///   <c>true</c> if the observer can support this element, <c>false</c> otherwise
		/// </returns>
		public virtual bool Supports(string path) {
			if (File.Exists(path)) {
				FileInfo fileInfo = new FileInfo(path);

				return (this.SupportedExtensions.Contains(fileInfo.Extension.ToLower()));
			}
			return (false);
		}

		/// <summary>
		/// Creates the element.
		/// </summary>
		/// <param name="path">The path of the element.</param>
		/// <returns>
		///   The element
		/// </returns>
		public abstract IElement Create(string path);
	}
}
