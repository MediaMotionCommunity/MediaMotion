using System;

namespace MediaMotion.Motion.Exceptions {
	/// <summary>
	/// The wrapper load exception.
	/// </summary>
	public class WrapperLoadException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		public WrapperLoadException()
			: base("Core dependency not found") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public WrapperLoadException(string message)
			: base(message) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="parent">
		/// The parent.
		/// </param>
		public WrapperLoadException(string message, Exception parent)
			: base(message, parent) {
		}
	}
}
