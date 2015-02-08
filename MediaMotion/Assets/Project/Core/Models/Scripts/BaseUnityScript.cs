using System;
using System.Collections.Generic;
using System.Reflection;
using MediaMotion.Core.Models.Module.Interfaces;
using UnityEngine;

namespace MediaMotion.Core.Models.Scripts {
	/// <summary>
	/// Base Unity Script
	/// </summary>
	/// <typeparam name="Script">Daughter Class Type (Curiously Recurring Template Pattern)</typeparam>
	public abstract class BaseUnityScript<Script> : MonoBehaviour
		where Script : BaseUnityScript<Script> {
		/// <summary>
		/// The method name
		/// </summary>
		public const string MethodName = "Init";

		/// <summary>
		/// Initialize the child instance
		/// </summary>
		public void Start() {
			MethodInfo method = typeof(Script).GetMethod(MethodName);

			if (method != null && method.IsPublic) {
				ParameterInfo[] parametersInfo = method.GetParameters();
				object[] parameters = new object[parametersInfo.Length];

				for (int i = 0; i < parametersInfo.Length; ++i) {
					parameters[i] = MediaMotionCore.Core.GetServicesContainer().Get(parametersInfo[i].ParameterType);
				}
				method.Invoke(this as Script, parameters);
			}
		}
	}
}
