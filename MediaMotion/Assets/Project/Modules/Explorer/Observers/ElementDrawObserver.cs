using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Observers {
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
			GameObject drewElement = GameObject.Instantiate(Resources.Load("Video3D")) as GameObject;

//			Debug.Log(drewElement);

			drewElement.transform.localScale = new Vector3(2.0f, 6.0f, 6.0f);
			drewElement.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			drewElement.transform.localRotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
			drewElement.GetComponent<Renderer>().material = Resources.Load<Material>("Video");
			return (drewElement);
		}
	}
}
