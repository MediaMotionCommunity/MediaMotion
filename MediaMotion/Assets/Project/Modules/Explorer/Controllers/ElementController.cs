using System.Collections;
using MediaMotion.Core.Models.Abstracts;
using MediaMotion.Core.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.ModuleManager.Interfaces;
using MediaMotion.Core.Services.Observers.Interfaces;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Element controller
	/// </summary>
	public class ElementController : AScript<ExplorerModule, ElementController> {
		/// <summary>
		/// The content
		/// </summary>
		private GameObject content;

		/// <summary>
		/// Gets the element.
		/// </summary>
		/// <value>
		/// The element.
		/// </value>
		public IElement Element { get; set; }

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		/// <param name="resourceManagerService">The resource manager service.</param>
		public void Init(IModuleManagerService moduleManagerService) {
			IModule module = moduleManagerService.Supports(this.Element);
			GameObject tile = GameObject.Find(this.gameObject.name + "/Content/TilePlaceholder/Tile");
			GameObject text = GameObject.Find(this.gameObject.name + "/Content/Name");
			TextMesh tileTextMesh = text.GetComponent<TextMesh>();

			this.content = GameObject.Find(this.gameObject.name + "/Content");
			if (module != null && module.Container.Has<IElementDrawObserver>()) {
				GameObject tmpTile = module.Container.Get<IElementDrawObserver>().Draw(this.Element);

				if (tmpTile != null) {
					Vector3 scale = tmpTile.transform.localScale;
					Vector3 position = tmpTile.transform.position;
					Quaternion rotation = tmpTile.transform.rotation;

					tmpTile.name = tile.name;
					tmpTile.transform.parent = tile.transform.parent;
					tmpTile.transform.localScale = scale;
					tmpTile.transform.localPosition = position;
					tmpTile.transform.localRotation = rotation;

					GameObject.Destroy(tile);
					tile = tmpTile;
				}
			}

			// Text
			tileTextMesh.text = this.Element.GetName();
			tileTextMesh.color = new Color(0.8f, 0.9f, 1.0f);
			if (tile.GetComponent<Renderer>().bounds.size.x < text.GetComponent<Renderer>().bounds.size.x) {
				tileTextMesh.text = this.Element.GetName().Substring(0, 10) + "...";
			}
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
	}
}
