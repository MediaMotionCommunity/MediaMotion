using System.Collections.Generic;
using MediaMotion.Core.Services.Logger.Interfaces;

namespace MediaMotion.Core.Services.Logger {
	public class Logger : ILogger {
		private Dictionary<string, IChannel> Channels;

		public Logger() {
		}
	}
}
