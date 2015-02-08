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
		/// Initializes a new instance of the <see cref="AElement"/> class.
		/// </summary>
		/// <param name="ElementType">Type of the element.</param>
		/// <param name="Path">The path.</param>
		/// <param name="Name">The name.</param>
		public AElement(ElementType ElementType) {
			this.ElementType = ElementType;
		}

		/// <summary>
		/// Gets the type of the element.
		/// </summary>
		/// <returns>The element Type</returns>
		public ElementType GetElementType() {
			return (this.ElementType);
		}

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <returns>The parent path</returns>
		public abstract string GetParent();

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The path of the element</returns>
		public abstract string GetPath();

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The name of the element</returns>
		public abstract string GetName();
	}
}
