using System;

namespace MediaMotion.Motion.Exceptions {
	/// <summary>
	/// The wrapper unload exception.
	/// </summary>
	public class WrapperUnloadException : Exception {
				/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		public WrapperUnloadException()
			: base("Core dependency not found") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public WrapperUnloadException(string message)
			: base(message) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="parent">
		/// The parent.
		/// </param>
		public WrapperUnloadException(string message, Exception parent)
			: base(message, parent) {
		}
	}
}
