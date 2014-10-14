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
        Frame frame = controller.Frame();
        if (frame.Fingers.Count > 0) {
			System.Console.Write ("LeapTest");
		}
    }
}