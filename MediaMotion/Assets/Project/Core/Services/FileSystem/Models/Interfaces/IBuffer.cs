using MediaMotion.Core.Services.FileSystem.Models.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Models.Interfaces {
	/// <summary>
	/// Element Buffer Model Interface
	/// </summary>
	public interface IBuffer {
		/// <summary>
		/// Gets the elements.
		/// </summary>
		/// <value>
		///   The elements.
		/// </value>
		IElement[] Elements { get; }

		/// <summary>
		/// Gets a value indicating whether [delete elements after paste].
		/// </summary>
		/// <value>
		///   <c>true</c> if [delete elements after paste]; otherwise, <c>false</c>.
		/// </value>
		bool DeleteElementsAfterPaste { get; }

		/// <summary>
		/// Gets a value indicating whether [delete buffer after paste].
		/// </summary>
		/// <value>
		///   <c>true</c> if [delete buffer after paste]; otherwise, <c>false</c>.
		/// </value>
		bool DeleteBufferAfterPaste { get; }
	}
}
