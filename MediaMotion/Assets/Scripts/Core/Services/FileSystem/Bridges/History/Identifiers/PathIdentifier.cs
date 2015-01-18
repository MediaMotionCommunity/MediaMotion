using MediaMotion.Core.Services.History.Interfaces;

namespace MediaMotion.Core.Services.FileSystem.Bridges.History.Identifiers {
	/// <summary>
	/// Identifier Path
	/// </summary>
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
		/// <returns>The path</returns>
		public string GetPath() {
			return (this.Path);
		}

		/// <summary>
		/// Compare two instances
		/// </summary>
		/// <param name="Identifier">The identifier.</param>
		/// <returns>True if the identifier are equals, False otherwise</returns>
		public bool Equals(IIdentifier Identifier) {
			return ((Identifier is PathIdentifier) && (((PathIdentifier)Identifier).Path.Equals(this.Path)));
		}
	}
}
