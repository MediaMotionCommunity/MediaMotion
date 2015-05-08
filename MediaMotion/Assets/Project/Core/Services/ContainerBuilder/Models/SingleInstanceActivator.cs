using System;
using MediaMotion.Core.Services.ContainerBuilder.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models {
	/// <summary>
	/// Single Instance Activator
	/// </summary>
	public class SingleInstanceActivator : AActivator {
		/// <summary>
		/// The service
		/// </summary>
		private object service;

		/// <summary>
		/// Initializes a new instance of the <see cref="SingleInstanceActivator"/> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="serviceType">Type of the service.</param>
		/// <param name="service">The service.</param>
		/// <exception cref="System.Exception">the provided instance does not match the provided type</exception>
		public SingleInstanceActivator(IResolverService resolver, Type serviceType, object service = null)
			: base(resolver, serviceType) {
			if (service != null && !service.GetType().IsAssignableFrom(serviceType)) {
				throw new Exception("the provided instance does not match the provided type");
			}
			this.service = service;
		}

		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the build was correctly performed, <c>false</c> otherwise
		/// </returns>
		public override bool Build() {
			if (this.service == null) {
				return (base.Build());
			}
			return (true);
		}

		/// <summary>
		/// Gets the service
		/// </summary>
		/// <returns>
		/// The service
		/// </returns>
		public override object Get() {
			if (this.service == null) {
				object[] parameters = this.resolver.ResolveParameters(this.constructorInfo.GetParameters());

				this.service = this.constructorInfo.Invoke(parameters);
			}
			return (this.service);
		}
	}
}
