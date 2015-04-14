using System;
using System.Linq;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Models {
	/// <summary>
	/// Element Buffer Model
	/// </summary>
	public class Buffer : IBuffer {
		/// <summary>
		/// Initializes a new instance of the <see cref="Buffer"/> class.
		/// </summary>
		/// <param name="elements">The elements.</param>
		/// <param name="deleteElementsAfterPaste">if set to <c>true</c> [delete elements after paste].</param>
		/// <param name="deleteBufferAfterPaste">if set to <c>true</c> [delete buffer after paste].</param>
		public Buffer(IElement[] elements, bool deleteElementsAfterPaste, bool deleteBufferAfterPaste) {
			if (elements == null) {
				throw new ArgumentNullException("elements must not be null");
			}
			if (!elements.All(element => element != null)) {
				throw new ArgumentNullException("elements must not contain any null element");
			}
			this.Elements = elements;
			this.DeleteElementsAfterPaste = deleteElementsAfterPaste;
			this.DeleteBufferAfterPaste = deleteBufferAfterPaste;
		}

		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		///   The elements.
		/// </value>
		public IElement[] Elements { get; private set; }

		/// <summary>
		/// Gets a value indicating whether [delete elements after paste].
		/// </summary>
		/// <value>
		///   <c>true</c> if [delete elements after paste]; otherwise, <c>false</c>.
		/// </value>
		public bool DeleteElementsAfterPaste { get; private set; }

		/// <summary>
		/// Gets a value indicating whether [delete buffer after paste].
		/// </summary>
		/// <value>
		///   <c>true</c> if [delete buffer after paste]; otherwise, <c>false</c>.
		/// </value>
		public bool DeleteBufferAfterPaste { get; private set; }
	}
}
