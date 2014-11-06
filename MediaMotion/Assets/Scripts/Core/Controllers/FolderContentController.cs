using UnityEngine;
using System.Collections.Generic;

namespace MediaMotion.Core.Controllers {
	public class FolderContentController : MonoBehaviour {

		private int rowSize = 5;

		/* Coordinates of the first placed tile */
		private double originX = -2;
		private double originZ = -1.5;

		/* Value to increment to get the next IElement or next row */
		private double incrementX = 1.5;
		private double incrementZ = 1.5;

		private List<GameObject> tiles;

		void Start() {

		}

		void Update() {

		}

		private void moveCursor() {
			//incrémenter le y de 0.5 ?
		}

		private void displayContent() {
			//On obtient la liste des fichiers, paramètres ou appel de fonction ???
			//int i = 0;
			//double x = originX;
			//double z = originZ;
			//while (files[i]) {
			//	GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Plane);
			//	tile.transform.position.Set(x, 1, z);
			//	tile.transform.rotation.Set(-40, 0, 0, -1);
			//	//attribuer texture en fonction du fichier ?
			//	//tile.renderer.material.mainTexture = Resources.Load<Texture2D>(NOM_CORRESPONDANT);
			//	//tile.AddComponent(COMPONENT_POUR_INFOS_FICHIER);
			//	//tile.name = NOM_DU_FICHIER;
			//	tiles.Add(tile);
			//	++i;
			//	if (i % rowSize == 0)
			//		z += incrementZ;
			//}
		}
	}
}