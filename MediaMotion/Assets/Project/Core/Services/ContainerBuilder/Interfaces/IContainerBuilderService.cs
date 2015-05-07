using System;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Interfaces {
	/// <summary>
	/// Service Container builder interface
	/// </summary>
	public interface IContainerBuilderService {
		/// <summary>
		/// Builds the specified parent.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns>
		///   The container
		/// </returns>
		IContainer Build(IContainer parent = null);

		/// <summary>
		/// Clears this instance.
		/// </summary>
		void Clear();

		/// <summary>
		/// Registers the specified instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <returns>
		///   The service definition
		/// </returns>
		IDefinition Register<Service>(Service instance = null) where Service : class;

		/// <summary>
		/// Registers the specified service.
		/// </summary>
		/// <param name="service">The type of the service.</param>
		/// <param name="instance">The instance.</param>
		/// <returns>
		///   The service definition
		/// </returns>
		IDefinition Register(Type service, object instance = null);
	}
}
