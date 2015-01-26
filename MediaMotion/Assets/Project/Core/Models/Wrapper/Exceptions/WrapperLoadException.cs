using System;

namespace MediaMotion.Core.Models.Wrapper.Exceptions {
	/// <summary>
	/// Exception Wrapper load
	/// </summary>
	public class WrapperLoadException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		public WrapperLoadException()
			: base("unknown wrapper load error") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		public WrapperLoadException(string Message)
			: base(Message) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperLoadException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		/// <param name="Parent">The parent.</param>
		public WrapperLoadException(string Message, Exception Parent)
			: base(Message, Parent) {
		}
	}
}
