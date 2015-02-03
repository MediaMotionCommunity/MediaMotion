using System;
using System.Collections.Generic;

using MediaMotion.Core.Resolver.Exceptions;
using MediaMotion.Core.Resolver.Activators;
using MediaMotion.Core.Resolver.Activators.Interfaces;
using MediaMotion.Core.Resolver.Registrations.Interfaces;

namespace MediaMotion.Core.Resolver.Registrations {
	/// <summary>
	/// The registration.
	/// </summary>
	/// <typeparam name="Service">The service</typeparam>
	internal class Registration<Service> : IRegistration
		where Service : class {
		/// <summary>
		/// The service type
		/// </summary>
		private readonly Type type;

		/// <summary>
		/// The activator class.
		/// </summary>
		private readonly IActivatorClass<Service> activatorClass;

		/// <summary>
		/// The other type.
		/// </summary>
		private List<Type> types;

		/// <summary>
		/// Initializes a new instance of the <see cref="Registration{Service}"/> class.
		/// </summary>
		/// <param name="activatorClass">The activator class.</param>
		public Registration(IActivatorClass<Service> activatorClass) {
			this.type = typeof(Service);
			this.types = new List<Type>(new Type[] { this.type });
			this.activatorClass = activatorClass;
		}

		/// <summary>
		/// The single instance.
		/// </summary>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		public IRegistration SingleInstance() {
			this.activatorClass.SingleInstance = true;
			return (this);
		}

		/// <summary>
		/// The build.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		public void Build(Resolver resolver) {
			this.activatorClass.Build(resolver);
		}

		/// <summary>
		/// Get the service
		/// </summary>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		public object Get() {
			return (this.activatorClass.Get());
		}

		/// <summary>
		/// The as.
		/// </summary>
		/// <typeparam name="Alias">The alias type</typeparam>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		public IRegistration As<Alias>() {
			Type alias = typeof(Alias);

			if (!alias.IsAssignableFrom(this.type)) {
				throw new NotAssignableTypeException("This type is not assignable to base type");
			}
			if (!this.types.Contains(alias)) {
				this.types.Add(alias);
			}
			return (this);
		}

		/// <summary>
		/// The is type.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool IsType(Type type) {
			return (this.types.Contains(type));
		}
	}
}