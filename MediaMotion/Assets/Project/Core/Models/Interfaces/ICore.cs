using System;
using System.Reflection;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Resolver.Containers.Interfaces;

namespace MediaMotion.Core.Models.Interfaces {
	/// <summary>
	/// Core Interface
	/// </summary>
	public interface ICore {
		/// <summary>
		/// Adds the service builder.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The service container</returns>
		IContainer AddServices(IContainerBuilder builder);

		/// <summary>
		/// Adds the services.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>The service container</returns>
		IContainer AddServices(IContainer container);

		/// <summary>
		/// Gets the services container.
		/// </summary>
		/// <returns>The service container</returns>
		IContainer GetServicesContainer();
	}
}
