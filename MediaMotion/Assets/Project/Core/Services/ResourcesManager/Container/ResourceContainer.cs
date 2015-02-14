using System.Collections.Generic;
using System.Linq;
using MediaMotion.Core.Services.ResourcesManager.Container.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.ResourcesManager.Container {
	/// <summary>
	/// Resources container
	/// </summary>
	/// <typeparam name="T">The type of the resource</typeparam>
	public class ResourceContainer<T> : IResourceContainer<T> where T : UnityEngine.Object {
		/// <summary>
		/// The resources
		/// </summary>
		private Dictionary<string, InternalResourceContainer<T>> resources;

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourcesContainer{T}"/> class.
		/// </summary>
		/// <param name="resources">The resources.</param>
		public ResourceContainer(string[] names) {
			this.resources = new Dictionary<string, InternalResourceContainer<T>>();
			foreach (string name in names) {
				this.resources.Add(name, new InternalResourceContainer<T>(name));
			}
		}

		/// <summary>
		/// Gets this instance.
		/// </summary>
		/// <param name="name">The resource name.</param>
		/// <returns>
		/// The resource
		/// </returns>
		public T Get(string name = null) {
			KeyValuePair<string, InternalResourceContainer<T>> value;

			if (name != null && this.resources.Any(r => r.Value.IsAvailable && r.Value.Name == name)) {
				// specific resource if requested
				value = this.resources.First(r => r.Value.IsAvailable && r.Value.Name == name);
			} else if (this.resources.Any(r => r.Value.IsAvailable && r.Value.Type != InternalResourceContainer<T>.ResourceType.Internal)) {
				// first resource which is not internal
				value = this.resources.First(r => r.Value.IsAvailable && r.Value.Type != InternalResourceContainer<T>.ResourceType.Internal);
			} else {
				// default internal resource
				value = this.resources.First(r => r.Value.IsAvailable && r.Value.Type == InternalResourceContainer<T>.ResourceType.Internal);
			}
			return (value.Value.Resource);
		}
	}
}
