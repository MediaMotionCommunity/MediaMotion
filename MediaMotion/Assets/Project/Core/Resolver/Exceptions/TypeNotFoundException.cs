using System;

namespace MediaMotion.Core.Resolver.Exceptions {
	/// <summary>
	/// The type not found exception.
	/// </summary>
	public class TypeNotFoundException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeNotFoundException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public TypeNotFoundException(string message) : base(message) {
		}
	}
}