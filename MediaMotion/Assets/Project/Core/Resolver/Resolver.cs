using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Resolver.Exceptions;
using MediaMotion.Core.Resolver.Registrations.Interfaces;

namespace MediaMotion.Core.Resolver {
	/// <summary>
	/// The resolver.
	/// </summary>
	public class Resolver {
		/// <summary>
		/// The registrations.
		/// </summary>
		private readonly IEnumerable<IRegistration> registrations;

		/// <summary>
		/// Initializes a new instance of the <see cref="Resolver"/> class.
		/// </summary>
		/// <param name="registrations">
		/// The registrations.
		/// </param>
		public Resolver(IEnumerable<IRegistration> registrations) {
			this.registrations = registrations;
		}

		/// <summary>
		/// Resolve the service
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>The service</returns>
		public Service Get<Service>() where Service : class {
			return (this.Get(typeof(Service)) as Service);
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
		/// <exception cref="TypeNotFoundException">
		/// When the type is not register in builder
		/// </exception>
		public object Get(Type type) {
			Debug.Assert(this.registrations != null, "Registration is null");
			IRegistration registration = this.registrations.FirstOrDefault(r => r.IsType(type));

			if (registration == null) {
				throw new TypeNotFoundException("The type '" + type.Name + "' is not registered.");
			}
			return (registration.Get());
		}

		/// <summary>
		/// The is register type.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool IsRegisterType<T>() {
			return (this.IsRegisterType(typeof(T)));
		}

		/// <summary>
		/// The is register type.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		public bool IsRegisterType(Type type) {
			return (this.registrations.Any(r => r.IsType(type)));
		}

		/// <summary>
		/// Gets the registrations.
		/// </summary>
		/// <returns>The registrations</returns>
		public IEnumerable<IRegistration> GetRegistrations() {
			return (this.registrations);
		}
	}
}