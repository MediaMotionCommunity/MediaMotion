using System;
using UnityEngine;
using System.Collections;
using Leap;

public class CompileTest : MonoBehaviour
{
    Controller controller;

    void Start()
    {
		Debug.Log ("Begin");
        controller = new Controller();
    }

    void Update()
    {
		if (controller == null) {
						Debug.Log ("controller = null");
						return;
				}
        Frame frame = controller.Frame();
		if (frame == null)
						Debug.Log ("Frame = null");
		else if (frame.Fingers.Count > 0) {
			Debug.Log ("LeapTest");
		}
    }
}