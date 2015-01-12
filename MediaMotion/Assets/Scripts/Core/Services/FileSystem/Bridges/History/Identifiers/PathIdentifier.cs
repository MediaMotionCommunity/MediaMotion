﻿using Mediamotion.Core.Services.History.Interfaces;

namespace Mediamotion.Core.Services.FileSystem.Bridges.History.Identifiers {
	public class PathIdentifier : IIdentifier {
		/// <summary>
		/// The path
		/// </summary>
		private readonly string Path;

		/// <summary>
		/// Initializes a new instance of the <see cref="PathIdentifier"/> class.
		/// </summary>
		/// <param name="Path">The path.</param>
		public PathIdentifier(string Path = "") {
			this.Path = Path;
		}

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <returns></returns>
		public string GetPath() {
			return (this.Path);
		}

		/// <summary>
		/// Compare two instances
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns></returns>
		public bool Equals(IIdentifier Identifier) {
			return ((Identifier is PathIdentifier) && (((PathIdentifier)Identifier).Path.Equals(this.Path)));
		}
	}
}
