using System;

namespace MediaMotion.Core.Exceptions {
	/// <summary>
	/// Exception module not found
	/// </summary>
	public class ModuleNotFoundException : NotFoundException {
		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleNotFoundException"/> class.
		/// </summary>
		/// <param name="Name">The name.</param>
		public ModuleNotFoundException(string Name)
			: base(Name + " module not found") {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ModuleNotFoundException"/> class.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Parent">The parent.</param>
		public ModuleNotFoundException(string Name, Exception Parent)
			: base(Name + " module not found", Parent) {
		}
	}
}
