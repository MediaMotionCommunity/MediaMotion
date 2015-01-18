using System;
using MediaMotion.Core.Models.FileManager.Interfaces;

namespace MediaMotion.Modules.MediaPlayer.Events {
	/// <summary>
	/// Media Event Args
	/// </summary>
	public class MediaEventArgs : EventArgs {
		/// <summary>
		/// Initializes a new instance of the <see cref="MediaEventArgs"/> class.
		/// </summary>
		/// <param name="Element">The element.</param>
		public MediaEventArgs(IElement Element) {
			this.Element = Element;
		}

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		public IElement Element { get; private set; }
	}
}
