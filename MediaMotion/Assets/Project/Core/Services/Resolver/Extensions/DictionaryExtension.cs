using System.Collections.Generic;

namespace MediaMotion.Core.Services.Resolver.Extensions {
	/// <summary>
	/// Dictionary Extension
	/// </summary>
	public static class DictionaryExtension {
		/// <summary>
		/// Merges the specified dictionaries.
		/// </summary>
		/// <typeparam name="Key">The type of the ey.</typeparam>
		/// <typeparam name="Value">The type of the alue.</typeparam>
		/// <param name="me">Me.</param>
		/// <param name="dictionaries">The dictionaries.</param>
		/// <returns>
		///   Merged dictionary
		/// </returns>
		public static Dictionary<Key, Value> Merge<Key, Value>(this Dictionary<Key, Value> me, params Dictionary<Key, Value>[] dictionaries) {
			foreach (Dictionary<Key, Value> dictionary in dictionaries) {
				foreach (KeyValuePair<Key, Value> entry in dictionary) {
					me[entry.Key] = entry.Value;
				}
			}
			return (me);
		}
	}
}
