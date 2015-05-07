using System;
using System.Collections.Generic;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models {
	/// <summary>
	/// Service Container
	/// </summary>
	public sealed class Container : IContainer {
		/// <summary>
		/// The parameters
		/// </summary>
		private readonly Dictionary<string, object> parameters;

		/// <summary>
		/// The services
		/// </summary>
		private readonly Dictionary<Type, IActivator> services;

		/// <summary>
		/// Initializes a new instance of the <see cref="Container"/> class.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="services">The services.</param>
		public Container(Dictionary<string, object> parameters, Dictionary<Type, IActivator> services) {
			this.parameters = parameters;
			this.services = services;
		}

		/// <summary>
		/// Gets the parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		public void GetParameters(out Dictionary<string, object> parameters) {
			parameters = new Dictionary<string, object>(this.parameters);
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		///   The parameter
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">name;No parameter with this name can be found</exception>
		public object GetParameter(string name) {
			object parameter;

			if (!this.parameters.TryGetValue(name, out parameter)) {
				throw new ArgumentOutOfRangeException("name", "No parameter with this name can be found");
			}
			return (parameter);
		}

		/// <summary>
		/// Determines whether the specified name has parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		///   <c>true</c> if a parameter with the specified name exist, <c>false</c> otherwise
		/// </returns>
		public bool HasParameter(string name) {
			return (this.parameters.ContainsKey(name));
		}

		/// <summary>
		/// Gets the services.
		/// </summary>
		/// <param name="services">The services.</param>
		public void GetServices(out Dictionary<Type, IActivator> services) {
			services = new Dictionary<Type, IActivator>(this.services);
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   The service
		/// </returns>
		public Service Get<Service>() where Service : class {
			return (this.Get(typeof(Service)) as Service);
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		/// The service
		/// </returns>
		/// <exception cref="System.ArgumentOutOfRangeException">type;The requested service cannot be found</exception>
		public object Get(Type type) {
			IActivator activator;

			if (!this.services.TryGetValue(type, out activator)) {
				throw new ArgumentOutOfRangeException("type", "The requested service cannot be found");
			}
			return (activator.Get());
		}

		/// <summary>
		/// Determines whether [has].
		/// </summary>
		/// <typeparam name="Service">The type of the ervice.</typeparam>
		/// <returns>
		///   <c>true</c> if the container has registered the service, <c>false</c> otherwise
		/// </returns>
		public bool Has<Service>() where Service : class {
			return (this.Has(typeof(Service)));
		}

		/// <summary>
		/// Determines whether [has] [the specified type].
		/// </summary>
		/// <param name="type">The type.</param>
		/// <returns>
		///   <c>true</c> if the container has registered the service, <c>false</c> otherwise
		/// </returns>
		public bool Has(Type type) {
			return (this.services.ContainsKey(type));
		}
	}
}
