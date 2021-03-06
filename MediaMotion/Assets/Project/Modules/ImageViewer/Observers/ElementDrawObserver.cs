﻿using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.ImageViewer.Observers {
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
			GameObject drewElement = GameObject.Instantiate(Resources.Load("3DObject")) as GameObject;

			float scale = 1.8f;
			drewElement.transform.localScale = new Vector3(1 / 0.09f / 1.3f / scale, 1 / 0.05f / 4 / scale, 1 / 0.09f / 1.3f / scale);
			drewElement.transform.localPosition = new Vector3(0, -1, 0.9f);
			drewElement.transform.localRotation = Quaternion.Euler(270.0f, 90.0f, 270.0f);

			drewElement.GetComponent<Renderer>().material = Resources.Load<Material>("Image");
			return (drewElement);
		}
	}
}
