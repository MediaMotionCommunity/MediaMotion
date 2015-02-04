using System;
using System.Collections.Generic;
using System.Reflection;
using MediaMotion.Core.Resolver.Containers.Interfaces;

namespace MediaMotion.Core.Resolver.Containers {
	/// <summary>
	/// Service Container
	/// </summary>
	public class Container : IContainer {
		/// <summary>
		/// The resolver
		/// </summary>
		private readonly Resolver resolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="Container"/> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		public Container(Resolver resolver) {
			this.resolver = resolver;
		}

		/// <summary>
		/// Get the service
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns></returns>
		public Service Get<Service>() where Service : class {
			return (this.resolver.Get<Service>());
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object Get(Type type) {
			return (this.resolver.Get(type));
		}

		/// <summary>
		/// Gets the resolver.
		/// </summary>
		/// <returns></returns>
		public Resolver GetResolver() {
			return (this.resolver);
		}
	}
}
