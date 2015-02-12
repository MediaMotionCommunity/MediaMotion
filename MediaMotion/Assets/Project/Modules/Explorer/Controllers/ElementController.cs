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
			this.InitTile();
			this.InitText();
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

		/// <summary>
		/// Initializes the tile.
		/// </summary>
		private void InitTile() {
			GameObject tile = GameObject.Find(this.gameObject.name + "/Content/Tile");

			tile.renderer.material.mainTexture = this.Element.GetTexture2D();
			tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
			tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);
		}

		/// <summary>
		/// Initializes the text.
		/// </summary>
		private void InitText() {
			GameObject text = GameObject.Find(this.gameObject.name + "/Content/Name");
			TextMesh tileTextMesh = text.GetComponent<TextMesh>();

			tileTextMesh.text = this.Element.GetName();
			tileTextMesh.color = new Color(0.8f, 0.9f, 1.0f);

			if (this.renderer.bounds.size.x < text.renderer.bounds.size.x) {
				tileTextMesh.text = this.Element.GetName().Substring(0, 10) + "...";
			}
		}
	}
}
