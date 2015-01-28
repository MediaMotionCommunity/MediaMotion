using System.Collections.Generic;

namespace MediaMotion.Core.Services.PluginDatabase.Interfaces {
	/// <summary>
	/// Plugin Database Interface
	/// </summary>
	public interface IPluginDatabaseService {
		/// <summary>
		/// Gets the plugin available.
		/// </summary>
		/// <returns>List of available plugin</returns>
		List<string> GetPluginAvailable();

		/// <summary>
		/// Gets the default plugin for extension.
		/// </summary>
		/// <param name="Extension">The extension.</param>
		/// <returns>The default plugin</returns>
		string GetDefaultPluginForExtension(string Extension);

		/// <summary>
		/// Gets all plugin for extension.
		/// </summary>
		/// <param name="Extension">The extension.</param>
		/// <returns>all plugins</returns>
		List<string> GetAllPluginForExtension(string Extension);

		/// <summary>
		/// Adds the plugin.
		/// </summary>
		/// <param name="Name">The name.</param>
		/// <param name="Location">The location.</param>
		void AddPlugin(string Name, string Location);

		/// <summary>
		/// Removes the plugin.
		/// </summary>
		/// <param name="Name">The name.</param>
		void RemovePlugin(string Name);
	}
}
