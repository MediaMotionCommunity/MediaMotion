namespace MediaMotion.Core.Services.Logger.Interfaces {
	public interface IChannel {
		void Panic(string Message, object Context = null);
		void Critical(string Message, object Context = null);
		void Error(string Message, object Context = null);
		void Info(string Message, object Context = null);
		void Debug(string Message, object Context = null);
	}
}
