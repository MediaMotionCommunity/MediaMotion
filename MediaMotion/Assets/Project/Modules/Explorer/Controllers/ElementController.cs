using System.Collections;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// 
	/// </summary>
	public class ElementController : BaseUnityScript<ElementController> {
		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		public IElement Element { get; private set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init() {
			GameObject text;
			TextMesh tileTextMesh;

			text = new GameObject();
			text.name = "filename";
			text.transform.parent = this.transform;
			text.transform.localPosition = new Vector3(4.5f, 0.0f, 5.5f);
			text.transform.localEulerAngles = new Vector3(90.0f, 180.0f, 0.0f);
			text.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

			tileTextMesh = text.AddComponent(typeof(TextMesh)) as TextMesh;
			tileTextMesh.transform.parent = text.transform;
			tileTextMesh.font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
			tileTextMesh.fontSize = 16;
			tileTextMesh.renderer.material = tileTextMesh.font.material;
			tileTextMesh.text = this.Element.GetName();

			Hashtable color = new Hashtable();
			color.Add("r", 0.8f);
			color.Add("g", 0.9f);
			color.Add("b", 1.0f);
			color.Add("time", 0.1f);
			iTween.ColorTo(text, color);

			color = new Hashtable();
			if (this.renderer.bounds.size.x < text.renderer.bounds.size.x) {
				tileTextMesh.text = this.Element.GetName().Substring(0, 10) + "...";
			}
		}

		/// <summary>
		/// Sets the element.
		/// </summary>
		/// <param name="element">The element.</param>
		public void SetElement(IElement element) {
			if (this.Element == null) {
				this.Element = element;
			}
		}
	}
}
