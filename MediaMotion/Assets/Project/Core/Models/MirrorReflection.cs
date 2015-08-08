using System.Collections;
using UnityEngine;

namespace MediaMotion.Core.Models {
	/// <summary>
	/// Mirror reflection (http://wiki.unity3d.com/index.php/MirrorReflection4)
	/// </summary>
	[ExecuteInEditMode]
	public class MirrorReflection : MonoBehaviour {
		/// <summary>
		/// The m_ disable pixel lights
		/// </summary>
		public bool DisablePixelLights = true;

		/// <summary>
		/// The m_ texture size
		/// </summary>
		public int TextureSize = 256;

		/// <summary>
		/// The m_ clip plane offset
		/// </summary>
		public float ClipPlaneOffset = 0.07f;

		/// <summary>
		/// The m_ reflect layers
		/// </summary>
		public LayerMask ReflectLayers = -1;

		/// <summary>
		/// The s_ inside rendering
		/// </summary>
		private static bool InsideRendering = false;

		/// <summary>
		/// The m_ reflection cameras
		/// </summary>
		private Hashtable ReflectionCameras = new Hashtable();

		/// <summary>
		/// The m_ reflection texture
		/// </summary>
		private RenderTexture ReflectionTexture = null;

		/// <summary>
		/// The m_ old reflection texture size
		/// </summary>
		private int OldReflectionTextureSize = 0;

		/// <summary>
		/// Called when [will render object].
		/// </summary>
		public void OnWillRenderObject() {
			var rend = GetComponent<Renderer>();
			if (!this.enabled || !rend || !rend.sharedMaterial || !rend.enabled) {
				return;
			}

			Camera cam = Camera.current;
			if (!cam) {
				return;
			}

			// Safeguard from recursive reflections.
			if (InsideRendering) {
				return;
			}
			InsideRendering = true;

			Camera reflectionCamera;
			this.CreateMirrorObjects(cam, out reflectionCamera);

			// find out the reflection plane: position and normal in world space
			Vector3 pos = transform.position;
			Vector3 normal = transform.up;

			// Optionally disable pixel lights for reflection
			int oldPixelLightCount = QualitySettings.pixelLightCount;
			if (this.DisablePixelLights) {
				QualitySettings.pixelLightCount = 0;
			}

			this.UpdateCameraModes(cam, reflectionCamera);

			// Render reflection
			// Reflect camera around reflection plane
			float d = -Vector3.Dot(normal, pos) - this.ClipPlaneOffset;
			Vector4 reflectionPlane = new Vector4(normal.x, normal.y, normal.z, d);

			Matrix4x4 reflection = Matrix4x4.zero;
			CalculateReflectionMatrix(ref reflection, reflectionPlane);
			Vector3 oldpos = cam.transform.position;
			Vector3 newpos = reflection.MultiplyPoint(oldpos);
			reflectionCamera.worldToCameraMatrix = cam.worldToCameraMatrix * reflection;

			// Setup oblique projection matrix so that near plane is our reflection
			// plane. This way we clip everything below/above it for free.
			Vector4 clipPlane = this.CameraSpacePlane(reflectionCamera, pos, normal, 1.0f);

			// Matrix4x4 projection = cam.projectionMatrix;
			Matrix4x4 projection = cam.CalculateObliqueMatrix(clipPlane);
			reflectionCamera.projectionMatrix = projection;

			// never render water layer
			reflectionCamera.cullingMask = ~(1 << 4) & this.ReflectLayers.value;
			reflectionCamera.targetTexture = this.ReflectionTexture;

			// GL.SetRevertBackfacing(true);
			GL.invertCulling = true;
			reflectionCamera.transform.position = newpos;
			Vector3 euler = cam.transform.eulerAngles;
			reflectionCamera.transform.eulerAngles = new Vector3(0, euler.y, euler.z);
			reflectionCamera.Render();
			reflectionCamera.transform.position = oldpos;

			// GL.SetRevertBackfacing(false);
			GL.invertCulling = false;
			Material[] materials = rend.sharedMaterials;
			foreach (Material mat in materials) {
				if (mat.HasProperty("_ReflectionTex")) {
					mat.SetTexture("_ReflectionTex", this.ReflectionTexture);
				}
			}

			// Restore pixel light count
			if (this.DisablePixelLights) {
				QualitySettings.pixelLightCount = oldPixelLightCount;
			}

			InsideRendering = false;
		}

		/// <summary>
		/// Called when [disable].
		/// </summary>
		public void OnDisable() {
			if (this.ReflectionTexture) {
				RenderTexture.DestroyImmediate(this.ReflectionTexture);
				this.ReflectionTexture = null;
			}
			foreach (DictionaryEntry kvp in this.ReflectionCameras) {
				GameObject.DestroyImmediate(((Camera)kvp.Value).gameObject);
			}
			this.ReflectionCameras.Clear();
		}

		/// <summary>
		/// SGNs the specified a.
		/// </summary>
		/// <param name="a">a.</param>
		/// <returns>The sign of the float</returns>
		private static float Sgn(float a) {
			if (a > 0.0f) {
				return (1.0f);
			}
			if (a < 0.0f) {
				return (-1.0f);
			}
			return (0.0f);
		}

