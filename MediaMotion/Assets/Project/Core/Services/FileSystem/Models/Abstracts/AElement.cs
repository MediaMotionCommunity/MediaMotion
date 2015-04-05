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
		private ElementType elementType;

		/// <summary>
		/// The texture2 d
		/// </summary>
		private string resourceId;

		/// <summary>
		/// Initializes a new instance of the <see cref="AElement" /> class.
		/// </summary>
		/// <param name="ElementType">Type of the element.</param>
		/// <param name="resourceId">The resource Id.</param>
		public AElement(ElementType ElementType, string resourceId = null) {
			this.elementType = ElementType;
			this.resourceId = resourceId;
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
		/// Gets the resource id.
		/// </summary>
		/// <returns>
		/// The resource id
		/// </returns>
		public string GetResourceId() {
			return (this.resourceId);
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
