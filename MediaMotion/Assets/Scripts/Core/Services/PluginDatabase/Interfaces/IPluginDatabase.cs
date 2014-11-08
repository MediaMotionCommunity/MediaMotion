using System.Collections.Generic;

namespace MediaMotion.Core.Services.PluginDatabase.Interfaces {
	public interface IPluginDatabase {
		List<string> GetPluginAvailable();

		string GetDefaultPluginForExtension(string Extension);

		List<string> GetAllPluginForExtension(string Extension);

		void AddPlugin(string Name, string Location);

		void RemovePlugin(string Name);
	}
}
