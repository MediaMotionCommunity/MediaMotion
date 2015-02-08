using System;

namespace MediaMotion.Core.Resolver.Containers.Interfaces {
	/// <summary>
	/// Container interface
	/// </summary>
	public interface IContainer {
		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		/// The service
		/// </returns>
		Service Get<Service>() where Service : class;

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object Get(Type type);

		/// <summary>
		/// Gets the resolver.
		/// </summary>
		/// <returns>The resolver</returns>
		Resolver GetResolver();
	}
}
