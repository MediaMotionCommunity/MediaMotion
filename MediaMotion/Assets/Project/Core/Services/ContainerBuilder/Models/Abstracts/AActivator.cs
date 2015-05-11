using System;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models.Abstracts {
	/// <summary>
	/// Activator Abstract
	/// </summary>
	public abstract class AActivator : IActivator {
		/// <summary>
		/// The resolver
		/// </summary>
		protected IResolverService resolver;

		/// <summary>
		/// The parameter
		/// </summary>
		protected ConstructorInfo constructorInfo;

		/// <summary>
		/// Initializes a new instance of the <see cref="AActivator" /> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="serviceType">Type of the service.</param>
		/// <exception cref="System.Exception">the provided instance does not match the provided type</exception>
		public AActivator(IResolverService resolver, Type serviceType) {
			this.resolver = resolver;
			this.ServiceType = serviceType;
		}

		/// <summary>
		/// Gets the type of the service.
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		public Type ServiceType { get; private set; }

		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the build was correctly performed, <c>false</c> otherwise
		/// </returns>
		public virtual bool Build() {
			foreach (ConstructorInfo constructorInfo in this.ServiceType.GetConstructors()) {
				if (constructorInfo.GetParameters().All(parameter => this.resolver.IsResolvable(parameter))) {
					this.constructorInfo = constructorInfo;
					return (true);
				}
			}
			return (false);
		}

		/// <summary>
		/// Gets the service
		/// </summary>
		/// <returns>
		/// The service
		/// </returns>
		public abstract object Get();
	}
}
