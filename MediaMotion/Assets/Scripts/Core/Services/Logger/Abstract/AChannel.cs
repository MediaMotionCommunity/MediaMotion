using MediaMotion.Core.Services.Logger.Interfaces;

namespace MediaMotion.Core.Services.Logger.Abstract {
	abstract public class AChannel : IChannel {

		public void Panic(string Message, object Context = null) {
		}

		public void Critical(string Message, object Context = null) {
		}

		public void Error(string Message, object Context = null) {
		}

		public void Info(string Message, object Context = null) {
		}

		public void Debug(string Message, object Context = null) {
		}
	}
}
