using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Core.Models.FileManager.Abstracts {
	/// <summary>
	/// Abstract Element
	/// </summary>
	public abstract class AElement : IElement {
		/// <summary>
		/// The element type
		/// </summary>
		private ElementType ElementType;

		/// <summary>
		/// The path
		/// </summary>
		private string Path;

		/// <summary>
		/// The name
		/// </summary>
		private string Name;

		/// <summary>
		/// Initializes a new instance of the <see cref="AElement"/> class.
		/// </summary>
		/// <param name="ElementType">Type of the element.</param>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		public AElement(ElementType ElementType, string Path, string Name) {
			this.ElementType = ElementType;
			this.Path = Path;
			this.Name = Name;
		}

		/// <summary>
		/// Gets the type of the element.
		/// </summary>
		/// <returns>The element Type</returns>
		public ElementType GetElementType() {
			return (this.ElementType);
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The path of the element</returns>
		public string GetPath() {
			return (this.Path);
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The name of the element</returns>
		public string GetName() {
			return (this.Name);
		}
	}
}
