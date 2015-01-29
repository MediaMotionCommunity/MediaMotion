using System;
using System.Collections.Generic;

using MediaMotion.Resolver.Activators;

namespace MediaMotion.Resolver {
	/// <summary>
	/// The container builder.
	/// </summary>
	public class ContainerBuilder {
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
		/// The register instance.
		/// </summary>
		/// <param name="instance">
		/// The instance.
		/// </param>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// instance must be not null
		/// </exception>
		public IRegistration RegisterInstance<T>(T instance) where T : class {
			if (instance == null) {
				throw new ArgumentNullException();
			}

			var registration = new Registration<T>(new SimpleActivatorClass<T>(instance));
			this.registrations.Add(registration);
			return registration;
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public T Resolve<T>() where T : class {
			return this.resolver.Resolve<T>();
		}

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object Resolve(Type type) {
			return this.resolver.Resolve(type);
		}

		/// <summary>
		/// The register type.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		public IRegistration RegisterType<T>() where T : class {
			var registration = new Registration<T>(new TypeActivatorClass<T>(this.resolver));
			this.registrations.Add(registration);
			return registration;
		}

		/// <summary>
		/// The build.
		/// </summary>
		public void Build() {
			foreach (var registration in this.registrations) {
				registration.Build();
			}
		}
	}
}
