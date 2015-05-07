using System;

namespace MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces {
	/// <summary>
	/// The service
	/// </summary>
	public interface IActivator {
		/// <summary>
		/// Gets the type of the service.
		/// </summary>
		/// <value>
		/// The type of the service.
		/// </value>
		Type ServiceType { get; }

		/// <summary>
		/// Builds this instance.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the build was correctly performed, <c>false</c> otherwise
		/// </returns>
		bool Build();

		/// <summary>
		/// Gets the service
		/// </summary>
		/// <returns>
		///   The service
		/// </returns>
		object Get();
	}
}
