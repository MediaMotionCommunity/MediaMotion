using System;
using System.Collections.Generic;
using System.Linq;

using MediaMotion.Resolver.Exceptions;

namespace MediaMotion.Resolver {
	/// <summary>
	/// The resolver.
	/// </summary>
	internal class Resolver {
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
		/// The resolve.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		public T Resolve<T>() where T : class {
			var type = typeof(T);
			return (T)this.Resolve(type);
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
		public object Resolve(Type type) {
			var registration = this.registrations.FirstOrDefault(r => r.IsType(type));
			if (registration == null) {
				throw new TypeNotFoundException("This type is not register");
			}
			return registration.Resolve();
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
			return this.IsRegisterType(typeof(T));
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
			return this.registrations.Any(r => r.IsType(type));
		}
	}
}