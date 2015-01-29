using System;

namespace MediaMotion.Resolver.Exceptions {
	/// <summary>
	/// The injectable constructor not found exception.
	/// </summary>
	public class InjectableConstructorNotFoundException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="InjectableConstructorNotFoundException"/> class.
		/// </summary>
		/// <param name="message">
		/// The message.
		/// </param>
		public InjectableConstructorNotFoundException(string message) : base(message) {
		}
	}
}