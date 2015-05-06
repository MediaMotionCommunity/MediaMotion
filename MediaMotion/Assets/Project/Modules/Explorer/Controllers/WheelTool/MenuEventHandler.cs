using System.Collections;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using UnityEngine;

/// <summary>
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuEventHandler : MonoBehaviour {
	/// <summary>
	/// The i
	/// </summary>
	private int i = 0;

	/// <summary>
	/// Receives the menu event.
	/// </summary>
	/// <param name="action">The action.</param>
	/// <param name="selectedElement">The selected element.</param>
	public void ReceiveMenuEvent(MenuBehavior.ButtonAction action, IElement selectedElement) {
		++this.i;
		Debug.Log("Events:\n" + this.i + ": " + action.ToString() + " " + selectedElement.GetName());
	}

	/// <summary>
	/// Starts this instance.
	/// </summary>
	private void Start() {
	}
}
