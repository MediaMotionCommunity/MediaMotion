using System;

namespace MediaMotion.Core.Exceptions {
	class WrapperNotFoundException : NotFoundException {
		public WrapperNotFoundException(string Name)
			: base(Name + " wrapper not found") {

		}

		public WrapperNotFoundException(string Name, Exception Parent)
			: base(Name + " wrapper not found", Parent) {

		}
	}
}
