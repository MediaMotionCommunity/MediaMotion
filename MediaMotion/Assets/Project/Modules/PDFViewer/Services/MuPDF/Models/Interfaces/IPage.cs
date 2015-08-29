using System;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Utils;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces {
	/// <summary>
	/// Page interface
	/// </summary>
	public interface IPage : IDisposable, IResetable, IComparable {
		/// <summary>
		/// Gets the session.
		/// </summary>
		/// <value>
		/// The session.
		/// </value>
		IntPtr Session { get; }

		/// <summary>
		/// Gets the document.
		/// </summary>
		/// <value>
		/// The document.
		/// </value>
		IDocument Document { get; }

		/// <summary>
		/// Gets the page number.
		/// </summary>
		/// <value>
		/// The page number.
		/// </value>
		int PageNumber { get; }

		/// <summary>
		/// Gets the width.
		/// </summary>
		/// <value>
		/// The width.
		/// </value>
		int Width { get; }

		/// <summary>
		/// Gets the height.
		/// </summary>
		/// <value>
		/// The height.
		/// </value>
		int Height { get; }

		/// <summary>
		/// Gets the page.
		/// </summary>
		/// <value>
		/// The page.
		/// </value>
		IntPtr Resource { get; }

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>
		/// The texture.
		/// </value>
		AutoPinner Texture { get; }

		/// <summary>
		/// Sets the texture.
		/// </summary>
		/// <param name="texture">The texture.</param>
		void SetTexture(object texture);
	}
}
