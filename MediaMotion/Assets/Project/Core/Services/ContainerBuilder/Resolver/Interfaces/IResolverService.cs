using System.Reflection;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces {
	/// <summary>
	/// Resolver Service Interface
	/// </summary>
	public interface IResolverService {
		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		///   The container.
		/// </value>
		IContainer Container { get; }

		/// <summary>
		/// Determines whether the specified parameter is resolvable.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>
		///   <c>true</c> if the parameter can be resolved, <c>false</c> otherwise
		/// </returns>
		bool IsResolvable(ParameterInfo parameter);

		/// <summary>
		/// Resolves the parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="userParameters">The user parameters.</param>
		/// <returns>
		/// Resolved parameters
		/// </returns>
		object[] ResolveParameters(ParameterInfo[] parameters, params object[] userParameters);
	}
}
