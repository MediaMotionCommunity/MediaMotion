using System.Collections;
using UnityEngine;

namespace MediaMotion.Modules.Explorer.Controllers {
	/// <summary>
	/// Whale Controller
	/// </summary>
	public class PetController : MonoBehaviour {
		/// <summary>
		/// The distance to rotate
		/// </summary>
		private const float DistanceToRotate = 2.5f;

		/// <summary>
		/// The speed
		/// </summary>
		private const float Speed = 2.0f;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.RandomDeplacement();
		}

		/// <summary>
		/// Randoms the deplacement.
		/// </summary>
		public void RandomDeplacement() {
			switch (Random.Range(1, 3)) {
				case 1:
					this.Deplacement(Random.Range(2.0f, 7.0f));
					break;
				case 2:
					this.Rotation(Random.Range(-40.0f, 40.0f), 5.0f);
					break;
			}
		}

		/// <summary>
		/// Rotations the specified angle.
		/// </summary>
		/// <param name="angle">The angle.</param>
		/// <param name="time">The time.</param>
		private void Rotation(float angle, float time) {
			Vector3 eulerAngles = this.gameObject.transform.eulerAngles + new Vector3(0.0f, angle, 0.0f);
			Hashtable moveParams = new Hashtable();
			Hashtable rotationParams = new Hashtable();

			moveParams.Add("easetype", iTween.EaseType.linear);
			moveParams.Add("position", this.gameObject.transform.position + (Quaternion.Euler(eulerAngles) * new Vector3(0.0f, 0.0f, -DistanceToRotate)));
			moveParams.Add("time", DistanceToRotate * Speed);
			iTween.MoveTo(this.gameObject, moveParams);

			rotationParams.Add("oncomplete", "RandomDeplacement");
			rotationParams.Add("rotation", eulerAngles);
			rotationParams.Add("easetype", iTween.EaseType.linear);
			rotationParams.Add("time", DistanceToRotate * Speed);
			iTween.RotateTo(this.gameObject, rotationParams);
		}

		/// <summary>
		/// Deplacements the specified direction.
		/// </summary>
		/// <param name="direction">The direction.</param>
		/// <param name="time">The time.</param>
		private void Deplacement(float distance) {
			Hashtable animationParam = new Hashtable();

			animationParam.Add("oncomplete", "RandomDeplacement");
			animationParam.Add("easetype", iTween.EaseType.linear);
			animationParam.Add("position", this.gameObject.transform.position + (Quaternion.Euler(this.gameObject.transform.eulerAngles) * new Vector3(0.0f, 0.0f, -distance)));
			animationParam.Add("time", distance * Speed);
			iTween.MoveTo(this.gameObject, animationParam);
		}
	}
}
