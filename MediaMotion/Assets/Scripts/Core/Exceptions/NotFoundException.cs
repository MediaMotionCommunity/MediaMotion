using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaMotion.Core.Exceptions {
	class NotFoundException : Exception {
		public NotFoundException()
			: base("Core dependency not found") {
		}

		public NotFoundException(string Message)
			: base(Message) {
		}

		public NotFoundException(string Message, Exception Parent)
			: base(Message, Parent) {
		}
	}
}
