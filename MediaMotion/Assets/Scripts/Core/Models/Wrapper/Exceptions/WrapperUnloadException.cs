using System;

namespace MediaMotion.Core.Models.Wrapper.Exceptions {
	public class WrapperUnloadException : Exception {
		public WrapperUnloadException() {

		}

		public WrapperUnloadException(string Message)
			: base(Message) {

		}

		public WrapperUnloadException(string Message, Exception Parent)
			: base(Message, Parent) {

		}
	}
}
