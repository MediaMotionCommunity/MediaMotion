using UnityEngine;

namespace MediaMotion.Core {
	/// <summary>
	/// The a unity object.
	/// </summary>
	public abstract class AUnityObject {
		/// <summary>
		/// The start.
		/// </summary>
		public abstract void Start();

		/// <summary>
		/// The update.
		/// </summary>
		public abstract void Update();

		protected void Destroy(Object obj) {
			Object.Destroy(obj, 0.0f);
		}
	}
}
