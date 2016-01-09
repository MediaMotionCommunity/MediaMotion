using System;
using System.Collections.Generic;
using MediaMotion.Core.Services.ContainerBuilder.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Models;
using MediaMotion.Core.Services.ContainerBuilder.Models.Interfaces;
using MediaMotion.Core.Services.ContainerBuilder.Resolver;
using MediaMotion.Core.Services.ContainerBuilder.Resolver.Interfaces;

namespace MediaMotion.Core.Services.ContainerBuilder {
	/// <summary>
	/// Service Container builder
	/// </summary>
	public class ContainerBuilderService : IContainerBuilderService {
		/// <summary>
		/// The parameters list
		/// </summary>
		private readonly Dictionary<string, object> parametersList;

		/// <summary>
		/// The registration list
		/// </summary>
		private readonly List<IDefinition> definitionList;

		/// <summary>
		/// Initializes a new instance of the <see cref="ContainerBuilderService"/> class.
		/// </summary>
		public ContainerBuilderService() {
			this.parametersList = new Dictionary<string, object>();
			this.definitionList = new List<IDefinition>();
		}

		/// <summary>
		/// Builds the specified parent.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <returns>
		///   The container
		/// </returns>
		public IContainer Build(IContainer parent = null) {
			Dictionary<string, object> parameters = new Dictionary<string, object>();
			Dictionary<Type, IActivator> services = new Dictionary<Type, IActivator>();
			IContainer container = new Container(parent, parameters, services);
			IResolverService resolver = new ResolverService(container);
			IActivator resolverActivator = new SingleInstanceActivator(resolver, typeof(ResolverService), resolver);

			foreach (KeyValuePair<string, object> parameter in this.parametersList) {
				parameters[parameter.Key] = parameter.Value;
			}
			foreach (IDefinition definition in this.definitionList) {
				IActivator activator = this.GetActivator(resolver, definition);

				foreach (Type alias in definition.Types) {
					if (!alias.IsAssignableFrom(definition.ServiceType)) {
						throw new Exception("Error");
					}
					services[alias] = activator;
				}
			}
			services[typeof(ResolverService)] = resolverActivator;
			services[typeof(IResolverService)] = resolverActivator;

			foreach (IActivator activator in services.Values) {
				activator.Build();
			}
			return (container);
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear() {
			this.definitionList.Clear();
		}

		/// <summary>
		/// Defines the specified parameter.
		/// </summary>
		/// <param name="parameter">The parameter.</param>
		/// <param name="value">The value.</param>
		public void Define(string parameter, object value) {
			this.parametersList[parameter] = value;
		}

		/// <summary>
		/// Registers the specified instance.
		/// </summary>
		/// <typeparam name="Service">The type of the service.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <returns>
		/// The service definition
		/// </returns>
		public IDefinition Register<Service>(Service instance = null) where Service : class {
			return (this.Register(typeof(Service), instance));
		}

		/// <summary>
		/// Registers the specified service.
		/// </summary>
		/// <param name="service">The type of the service.</param>
		/// <param name="instance">The instance.</param>
		/// <returns>
		/// The service definition
		/// </returns>
		public IDefinition Register(Type service, object instance = null) {
			IDefinition definition = new Definition(service, instance);

			this.definitionList.Add(definition);
			return (definition);
		}

		/// <summary>
		/// Gets the activator.
		/// </summary>
		/// <param name="resolver">The resolver.</param>
		/// <param name="definition">The definition.</param>
		/// <returns>
		///   The activator
		/// </returns>
		private IActivator GetActivator(IResolverService resolver, IDefinition definition) {
			if (definition.SingleInstance) {
				return (new SingleInstanceActivator(resolver, definition.ServiceType, definition.Instance));
			}
			return (new DefaultActivator(resolver, definition.ServiceType));
		}
	}
}
