using System;

namespace MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes {
	/// <summary>
	/// Parameter Attribute
	/// </summary>
	[AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = true)]
	public class Parameter : System.Attribute {
		/// <summary>
		/// Initializes a new instance of the <see cref="Parameter"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		public Parameter(string key) {
			this.Key = key;
		}

		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string Key { get; private set; }
	}
}
