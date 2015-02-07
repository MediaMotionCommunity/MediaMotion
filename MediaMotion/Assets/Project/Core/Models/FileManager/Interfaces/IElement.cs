using System;
using MediaMotion.Core.Models.FileManager.Enums;

namespace MediaMotion.Core.Models.FileManager.Interfaces {
	/// <summary>
	/// Interface Element
	/// </summary>
	public interface IElement {
		/// <summary>
		/// Gets the type of the element.
		/// </summary>
		/// <returns>The element Type</returns>
		ElementType GetElementType();

		/// <summary>
		/// Gets the parent.
		/// </summary>
		/// <returns>The parent path</returns>
		string GetParent();

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns>The element path</returns>
		string GetPath();

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <returns>The element name</returns>
		string GetName();
	}
}