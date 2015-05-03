using System;
using System.Collections.Generic;
using MediaMotion.Core.Services.Resolver.Models.Interfaces;

namespace MediaMotion.Core.Services.Resolver.Models {
	/// <summary>
	/// Container Service
	/// </summary>
	public class Container : IContainer {
		/// <summary>
		/// The service definitions
		/// </summary>
		public Dictionary<Type, IServiceDefinition> Services { get; private set; }

		/// <summary>
		/// The parameters
		/// </summary>
		public Dictionary<string, object> Parameters { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerService" /> class.
		/// </summary>
		/// <param name="services">The services.</param>
		/// <param name="parameters">The parameters.</param>
		public Container(Dictionary<Type, IServiceDefinition> services = null, Dictionary<string, object> parameters = null) {
			this.Services = services;
			this.Parameters = parameters;
		}

		/// <summary>
		/// Determines whether the specified key has parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		///   <c>true</c> if the parameter exists, <c>false</c> otherwise
		/// </returns>
		public bool HasParameter(string key) {
			return (this.Parameters != null && this.Parameters.ContainsKey(key));
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>
		/// The parameter
		/// </returns>
		public object GetParameter(string key) {
			if (this.Parameters != null && this.Parameters.ContainsKey(key)) {
				object parameter;

				if (!this.Parameters.TryGetValue(key, out parameter)) {
					throw new Exception("Unknown error");
				}
				return (parameter);
			}
			throw new ArgumentOutOfRangeException("key", "Cannot find any parameter with this key.");
		}

		/// <summary>
		/// Determines whether [has] [the specified service].
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns>
		///   <c>true</c> if the service exists, <c>false</c> otherwise
		/// </returns>
		public bool Has(Type service) {
			return (this.Services != null && this.Services.ContainsKey(service));
		}

		/// <summary>
		/// Determines whether [has] [the specified service].
		/// </summary>
		/// <typeparam name="Service">The type of the ervice.</typeparam>
		/// <returns>
		///   <c>true</c> if the service exists, <c>false</c> otherwise
		/// </returns>
		public bool Has<Service>() {
			return (this.Has(typeof(Service)));
		}

		/// <summary>
		/// Gets the specified service.
		/// </summary>
		/// <param name="service">The service.</param>
		/// <returns>
		///   The service
		/// </returns>
		public object Get(Type service) {
			if (this.Services != null && this.Services.ContainsKey(service)) {
				IServiceDefinition serviceDefinition;

				if (!this.Services.TryGetValue(service, out serviceDefinition)) {
					throw new Exception("Unknown error");
				}
				return (serviceDefinition.Get());
			}
			throw new Exception("Not found");
		}

		/// <summary>
		/// Gets the requested service.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   The service
		/// </returns>
		public Service Get<Service>() {
			return ((Service)this.Get(typeof(Service)));
		}
	}
}
