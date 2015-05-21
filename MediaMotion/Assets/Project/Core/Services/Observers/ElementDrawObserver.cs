using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Services.Observers {
	/// <summary>
	/// Element Draw Observer
	/// </summary>
	public class ElementDrawObserver : IElementDrawObserver {
		/// <summary>
		/// Draws this instance.
		/// </summary>
		/// <param name="element">The element.</param>
		/// <returns>
		/// The gameObject drew
		/// </returns>
		public GameObject Draw(IElement element) {
			GameObject drewElement = GameObject.CreatePrimitive(PrimitiveType.Plane);

			drewElement.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			drewElement.transform.rotation = Quaternion.Euler(90.0f, 180.0f, 0.0f);
			drewElement.GetComponent<Renderer>().material = Resources.Load<Material>("File");
			return (drewElement);
		}
	}
}
