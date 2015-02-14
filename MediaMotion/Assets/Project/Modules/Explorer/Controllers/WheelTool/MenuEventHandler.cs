using System.Collections;
using UnityEngine;

/// <summary>
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuEventHandler : MonoBehaviour
{
    /// <summary>
    /// The event text
    /// </summary>
    private TextMesh eventText;

    /// <summary>
    /// The i
    /// </summary>
    private int i = 0;

    /// <summary>
    /// Receives the menu event.
    /// </summary>
    /// <param name="action">The action.</param>
    public void ReceiveMenuEvent(MenuBehavior.ButtonAction action)
    {
        ++this.i;
        this.eventText.text = "Events:\n" + this.i + ": " + action.ToString() + this.eventText.text.Substring(7);
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        this.eventText = gameObject.GetComponent(typeof(TextMesh)) as TextMesh;
    }
}
