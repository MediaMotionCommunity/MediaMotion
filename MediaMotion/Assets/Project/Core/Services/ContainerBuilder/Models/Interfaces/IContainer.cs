using System;
using System.Collections.Generic;

namespace MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces {
	/// <summary>
	/// Service Container interface
	/// </summary>
	public interface IContainer : IDisposable {
		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		///   The parameter
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">name;No parameter with this name can be found</exception>
		object GetParameter(string name);

		/// <summary>
		/// Determines whether the specified name has parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		///   <c>true</c> if a parameter with the specified name exist, <c>false</c> otherwise
		/// </returns>
		bool HasParameter(string name);

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   The service
		/// </returns>
		Service Get<Service>() where Service : class;

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		/// The service
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">type;The requested service cannot be found</exception>
		object Get(Type type);

		/// <summary>
		/// Determines whether [has].
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   <c>true</c> if the container has registered the service, <c>false</c> otherwise
		/// </returns>
		bool Has<Service>() where Service : class;

		/// <summary>
		/// Determines whether [has] [the specified type].
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if the container has registered the service, <c>false</c> otherwise
		/// </returns>
		bool Has(Type type);
	}
}
