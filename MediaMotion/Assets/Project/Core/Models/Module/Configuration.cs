using System;
using System.Collections.Generic;
namespace MediaMotion.Core.Models.Module {
	/// <summary>
	/// Module configuration
	/// </summary>
	public sealed class Configuration {
		/// <summary>
		/// Gets or sets the services.
		/// </summary>
		/// <value>
		/// The services.
		/// </value>
		private List<Type> services;

		/// <summary>
		/// Initializes a new instance of the <see cref="Configuration"/> class.
		/// </summary>
		public Configuration() {
			this.services = new List<Type>();
		}

		/// <summary>
		/// Gets the name of the module.
		/// </summary>
		/// <value>
		/// The name of the module.
		/// </value>
		public string Name { get; set; }

		/// <summary>
		/// Gets the module scene.
		/// </summary>
		/// <value>
		/// The module scene.
		/// </value>
		public string Scene { get; set; }

		/// <summary>
		/// Gets the module description.
		/// </summary>
		/// <value>
		/// The module description.
		/// </value>
		public string Description { get; set; }

		/// <summary>
		/// Adds the service.
		/// </summary>
		/// <param name="service">The service.</param>
		public void AddService(Type service) {
			this.services.Add(service);
		}

		/// <summary>
		/// Gets the service.
		/// </summary>
		/// <returns>
		/// The services
		/// </returns>
		public List<Type> GetService() {
			return (this.services);
		}
	}
}
