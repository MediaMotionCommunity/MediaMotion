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
			if (instance != null) {
				this.singleInstance = true;
			}
			this.Instance = instance;
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
		/// <exception cref="System.ArgumentException">SingleInstance cannot be false if an instance already exist</exception>
		public bool SingleInstance {
			get {
				return (this.singleInstance);
			}
			set {
				if (this.Instance != null && value == false) {
					throw new ArgumentException("SingleInstance cannot be false in this context");
				}
				this.singleInstance = value;
			}
		}

		/// <summary>
		/// Ases this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		/// itself
		/// </returns>
		public IDefinition As<Service>() where Service : class {
			return (this.As(typeof(Service)));
		}

		/// <summary>
		/// Ases the specified service.
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
