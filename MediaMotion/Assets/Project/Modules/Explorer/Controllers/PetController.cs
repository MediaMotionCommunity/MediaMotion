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
		/// The animation
		/// </summary>
		private Animation animationComponent;

		/// <summary>
		/// The animation name
		/// </summary>
		private string animationName = string.Empty;

		/// <summary>
		/// The rotation
		/// </summary>
		private Vector3 rotation;

		/// <summary>
		/// The distance
		/// </summary>
		private float distance;

		/// <summary>
		/// The total time
		/// </summary>
		private float totalTime = 0.0f;

		/// <summary>
		/// The time
		/// </summary>
		private float elapsedTime = 10.0f;

		/// <summary>
		/// Starts this instance.
		/// </summary>
		public void Start() {
			this.animationComponent = this.gameObject.GetComponent<Animation>();
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		public void Update() {
			this.elapsedTime += Time.deltaTime;
			if (this.elapsedTime > this.totalTime) {
				if (this.animationName.CompareTo("dive") == 0) {
					this.gameObject.transform.Translate(new Vector3(0.0f, -2.6f, -5.8f), Space.Self);
				}
				this.gameObject.transform.localEulerAngles = new Vector3(0.0f, this.gameObject.transform.localEulerAngles.y, 0.0f);
				if (this.gameObject.transform.localPosition.y > 1.2f && Random.Range(0, 2) == 0) {
					this.animationName = "dive";
					this.rotation = new Vector3(0.0f, 0.0f, 0.0f);
					this.distance = 0.0f;
					this.totalTime = 9.0f;
				} else {
					this.animationName = "swim";
					this.rotation = new Vector3(Random.Range(this.gameObject.transform.localPosition.y < -1.5f ? 2.0f : -10.0f, this.gameObject.transform.localPosition.y > 1.5f ? -2.0f : 10.0f), Random.Range(-20.0f, 20.0f), 0.0f);
					this.distance = Random.Range(2.5f, 5.0f);
					this.totalTime = this.distance * 2.0f;
				}
				this.elapsedTime = 0.0f;
			}
			this.Play();
		}

		/// <summary>
		/// Plays this instance.
		/// </summary>
		private void Play() {
			float animationPourcentage = this.elapsedTime / this.totalTime;
			float coefficient = Time.deltaTime / this.totalTime;
			Vector3 angle = new Vector3(this.rotation.x * 2.0f * coefficient, this.rotation.y * coefficient, this.rotation.y * 0.5f * coefficient);

			if (!this.animationComponent.IsPlaying(this.animationName)) {
				this.animationComponent.Play(this.animationName);
			}
			if (animationPourcentage > 0.5f) {
				angle.x *= -1.0f;
				angle.z *= -1.0f;
			}
			this.gameObject.transform.Rotate(angle, Space.Self);
			this.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, this.distance * coefficient * -1.0f), Space.Self);
		}
	}
}
