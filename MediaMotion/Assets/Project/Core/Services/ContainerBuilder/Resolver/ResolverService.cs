using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Attributes;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder.Resolver {
	/// <summary>
	/// Resolver service
	/// </summary>
	public class ResolverService : IResolverService {
		/// <summary>
		/// Initializes a new instance of the <see cref="ResolverService"/> class.
		/// </summary>
		/// <param name="container">The container.</param>
		public ResolverService(IContainer container) {
			this.Container = container;
		}

		/// <summary>
		/// Gets the container.
		/// </summary>
		/// <value>
		///   The container.
		/// </value>
		public IContainer Container { get; private set; }

		/// <summary>
		/// Determines whether the specified parameter is resolvable.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <returns>
		///   <c>true</c> if the parameter can be resolved, <c>false</c> otherwise
		/// </returns>
		public bool IsResolvable(ParameterInfo parameter) {
			object[] attributes = parameter.GetCustomAttributes(typeof(Parameter), false);

			if (attributes.Length > 0) {
				foreach (object attribute in attributes) {
					if (this.Container.HasParameter(((Parameter)attribute).key)) {
						return (true);
					}
				}
				return (false);
			}
			return (this.Container.Has(parameter.ParameterType));
		}

		/// <summary>
		/// Resolves the parameters.
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <param name="userParameters">The user parameters.</param>
		/// <returns>
		///   resolved parameters
		/// </returns>
		public object[] ResolveParameters(ParameterInfo[] parameters, params object[] userParameters) {
			int userParametersOffset = 0;
			object[] resolvedParameters = new object[parameters.Length];

			for (int i = 0; i < parameters.Length; ++i) {
				resolvedParameters[i] = this.ResolveParameter(parameters[i], ref userParametersOffset, userParameters);
			}
			if (userParametersOffset != userParameters.Length) {
				throw new ArgumentException("Too much parameters");
			}
			return (resolvedParameters);
		}

		/// <summary>
		/// Resolves the parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="userParametersOffset">The user parameters offset.</param>
		/// <param name="userParameters">The user parameters.</param>
		/// <returns>
		///   The value of the parameter
		/// </returns>
		private object ResolveParameter(ParameterInfo parameter, ref int userParametersOffset, object[] userParameters) {
			object[] attributes = parameter.GetCustomAttributes(typeof(Parameter), false);

			if (attributes.Length > 0) {
				foreach (object attribute in attributes) {
					string key = ((Parameter)attribute).key;

					if (this.Container.HasParameter(key)) {
						return (this.Container.GetParameter(key));
					}
				}
				throw new Exception("Parameter not found");
			}
			if (this.Container.Has(parameter.ParameterType)) {
				return (this.Container.Get(parameter.ParameterType));
			}
			if (userParameters.Length > userParametersOffset) {
				if (!userParameters[userParametersOffset].GetType().IsAssignableFrom(parameter.ParameterType)) {
					throw new ArgumentException("Incompatible parameter type", parameter.Name);
				}
				return (userParameters[(++userParametersOffset) - 1]);
			}
			if (parameter.IsOptional) {
				return (parameter.DefaultValue);
			}
			throw new Exception("Cannot find an acceptable value for a required parameter");
		}
	}
}
