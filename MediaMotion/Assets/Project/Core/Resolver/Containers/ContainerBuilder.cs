using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using MediaMotion.Core.Resolver.Activators;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Resolver.Registrations.Interfaces;
using MediaMotion.Core.Resolver.Registrations;

namespace MediaMotion.Core.Resolver.Containers {
	/// <summary>
	/// The container builder.
	/// </summary>
	public class ContainerBuilder : IContainerBuilder {
		/// <summary>
		/// The registrations.
		/// </summary>
		private readonly List<IRegistration> registrations;

		/// <summary>
		/// The resolver.
		/// </summary>
		private readonly Resolver resolver;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
		/// </summary>
		public ContainerBuilder() {
			this.registrations = new List<IRegistration>();
			this.resolver = new Resolver(this.registrations);
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>The registration</returns>
		public IRegistration Register<Service>() where Service : class {
			Registration<Service> registration = new Registration<Service>(new TypeActivatorClass<Service>(this.resolver));

			this.registrations.Add(registration);
			return (registration);
		}

		/// <summary>
		/// Registers the specified instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <returns>The registration</returns>
		/// <exception cref="System.ArgumentNullException">instance must not be null</exception>
		public IRegistration Register<Service>(Service instance) where Service : class {
			if (instance == null) {
				throw new ArgumentNullException("instance must not be null");
			}
			Registration<Service> registration = new Registration<Service>(new SimpleActivatorClass<Service>(instance));

			this.registrations.Add(registration);
			return (registration);
		}

		/// <summary>
		/// The build.
		/// </summary>
		public IContainer Build() {
			foreach (IRegistration registration in this.registrations) {
				registration.Build();
			}
			return (new Container(this.resolver));
		}
	}
}
