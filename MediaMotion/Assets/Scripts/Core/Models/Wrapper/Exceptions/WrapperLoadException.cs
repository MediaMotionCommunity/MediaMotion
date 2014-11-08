using System;

namespace MediaMotion.Core.Models.Wrapper.Exceptions {
	public class WrapperLoadException : Exception {
		public WrapperLoadException() {

		}

		public WrapperLoadException(string Message)
			: base(Message) {

		}

		public WrapperLoadException(string Message, Exception Parent)
			: base(Message, Parent) {

		}
	}
}
