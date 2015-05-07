using System;

namespace MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces {
	/// <summary>
	/// Service Definition interface
	/// </summary>
	public interface IDefinition {
		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		object Instance { get; }

		/// <summary>
		/// Gets the type of the service.
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		Type ServiceType { get; }

		/// <summary>
		/// Gets the types.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		Type[] Types { get; }

		/// <summary>
		/// Gets or sets a value indicating whether [single instance].
		/// </summary>
		/// <value>
		///   <c>true</c> if [single instance]; otherwise, <c>false</c>.
		/// </value>
		bool SingleInstance { get; set; }

		/// <summary>
		/// Ases this instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <returns>
		///   itself
		/// </returns>
		IDefinition As<Service>() where Service : class;

		/// <summary>
		/// Ases the specified service.
		/// </summary>
		/// <param name="service">The type of the service.</param>
		/// <returns>
		///   itself
		/// </returns>
		IDefinition As(Type service);
	}
}
