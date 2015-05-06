using System.Collections;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ResourcesManager.Container.Interfaces;
using MediaMotion.Core.Services.ResourcesManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Element controller
	/// </summary>
	public class ElementController : BaseUnityScript<ElementController> {
		/// <summary>
		/// The texture
		/// </summary>
		private IResourceContainer<Material> texture;

		/// <summary>
		/// The content
		/// </summary>
		private GameObject content;

		/// <summary>
		/// The tile
		/// </summary>
		private GameObject tile;

		/// <summary>
		/// The text
		/// </summary>
		private GameObject text;

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
		/// <param name="resourceManagerService">The resource manager service.</param>
		public void Init(IResourceManagerService resourceManagerService) {
			TextMesh tileTextMesh;

			this.content = GameObject.Find(this.gameObject.name + "/Content");
			this.tile = GameObject.Find(this.gameObject.name + "/Content/Tile");
			this.text = GameObject.Find(this.gameObject.name + "/Content/Name");
			tileTextMesh = this.text.GetComponent<TextMesh>();

			// texture container
			this.texture = resourceManagerService.GetContainer<Material>(this.Element.GetResourceId());

			// texture
			this.tile.GetComponent<Renderer>().material = this.texture.Get();

			// text
			tileTextMesh.text = this.Element.GetName();
			tileTextMesh.color = new Color(0.8f, 0.9f, 1.0f);

			if (this.tile.GetComponent<Renderer>().bounds.size.x < this.text.GetComponent<Renderer>().bounds.size.x) {
				tileTextMesh.text = this.Element.GetName().Substring(0, 10) + "...";
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			// this.tile.renderer.material.mainTexture = this.texture.Get();
		}

		/// <summary>
		/// Selects this instance.
		/// </summary>
		public void Select() {
			iTween.MoveTo(this.content, new Vector3(this.gameObject.transform.position.x, 2.0f, this.gameObject.transform.position.z), 0.5f);
		}

		/// <summary>
		/// Deselects this instance.
		/// </summary>
		public void Deselect() {
			iTween.MoveTo(this.content, new Vector3(this.gameObject.transform.position.x, 1.0f, this.gameObject.transform.position.z), 0.5f);
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
