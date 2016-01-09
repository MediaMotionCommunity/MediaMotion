using System;
using System.Collections.Generic;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models {
	/// <summary>
	/// Service Container
	/// </summary>
	public sealed class Container : IContainer {
		/// <summary>
		/// The parent
		/// </summary>
		private readonly IContainer parent;

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
		public Container(IContainer parent, Dictionary<string, object> parameters, Dictionary<Type, IActivator> services) {
			this.parent = parent;
			this.parameters = parameters;
			this.services = services;
		}

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose() {
			foreach (KeyValuePair<string, object> entry in this.parameters) {
				if (entry.Value is IDisposable) {
					((IDisposable)entry.Value).Dispose();
				}
			}
			this.parameters.Clear();
			foreach (KeyValuePair<Type, IActivator> entry in this.services) {
				if (entry.Value is IDisposable) {
					((IDisposable)entry.Value).Dispose();
				}
			}
			this.services.Clear();
		}

		/// <summary>
		/// Gets the parameter.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>
		/// The parameter
		/// </returns>
		/// <exception cref="System.ArgumentException">No parameter with this name can be found.;name</exception>
		public object GetParameter(string name) {
			object parameter;

			if (!this.parameters.TryGetValue(name, out parameter)) {
				if (this.parent != null) {
					return (this.parent.GetParameter(name));
				}
				throw new ArgumentException("No parameter with this name can be found.", "name");
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
			return (this.parameters.ContainsKey(name) || (this.parent != null && this.parent.HasParameter(name)));
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
		/// <exception cref="System.ArgumentException">The requested service cannot be found;type</exception>
		public object Get(Type type) {
			IActivator activator;

			if (!this.services.TryGetValue(type, out activator)) {
				if (this.parent != null) {
					return (this.parent.Get(type));
				}
				throw new ArgumentException("The requested service cannot be found", "type");
			}
			return (activator.Get());
		}

		/// <summary>
		/// Determines whether [has].
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
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
			return (this.services.ContainsKey(type) || (this.parent != null && this.parent.Has(type)));
		}
	}
}
