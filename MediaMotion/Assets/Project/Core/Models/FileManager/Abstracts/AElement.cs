using MediaMotion.Core.Models.FileManager.Enums;
using MediaMotion.Core.Models.FileManager.Interfaces;
using UnityEngine;

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
		/// The texture2 d
		/// </summary>
		private Texture2D texture2D;

		/// <summary>
		/// Initializes a new instance of the <see cref="AElement" /> class.
		/// </summary>
		/// <param name="ElementType">Type of the element.</param>
		/// <param name="texture2D">The texture 2D.</param>
		public AElement(ElementType ElementType, Texture2D texture2D = null) {
			this.ElementType = ElementType;
			this.texture2D = texture2D;
		}

		/// <summary>
		/// Gets the type of the element.
		/// </summary>
		/// <returns>The element Type</returns>
		public ElementType GetElementType() {
			return (this.ElementType);
		}

		/// <summary>
		/// Gets the 2D texture.
		/// </summary>
		/// <returns>The texture 2D</returns>
		public Texture2D GetTexture2D() {
			return (this.texture2D);
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
