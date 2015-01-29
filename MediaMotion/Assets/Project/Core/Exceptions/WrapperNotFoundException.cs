using System;

namespace MediaMotion.Core.Exceptions {
	/// <summary>
	/// Exception wrapper not found
	/// </summary>
	public class WrapperNotFoundException : NotFoundException {
		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperNotFoundException"/> class.
		/// </summary>
		/// <param name="Name">The name.</param>
		public WrapperNotFoundException(string Name)
			: base(Name + " wrapper not found") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WrapperNotFoundException"/> class.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Parent">The parent.</param>
		public WrapperNotFoundException(string Name, Exception Parent)
			: base(Name + " wrapper not found", Parent) {
		}
	}
}
