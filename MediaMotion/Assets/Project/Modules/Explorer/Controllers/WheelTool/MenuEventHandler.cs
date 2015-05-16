using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Core.Services.FileSystem.Models.Enums;
using UnityEngine;

/// <summary>
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuEventHandler : MonoBehaviour {

	/// <summary>
	/// Receives the menu event.
	/// </summary>
	/// <param name="action">The action.</param>
	/// <param name="selectedElement">The selected element.</param>
	public void ReceiveMenuEvent(MenuBehavior.ButtonAction action, IElement selectedElement) {
		Debug.Log("Events: " + action.ToString() + " " + selectedElement.GetName());
		if (action.Equals(MenuBehavior.ButtonAction.OPEN) && selectedElement.GetElementType().Equals(ElementType.File)) {
			System.Diagnostics.Process.Start(selectedElement.GetPath());
		}

	}

	/// <summary>
	/// Starts this instance.
	/// </summary>
	private void Start() {
	}
}
