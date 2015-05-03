using MediaMotion.Core.Models.Abstracts;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Pet Manager Controller
	/// </summary>
	public class PetManagerController : BaseUnityScript<PetManagerController> {
		/// <summary>
		/// The maximum pets
		/// </summary>
		public int MaxPets;

		/// <summary>
		/// The minimum pets
		/// </summary>
		public int MinPets;

		/// <summary>
		/// The pets
		/// </summary>
		public GameObject[] PetsModels;

		/// <summary>
		/// Draw all the pets
		/// </summary>
		public void Init() {
			int max = Random.Range(this.MinPets / this.PetsModels.Length, this.MaxPets / this.PetsModels.Length);
			int count = 0;

			foreach (GameObject petModel in this.PetsModels) {
				for (int i = 0; i < max; ++i) {
					GameObject pet = GameObject.Instantiate(petModel);

					pet.name = "pet_" + ++count;
					pet.transform.parent = this.gameObject.transform;
					pet.transform.localPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-1.5f, 1.5f), Random.Range(-5.0f, 40.0f));
					pet.transform.eulerAngles = new Vector3(0.0f, Random.Range(-180.0f, 180.0f), 0.0f);
					pet.AddComponent<PetController>();
				}
			}
		}
	}
}
