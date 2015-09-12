using System;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Utils;
using UnityEngine;

namespace MediaMotion.Modules.PDFViewer.Services.MuPDF.Models.Interfaces {
	/// <summary>
	/// Page interface
	/// </summary>
	public interface IPage : IDisposable, IComparable {
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
		/// Gets the resource.
		/// </summary>
		/// <value>
		/// The resource.
		/// </value>
		IntPtr Resource { get; }

		/// <summary>
		/// Gets the texture.
		/// </summary>
		/// <value>
		/// The texture.
		/// </value>
		Color32[] Texture { get; }

		/// <summary>
		/// Gets a value indicating whether this instance is render.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is render; otherwise, <c>false</c>.
		/// </value>
		bool IsRender { get; }

		/// <summary>
		/// Renders this page in the texture.
		/// </summary>
		void Render();
	}
}
