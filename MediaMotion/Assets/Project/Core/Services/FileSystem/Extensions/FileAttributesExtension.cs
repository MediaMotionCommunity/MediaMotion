using System.IO;

namespace MediaMotion.Core.Services.FileSystem.Extensions {
	/// <summary>
	/// FileAttributes Extensions
	/// </summary>
	public static class FileAttributesExtension {
		/// <summary>
		/// Determines whether the specified attribute has attribute.
		/// </summary>
		/// <param name="attributes">The attributes.</param>
		/// <param name="attribute">The attribute.</param>
		/// <returns>
		///   <c>true</c> if the file has the attribute<c>attribute</c>, <c>false</c> otherwise
		/// </returns>
		public static bool HasAttribute(this FileAttributes attributes, FileAttributes attribute) {
			return ((attributes & attribute) == attribute);
		}
	}
}
