using MediaMotion.Core.Resolver.Registrations.Interfaces;

namespace MediaMotion.Core.Resolver.Containers.Interfaces {
	/// <summary>
	/// Container builder interface
	/// </summary>
	public interface IContainerBuilder {
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
		/// Build the container
		/// </summary>
		/// <returns>The container</returns>
		IContainer Build();
	}
}
