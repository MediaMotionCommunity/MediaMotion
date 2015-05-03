using System;
using MediaMotion.Core.Services.Resolver.Interfaces;

namespace MediaMotion.Core.Services.Resolver.Models.Interfaces {
	/// <summary>
	/// Service Definition Interface
	/// </summary>
	public interface IServiceDefinition {
		/// <summary>
		/// Gets a value indicating whether this instance is build.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is build; otherwise, <c>false</c>.
		/// </value>
		bool IsBuild { get; }

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <returns>
		///   The service
		/// </returns>
		object Get();

		/// <summary>
		/// Builds the specified resolver service.
		/// </summary>
		/// <param name="resolverService">The resolver service.</param>
		void Build(IResolverService resolverService);

		/// <summary>
		/// Clears this instance.
		/// </summary>
		void Clear();
	}
}
