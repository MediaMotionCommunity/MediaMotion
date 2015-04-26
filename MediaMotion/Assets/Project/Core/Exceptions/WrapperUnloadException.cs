using System;

namespace MediaMotion.Core.Exceptions {
	/// <summary>
	/// Exception Wrapper unload
	/// </summary>
	public class WrapperUnloadException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		public WrapperUnloadException()
			: base("unknown wrapper unload error") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		public WrapperUnloadException(string Message)
			: base(Message) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperUnloadException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		/// <param name="Parent">The parent.</param>
		public WrapperUnloadException(string Message, Exception Parent)
			: base(Message, Parent) {
		}
	}
}
