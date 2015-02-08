using System.Collections.Generic;
using MediaMotion.Core.Resolver.Registrations.Interfaces;

namespace MediaMotion.Core.Resolver.Containers.Interfaces {
	/// <summary>
	/// Container builder interface
	/// </summary>
	public interface IContainerBuilder {
		/// <summary>
		/// Adds the builder.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The builder</returns>
		IContainerBuilder Add(IContainerBuilder builder);

		/// <summary>
		/// Adds the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>The builder</returns>
		IContainerBuilder Add(IContainer container);

		/// <summary>
		/// Registers a service
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>The registration</returns>
		IRegistration Register<Service>() where Service : class;

		/// <summary>
		/// Registers the specified instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <returns>The registration</returns>
		IRegistration Register<Service>(Service instance) where Service : class;

		/// <summary>
		/// Gets the registrations.
		/// </summary>
		/// <returns>the registrations</returns>
		IEnumerable<IRegistration> GetRegistrations();

		/// <summary>
		/// Build the container
		/// </summary>
		/// <returns>
		/// The container
		/// </returns>
		IContainer Build();
	}
}
