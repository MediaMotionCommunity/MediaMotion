using System;
using System.Collections.Generic;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models {
	/// <summary>
	/// Service Definition
	/// </summary>
	public class Definition : IDefinition {
		/// <summary>
		/// The types list
		/// </summary>
		private readonly List<Type> typesList;

		/// <summary>
		/// The disposable
		/// </summary>
		private bool disposable;

		/// <summary>
		/// The single instance
		/// </summary>
		private bool singleInstance;

		/// <summary>
		/// Initializes a new instance of the <see cref="Definition"/> class.
		/// </summary>
		/// <param name="service">The service.</param>
		/// <param name="instance">The instance.</param>
		public Definition(Type service, object instance = null) {
			this.ServiceType = service;
			this.typesList = new List<Type>();
			this.typesList.Add(this.ServiceType);
			this.Instance = instance;
			this.disposable = typeof(IDisposable).IsAssignableFrom(service);
			if (this.Instance != null || this.disposable) {
				this.SingleInstance = true;
			}
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public object Instance { get; private set; }

		/// <summary>
		/// The service type
		/// </summary>
		public Type ServiceType { get; private set; }

		/// <summary>
		/// Gets the types.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		public Type[] Types {
			get { return (this.typesList.ToArray()); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether [single instance].
		/// </summary>
		/// <value>
		///   <c>true</c> if [single instance]; otherwise, <c>false</c>.
		/// </value>
		/// <exception cref="System.ArgumentException">SingleInstance cannot be false when the service is defined using an Instance or if the service need to free some managed/unmanaged memory/object (if it implement the IDisposable interface).</exception>
		public bool SingleInstance {
			get {
				return (this.singleInstance);
			}
			set {
				if ((this.Instance != null || this.disposable) && value == false) {
					throw new ArgumentException("SingleInstance cannot be false when the service is defined using an Instance or if the service need to free some managed/unmanaged memory/object (if it implement the IDisposable interface).");
				}
				this.singleInstance = value;
			}
		}

		/// <summary>
		/// Define an alias for the service type
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		/// itself
		/// </returns>
		public IDefinition As<Service>() where Service : class {
			return (this.As(typeof(Service)));
		}

		/// <summary>
		/// Define an alias for the service type
		/// </summary>
		/// <param name="service">The type of the service.</param>
		/// <returns>
		/// itself
		/// </returns>
		public IDefinition As(Type service) {
			if (!service.IsAssignableFrom(this.ServiceType)) {
				throw new Exception("This type is not assignable to base type");
			}
			if (!this.typesList.Contains(service)) {
				this.typesList.Add(service);
			}
			return (this);
		}
	}
}
