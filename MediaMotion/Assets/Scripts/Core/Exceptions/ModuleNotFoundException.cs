using System;

namespace MediaMotion.Core.Exceptions {
	class ModuleNotFoundException : NotFoundException {
		public ModuleNotFoundException(string Name)
			: base(Name + " module not found") {

		}

		public ModuleNotFoundException(string Name, Exception Parent)
			: base(Name + " module not found", Parent) {

		}
	}
}
