using System;

namespace MediaMotion.Core.Resolver.Registrations.Interfaces {
	/// <summary>
	/// The Registration interface.
	/// </summary>
	public interface IRegistration {
		/// <summary>
		/// The resolve.
		/// </summary>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object Get();

		/// <summary>
		/// The as.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		IRegistration As<T>();

		/// <summary>
		/// The is type.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="bool"/>.
		/// </returns>
		bool IsType(Type type);

		/// <summary>
		/// The build.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		void Build(Resolver resolver);

		/// <summary>
		/// The single instance.
		/// </summary>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		IRegistration SingleInstance();
	}
}
