using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Models.Abstracts {
	/// <summary>
	/// Abstract Element
	/// </summary>
	public abstract class AElement : IElement {
		/// <summary>
		/// The element type
		/// </summary>
		private readonly ElementType elementType;

		/// <summary>
		/// Initializes a new instance of the <see cref="AElement" /> class.
		/// </summary>
		/// <param name="ElementType">Type of the element.</param>
		protected AElement(ElementType ElementType) {
			this.elementType = ElementType;
		}

		/// <summary>
		/// Gets the type of the element.
		/// </summary>
		/// <returns>The element Type</returns>
		public ElementType GetElementType() {
			return (this.elementType);
		}

		/// <summary>
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		public abstract string GetHumanTypeString();

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
