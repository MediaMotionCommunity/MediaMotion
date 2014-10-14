using System;
using UnityEngine;
using System.Collections;
using Leap;

public class CompileTest : MonoBehaviour
{
    Controller controller;

    void Start()
    {
        controller = new Controller();
    }

    void Update()
    {
		if (controller == null) {
						System.Console.Write ("controller = null");
						return;
				}
        Frame frame = controller.Frame();
		if (frame == null)
						System.Console.Write ("Frame = null");
		else if (frame.Fingers.Count > 0) {
			System.Console.Write ("LeapTest");
		}
    }
}