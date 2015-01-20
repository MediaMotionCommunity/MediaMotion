using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaMotion.Core.Exceptions {
	/// <summary>
	/// Exception not found
	/// </summary>
	public class NotFoundException : Exception {
		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class.
		/// </summary>
		public NotFoundException()
			: base("Core dependency not found") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		public NotFoundException(string Message)
			: base(Message) {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class.
		/// </summary>
		/// <param name="Message">The message.</param>
		/// <param name="Parent">The parent.</param>
		public NotFoundException(string Message, Exception Parent)
			: base(Message, Parent) {
		}
	}
}
