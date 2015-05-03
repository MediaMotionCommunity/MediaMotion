using System;
using System.Collections.Generic;

namespace MediaMotion.Core.Services.Resolver.Models.Interfaces {
	/// <summary>
	/// Container Interface
	/// </summary>
	public interface IContainer {
		/// <summary>
		/// The service definitions
		/// </summary>
		/// <value>
		///   The services.
		/// </value>
		Dictionary<Type, IServiceDefinition> Services { get; }

		/// <summary>
		/// The parameters
		/// </summary>
		/// <value>
		///   The parameters.
		/// </value>
		Dictionary<string, object> Parameters { get; }

		/// <summary>
		/// Determines whether the specified key has parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the parameter exists, <c>false</c> otherwise
		/// </returns>
		bool HasParameter(string key);

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   The parameter
		/// </returns>
		object GetParameter(string key);

		/// <summary>
		/// Determines whether [has] [the specified service].
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns>
		///   <c>true</c> if the service exists, <c>false</c> otherwise
		/// </returns>
		bool Has(Type service);

		/// <summary>
		/// Determines whether [has] [the specified service].
		/// </summary>
		/// <typeparam name="Service">The type of the ervice.</typeparam>
		/// <returns>
		///   <c>true</c> if the service exists, <c>false</c> otherwise
		/// </returns>
		bool Has<Service>();

		/// <summary>
		/// Gets the specified service.
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns>
		///   The service
		/// </returns>
		object Get(Type service);

		/// <summary>
		/// Gets the requested service.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   The service
		/// </returns>
		Service Get<Service>();
	}
}