		/// <summary>
		/// Calculates the reflection matrix.
		/// </summary>
		/// <param name="reflectionMat">The reflection mat.</param>
		/// <param name="plane">The plane.</param>
		private static void CalculateReflectionMatrix(ref Matrix4x4 reflectionMat, Vector4 plane) {
			reflectionMat.m00 = (1F - (2F * plane[0] * plane[0]));
			reflectionMat.m01 = (-2F * plane[0] * plane[1]);
			reflectionMat.m02 = (-2F * plane[0] * plane[2]);
			reflectionMat.m03 = (-2F * plane[3] * plane[0]);

			reflectionMat.m10 = (-2F * plane[1] * plane[0]);
			reflectionMat.m11 = (1F - (2F * plane[1] * plane[1]));
			reflectionMat.m12 = (-2F * plane[1] * plane[2]);
			reflectionMat.m13 = (-2F * plane[3] * plane[1]);

			reflectionMat.m20 = (-2F * plane[2] * plane[0]);
			reflectionMat.m21 = (-2F * plane[2] * plane[1]);
			reflectionMat.m22 = (1F - (2F * plane[2] * plane[2]));
			reflectionMat.m23 = (-2F * plane[3] * plane[2]);

			reflectionMat.m30 = 0F;
			reflectionMat.m31 = 0F;
			reflectionMat.m32 = 0F;
			reflectionMat.m33 = 1F;
		}

		/// <summary>
		/// Updates the camera modes.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="destination">The destination.</param>
		private void UpdateCameraModes(Camera source, Camera destination) {
			if (destination == null) {
				return;
			}

			// set camera to clear the same way as current camera
			destination.clearFlags = source.clearFlags;
			destination.backgroundColor = source.backgroundColor;
			if (source.clearFlags == CameraClearFlags.Skybox) {
				Skybox sky = source.GetComponent(typeof(Skybox)) as Skybox;
				Skybox mysky = destination.GetComponent(typeof(Skybox)) as Skybox;
				if (!sky || !sky.material) {
					mysky.enabled = false;
				} else {
					mysky.enabled = true;
					mysky.material = sky.material;
				}
			}

			// update other values to match current camera.
			// even if we are supplying custom camera&projection matrices,
			// some of values are used elsewhere (e.g. skybox uses far plane)
			destination.farClipPlane = source.farClipPlane;
			destination.nearClipPlane = source.nearClipPlane;
			destination.orthographic = source.orthographic;
			destination.fieldOfView = source.fieldOfView;
			destination.aspect = source.aspect;
			destination.orthographicSize = source.orthographicSize;
		}

		/// <summary>
		/// Creates the mirror objects.
		/// </summary>
		/// <param name="currentCamera">The current camera.</param>
		/// <param name="reflectionCamera">The reflection camera.</param>
		private void CreateMirrorObjects(Camera currentCamera, out Camera reflectionCamera) {
			reflectionCamera = null;

			// Reflection render texture
			if (!this.ReflectionTexture || this.OldReflectionTextureSize != this.TextureSize) {
				if (this.ReflectionTexture) {
					RenderTexture.DestroyImmediate(this.ReflectionTexture);
				}
				this.ReflectionTexture = new RenderTexture(this.TextureSize, this.TextureSize, 16);
				this.ReflectionTexture.name = "__MirrorReflection" + this.GetInstanceID();
				this.ReflectionTexture.isPowerOfTwo = true;
				this.ReflectionTexture.hideFlags = HideFlags.DontSave;
				this.OldReflectionTextureSize = this.TextureSize;
			}

			// Camera for reflection
			reflectionCamera = this.ReflectionCameras[currentCamera] as Camera;

			// catch both not-in-dictionary and in-dictionary-but-deleted-GO
			if (!reflectionCamera) {
				GameObject go = new GameObject("Mirror Refl Camera id" + GetInstanceID() + " for " + currentCamera.GetInstanceID(), typeof(Camera), typeof(Skybox));
				reflectionCamera = go.GetComponent<Camera>();
				reflectionCamera.enabled = false;
				reflectionCamera.transform.position = transform.position;
				reflectionCamera.transform.rotation = transform.rotation;
				reflectionCamera.gameObject.AddComponent<FlareLayer>();
				go.hideFlags = HideFlags.HideAndDontSave;
				this.ReflectionCameras[currentCamera] = reflectionCamera;
			}
		}

		/// <summary>
		/// Cameras the space plane.
		/// </summary>
		/// <param name="cam">The cam.</param>
		/// <param name="pos">The position.</param>
		/// <param name="normal">The normal.</param>
		/// <param name="sideSign">The side sign.</param>
		/// <returns>Camera space plane</returns>
		private Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal, float sideSign) {
			Vector3 offsetPos = pos + (normal * this.ClipPlaneOffset);
			Matrix4x4 m = cam.worldToCameraMatrix;
			Vector3 cpos = m.MultiplyPoint(offsetPos);
			Vector3 cnormal = m.MultiplyVector(normal).normalized * sideSign;

			return (new Vector4(cnormal.x, cnormal.y, cnormal.z, -Vector3.Dot(cpos, cnormal)));
		}
	}
}