using System;

namespace MediaMotion.Core.Resolver.Containers.Interfaces {
	public interface IContainer {
		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the ervice.</typeparam>
		/// <returns></returns>
		Service Get<Service>() where Service : class;

		/// <summary>
		/// The resolve.
		/// </summary>
		/// <param name="type">
		/// The type.
		/// </param>
		/// <returns>
		/// The <see cref="object"/>.
		/// </returns>
		object Get(Type type);
	}
}
