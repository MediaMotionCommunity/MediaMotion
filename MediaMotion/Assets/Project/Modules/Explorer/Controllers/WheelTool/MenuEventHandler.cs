﻿using MediaMotion.Core.Services.FileSystem.Models.Enums;
using MediaMotion.Core.Services.FileSystem.Models.Interfaces;
using MediaMotion.Modules.Explorer.Controllers;
using UnityEngine;

/// <summary>
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuEventHandler : MonoBehaviour {
	/// <summary>
	/// Explorer controller
	/// </summary>
	private ExplorerController explorer;

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
		if (this.explorer.PopupVisibility == false) {
			this.explorer.PopupVisibility = true;
		}
	}

	/// <summary>
	/// Starts this instance.
	/// </summary>
	private void Start() {
		this.explorer = GameObject.Find("Explorer").GetComponent<ExplorerController>();
	}
}
