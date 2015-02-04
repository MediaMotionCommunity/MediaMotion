using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Resolver.Activators;
using MediaMotion.Core.Resolver.Containers.Interfaces;
using MediaMotion.Core.Resolver.Registrations;
using MediaMotion.Core.Resolver.Registrations.Interfaces;
using UnityEngine;

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
		/// The locker
		/// </summary>
		private readonly object locker = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBuilder"/> class.
		/// </summary>
		public ContainerBuilder() {
			this.registrations = new List<IRegistration>();
		}

		/// <summary>
		/// Adds the builder.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <returns>The builder</returns>
		public IContainerBuilder Add(IContainerBuilder builder) {
			lock (this.locker) {
				this.registrations.AddRange(builder.GetRegistrations());
				return (this);
			}
		}

		/// <summary>
		/// Adds the container.
		/// </summary>
		/// <param name="container">The container.</param>
		/// <returns>The builder</returns>
		public IContainerBuilder Add(IContainer container) {
			lock (this.locker) {
				this.registrations.AddRange(container.GetResolver().GetRegistrations());
				return (this);
			}
		}

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>The registration</returns>
		public IRegistration Register<Service>() where Service : class {
			lock (this.locker) {
				Registration<Service> registration = new Registration<Service>(new TypeActivatorClass<Service>());

				this.registrations.Add(registration);
				return (registration);
			}
		}

		/// <summary>
		/// Registers the specified instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <returns>The registration</returns>
		/// <exception cref="System.ArgumentNullException">instance must not be null</exception>
		public IRegistration Register<Service>(Service instance) where Service : class {
			lock (this.locker) {
				if (instance == null) {
					throw new ArgumentNullException("instance must not be null");
				}
				Registration<Service> registration = new Registration<Service>(new SimpleActivatorClass<Service>(instance));

				this.registrations.Add(registration);
				return (registration);
			}
		}

		/// <summary>
		/// Gets the registrations.
		/// </summary>
		/// <returns>the registrations</returns>
		public IEnumerable<IRegistration> GetRegistrations() {
			return (this.registrations);
		}

		/// <summary>
		/// The build.
		/// </summary>
		/// <returns>
		/// The container
		/// </returns>
		public IContainer Build() {
			lock (this.locker) {
				Resolver resolver = new Resolver(this.registrations);

				foreach (IRegistration registration in this.registrations) {
					registration.Build(resolver);
				}
				return (new Container(resolver));
			}
		}
	}
}
