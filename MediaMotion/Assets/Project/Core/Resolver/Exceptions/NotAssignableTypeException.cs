using System;

namespace MediaMotion.Core.Resolver.Exceptions {
	/// <summary>
	/// The not assignable type exception.
	/// </summary>
	internal class NotAssignableTypeException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="NotAssignableTypeException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public NotAssignableTypeException(string message) : base(message) {
		}
	}
}