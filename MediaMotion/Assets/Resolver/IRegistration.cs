using System;

namespace MediaMotion.Resolver {
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
		object Resolve();

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
		void Build();

		/// <summary>
		/// The single instance.
		/// </summary>
		/// <returns>
		/// The <see cref="IRegistration"/>.
		/// </returns>
		IRegistration SingleInstance();
	}
}
