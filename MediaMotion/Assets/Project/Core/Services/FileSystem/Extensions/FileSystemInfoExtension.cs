using System.IO;

namespace MediaMotion.Core.Services.FileSystem.Extensions {
	/// <summary>
	/// FileSystemInfo Extensions
	/// </summary>
	public static class FileSystemInfoExtension {
		/// <summary>
		/// Determines whether the specified attribute has attribute.
		/// </summary>
		/// <param name="info">The information.</param>
		/// <param name="attribute">The attribute.</param>
		/// <returns>
		///   <c>true</c> if the file has the attribute<c>attribute</c>, <c>false</c> otherwise
		/// </returns>
		public static bool HasAttribute(this FileSystemInfo info, FileAttributes attribute) {
			return (info.Attributes.HasAttribute(attribute));
		}
	}
}
