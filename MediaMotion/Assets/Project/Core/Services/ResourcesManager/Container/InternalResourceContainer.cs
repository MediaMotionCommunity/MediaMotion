using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MediaMotion.Core.Services.ResourcesManager.Container {
	/// <summary>
	/// Internal resource container
	/// </summary>
	/// <typeparam name="T">The type of the resource.</typeparam>
	internal class InternalResourceContainer<T> where T : UnityEngine.Object {
		/// <summary>
		/// The www getter
		/// </summary>
		private WWW wwwGetter;

		/// <summary>
		/// The internal resource
		/// </summary>
		private T internalResource;

		/// <summary>
		/// Resource type
		/// </summary>
		public enum ResourceType {
			Internal,
			Local,
			Distant
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InternalResourceContainer`1"/> class.
		/// </summary>
		/// <param name="name">The resource name.</param>
		public InternalResourceContainer(string name) {
			this.Name = name;
			if (this.Name.Contains("/") || this.Name.Contains("\\")) {
				if (this.Name.Contains("://")) {
					this.Type = ResourceType.Distant;
					this.Path = name;
				} else {
					switch (System.Environment.OSVersion.Platform) {
						case System.PlatformID.Win32Windows:
						case System.PlatformID.Win32S:
						case System.PlatformID.Win32NT:
							this.Path = "file:///" + this.Name;
							break;
						case System.PlatformID.MacOSX:
						default:
							this.Path = "file://" + this.Name;
							break;
					}
					this.Type = ResourceType.Local;
				}
			} else {
				this.Type = ResourceType.Internal;
				this.Path = name;
			}
		}

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		public string Name { get; private set; }

		/// <summary>
		/// Gets the path.
		/// </summary>
		/// <value>
		/// The path.
		/// </value>
		public string Path { get; private set; }

		/// <summary>
		/// Gets the type of the resource.
		/// </summary>
		/// <value>
		/// The type of the resource.
		/// </value>
		public ResourceType Type { get; private set; }

		/// <summary>
		/// Gets a value indicating whether the resource is available.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is available; otherwise, <c>false</c>.
		/// </value>
		public bool IsAvailable {
			get {
				if (this.internalResource == null) {
					switch (this.Type) {
						case ResourceType.Distant:
						case ResourceType.Local:
							this.LoadNotInternalResources();
							break;
						case ResourceType.Internal:
						default:
							this.LoadInternalResources();
							break;
					}
				}
				return (this.internalResource != null);
			}
			private set {
			}
		}

		/// <summary>
		/// Gets the resource.
		/// </summary>
		/// <value>
		/// The resource.
		/// </value>
		public T Resource {
			get {
				if (this.IsAvailable) {
					return (this.internalResource);
				}
				throw new System.Exception("Unavailable resource");
			}
			private set {
				this.internalResource = value;
			}
		}

		/// <summary>
		/// Loads the resource.
		/// </summary>
		/// <returns>
		///   <c>true</c> if the resource is available, <c>false</c> otherwise
		/// </returns>
		private void LoadInternalResources() {
			this.Resource = Resources.Load<T>(this.Path);
		}

		/// <summary>
		/// Loads the not internal resources.
		/// </summary>
		private void LoadNotInternalResources() {
			if (this.wwwGetter != null) {
				if (this.wwwGetter.isDone) {
					if (typeof(T).IsSubclassOf(typeof(Texture2D))) {
						this.Resource = this.wwwGetter.texture as T;
					}
				}
			} else {
				this.wwwGetter = new WWW(this.Path);
			}
		}
	}
}
