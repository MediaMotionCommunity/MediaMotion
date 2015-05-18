using System;
using MediaMotion.Core.Services.ContainerBuilder.Models.Abstracts;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Models {
	/// <summary>
	/// Single Instance Activator
	/// </summary>
	public class DefaultActivator : AActivator {
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultActivator" /> class.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="serviceType">Type of the service.</param>
		public DefaultActivator(IResolverService resolver, Type serviceType)
			: base(resolver, serviceType) {
		}

		/// <summary>
		/// Gets the service
		/// </summary>
		/// <returns>
		/// The service
		/// </returns>
		public override object Get() {
			object[] parameters = this.resolver.ResolveParameters(this.constructorInfo.GetParameters());

			return (this.constructorInfo.Invoke(parameters));
		}
	}
}
