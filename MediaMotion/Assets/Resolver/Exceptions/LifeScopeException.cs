using System;

namespace MediaMotion.Resolver.Exceptions {
	/// <summary>
	/// The life scope exception.
	/// </summary>
	internal class LifeScopeException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="LifeScopeException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public LifeScopeException(string message) : base(message) {
		}
	}
}