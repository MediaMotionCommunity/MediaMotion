using System.Collections.Generic;
using MediaMotion.Core.Services.PluginDatabase.Interfaces;

namespace MediaMotion.Core.Services.PluginDatabase {
	class PluginDatabase : IPluginDatabase {
		public List<string> GetPluginAvailable() {
			throw new System.NotImplementedException();
		}

		public string GetDefaultPluginForExtension(string Extension) {
			throw new System.NotImplementedException();
		}

		public List<string> GetAllPluginForExtension(string Extension) {
			throw new System.NotImplementedException();
		}

		public void AddPlugin(string Name, string Location) {
			throw new System.NotImplementedException();
		}

		public void RemovePlugin(string Name) {
			throw new System.NotImplementedException();
		}
	}
}
