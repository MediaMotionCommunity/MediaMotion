﻿using System.Collections;
using MediaMotion.Core.Models.FileManager.Interfaces;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.ResourcesManager.Container.Interfaces;
using MediaMotion.Core.Services.ResourcesManager.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Element controller
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
		/// The texture
		/// </summary>
		private IResourceContainer<Texture2D> texture;

		/// <summary>
		/// The tile
		/// </summary>
		private GameObject tile;

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		public void Init(IResourceManagerService resourceManagerService) {
			GameObject text;
			TextMesh tileTextMesh;

			this.tile = GameObject.Find(this.gameObject.name + "/Content/Tile");
			text = GameObject.Find(this.gameObject.name + "/Content/Name");
			tileTextMesh = text.GetComponent<TextMesh>();

			// texture container
			this.texture = resourceManagerService.GetContainer<Texture2D>(this.Element.GetResourceId());

			// texture
			this.tile.renderer.material.mainTexture = this.texture.Get();
			this.tile.renderer.material.shader = Shader.Find("Transparent/Diffuse");
			this.tile.renderer.material.color = new Color(0.3f, 0.6f, 0.9f, 1);

			// text
			tileTextMesh.text = this.Element.GetName();
			tileTextMesh.color = new Color(0.8f, 0.9f, 1.0f);

			if (this.tile.renderer.bounds.size.x < text.renderer.bounds.size.x) {
				tileTextMesh.text = this.Element.GetName().Substring(0, 10) + "...";
			}
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			//this.tile.renderer.material.mainTexture = this.texture.Get();
		}

		/// <summary>
		/// Selects this instance.
		/// </summary>
		public void Select() {
			iTween.MoveTo(this.gameObject, new Vector3(this.gameObject.transform.position.x, 3.0f, this.gameObject.transform.position.z), 0.5f);
		}

		/// <summary>
		/// Deselects this instance.
		/// </summary>
		public void Deselect() {
			iTween.MoveTo(this.gameObject, new Vector3(this.gameObject.transform.position.x, 2.0f, this.gameObject.transform.position.z), 0.5f);
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
