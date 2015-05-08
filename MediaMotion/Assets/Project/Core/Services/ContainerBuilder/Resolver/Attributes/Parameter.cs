﻿using System;

namespace MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes {
	/// <summary>
	/// Parameter Attribute
	/// </summary>
	[AttributeUsage(System.AttributeTargets.Parameter, AllowMultiple = true)]
	public class Parameter : System.Attribute {
		/// <summary>
		/// Gets the key.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		public string key { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Parameter"/> class.
		/// </summary>
		/// <param name="key">The key.</param>
		public Parameter(string key) {
			this.key = key;
		}
	}
}