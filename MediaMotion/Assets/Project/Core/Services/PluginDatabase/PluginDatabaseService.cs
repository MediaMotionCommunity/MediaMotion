using System.Collections.Generic;
using MediaMotion.Core.Models.Core;
using MediaMotion.Core.Models.Service;
using MediaMotion.Core.Services.PluginDatabase.Interfaces;

namespace MediaMotion.Core.Services.PluginDatabase {
	/// <summary>
	/// Plugin Database Service
	/// </summary>
	public class PluginDatabaseService : ServiceBase, IPluginDatabase {
		/// <summary>
		/// Prevents a default instance of the <see cref="PluginDatabaseService"/> class from being created.
		/// </summary>
		private PluginDatabaseService(ICore Core)
			: base(Core) {
		}

		/// <summary>
		/// Gets the plugin available.
		/// </summary>
		/// <returns>List of available plugin</returns>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public List<string> GetPluginAvailable() {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets the default plugin for extension.
		/// </summary>
		/// <param name="Extension">The extension.</param>
		/// <returns>The default plugin</returns>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public string GetDefaultPluginForExtension(string Extension) {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Gets all plugin for extension.
		/// </summary>
		/// <param name="Extension">The extension.</param>
		/// <returns>all plugins</returns>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public List<string> GetAllPluginForExtension(string Extension) {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Adds the plugin.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Location">The location.</param>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void AddPlugin(string Name, string Location) {
			throw new System.NotImplementedException();
		}

		/// <summary>
		/// Removes the plugin.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <exception cref="System.NotImplementedException">Not implemented</exception>
		public void RemovePlugin(string Name) {
			throw new System.NotImplementedException();
		}
	}
}
