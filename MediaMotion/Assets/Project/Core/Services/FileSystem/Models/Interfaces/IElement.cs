using System;
using MediaMotion.Core.Services.FileSystem.Models.Enums;

namespace MediaMotion.Core.Services.FileSystem.Models.Interfaces {
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
		/// Gets the human type string.
		/// </summary>
		/// <returns>the type that a human can read and understand</returns>
		string GetHumanTypeString();

		/// <summary>
		/// Gets the resource id.
		/// </summary>
		/// <returns>The resource id</returns>
		string GetResourceId();

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