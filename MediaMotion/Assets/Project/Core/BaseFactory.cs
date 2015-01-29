using System;

using UnityEngine;

namespace MediaMotion.Core {
	/// <summary>
	/// The base factory.
	/// </summary>
	public class BaseFactory : MonoBehaviour {
		/// <summary>
		/// The class name.
		/// </summary>
		public string ClassName;

		/// <summary>
		/// The unity object.
		/// </summary>
		private AUnityObject unityObject;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseFactory"/> class.
		/// </summary>
		public BaseFactory() {
		}

		/// <summary>
		/// The start.
		/// </summary>
		public void Start() {
			if (this.ClassName == null) {
				throw new NullReferenceException("Empty class Name attribute.");
			}
			var type = Type.GetType(this.ClassName);
			this.unityObject = MediaMotionCore.Core.Resolve(type) as AUnityObject;
			if (this.unityObject == null) {
				throw new NullReferenceException("Bad type name");
			}
			this.unityObject.Start();
		}

		/// <summary>
		/// The update.
		/// </summary>
		public void Update() {
			this.unityObject.Update();
		}
	}
}
