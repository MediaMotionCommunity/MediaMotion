using System;

using MediaMotion.Core.Models.Service;

namespace MediaMotion.Core.Models.Core {
	/// <summary>
	/// Core Interface
	/// </summary>
	public interface ICore {
		/// <summary>
		/// The get service.
		/// </summary>
		/// <typeparam name="T">
		/// </typeparam>
		/// <returns>
		/// The <see cref="T"/>.
		/// </returns>
		T Resolve<T>() where T : class;

		/// <summary>
		/// The get service.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object Resolve(Type type);
	}
}
