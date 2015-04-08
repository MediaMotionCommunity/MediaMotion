using System.Collections;
using UnityEngine;

/// <summary>
/// MenuMover behavior
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuMoverBehavior : MonoBehaviour
{
    /// <summary>
    /// The hand sweep enabled
    /// </summary>
    public bool HandSweepEnabled;

    /// <summary>
    /// The full throw distance
    /// </summary>
    public float FullThrowDistance;

    /// <summary>
    /// The throw filter
    /// </summary>
    public AnimationCurve ThrowFilter;

    /// <summary>
    /// The throw speed to move from 0 to 1 on the filter.
    /// </summary>
    public float ThrowSpeed;

    /// <summary>
    /// The fade in time
    /// </summary>
    public float FadeInTime;

    /// <summary>
    /// The fade out time
    /// </summary>
    public float FadeOutTime;

    /// <summary>
    /// The fade curve
    /// </summary>
    public AnimationCurve FadeCurve;

    /// <summary>
    /// The fade throw distance
    /// </summary>
    public float FadeThrowDistance;

    /// <summary>
    /// The layout original aspect ratio
    /// </summary>
    public Vector2 LayoutOriginalAspectRatio;

    /// <summary>
    /// The menu roots
    /// </summary>
    private GameObject[] menuRoots;

    /// <summary>
    /// The menus
    /// </summary>
    private GameObject[] menus;

    /// <summary>
    /// The throw location
    /// </summary>
    private float throwLocation;

    /// <summary>
    /// The fade push percent
    /// </summary>
    private float fadePushPercent;

    /// <summary>
    /// The fade start percent
    /// </summary>
    private float fadeStartPercent;

    /// <summary>
    /// The active menu
    /// </summary>
    private GameObject activeMenu;

    /// <summary>
    /// The current state
    /// </summary>
    private MoverState currentState;

    /// <summary>
    /// The fade start time
    /// </summary>
    private float fadeStartTime;

    /// <summary>
    /// The GUI matrix
    /// </summary>
    private Matrix4x4 guiMatrix;

    /// <summary>
    /// Mover state enumeration
    /// </summary>
    private enum MoverState { 
        FADING_IN, FADING_OUT, PASSIVE 
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        this.menuRoots = GameObject.FindGameObjectsWithTag("MenuRoot");
        this.menus = GameObject.FindGameObjectsWithTag("Menu");
        this.currentState = MoverState.PASSIVE;

        ////Account for differing aspect ratios
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);
        Vector2 screenDiff = this.LayoutOriginalAspectRatio - screenSize;

        if (Mathf.Abs(screenDiff.x) < Mathf.Abs(screenDiff.y))
        {
            float amt = this.LayoutOriginalAspectRatio.x / screenSize.x;
            screenSize *= amt;
        }
        else
        {
            float amt = this.LayoutOriginalAspectRatio.y / screenSize.y;
            screenSize *= amt;
        }

        float horizRatio = screenSize.x / (float)this.LayoutOriginalAspectRatio.x;
        float vertRatio = screenSize.y / (float)this.LayoutOriginalAspectRatio.y;

        this.guiMatrix = Matrix4x4.TRS(new Vector3(0, 0, 0), Quaternion.identity, new Vector3(horizRatio, vertRatio, 1));

        foreach (GameObject menu in this.menus)
        {
            MenuBehavior menuScript = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
            menuScript.BaseLocation = this.guiMatrix.MultiplyPoint(menuScript.BaseLocation);
        }
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        bool menuIsActive = false;
        bool menuIsClosing = false;

        foreach (GameObject menu in this.menus)
        {
            MenuBehavior menuBehavior = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
            if (menuBehavior.CurrentState == MenuBehavior.MenuState.ACTIVE || menuBehavior.CurrentState == MenuBehavior.MenuState.ACTIVATING)
            {
                menuIsActive = true;
                this.activeMenu = menu;
            }
            else if (menuBehavior.CurrentState == MenuBehavior.MenuState.DEACTIVATION || menuBehavior.CurrentState == MenuBehavior.MenuState.SELECTION)
            {
                menuIsClosing = true;
            }
        }

        switch (this.currentState)
        {
            case MoverState.FADING_IN:
                this.fadePushPercent = Mathf.Clamp((Time.time - this.fadeStartTime) / (this.FadeInTime * this.fadeStartPercent), 0.0f, 1.0f);

                foreach (GameObject menuRoot in this.menuRoots)
                {
                    GameObject menu = null;
                    MenuBehavior menuRootBehavior = null;
                    foreach (Transform child in menuRoot.transform)
                    {
                        if (child.name == "Menu")
                        {
                            menu = child.gameObject;
                            menuRootBehavior = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
                        }
                    }

                    if (menu != this.activeMenu)
                    {
                        Vector3 awayVector = new Vector3(
                            menuRoot.transform.position.x - this.activeMenu.transform.position.x,
                            menuRoot.transform.position.y - this.activeMenu.transform.position.y,
                            0).normalized * this.FadeCurve.Evaluate(1 - this.fadePushPercent) * this.FadeThrowDistance;

                        if (menuRootBehavior != null)
                        {
                            menuRoot.transform.position = menuRootBehavior.BaseLocation + awayVector;
                            menuRootBehavior.SetOpacity(this.fadePushPercent);
                        }
                    }
                }

                if (Time.time > this.fadeStartTime + (this.FadeInTime * this.fadeStartPercent))
                {
                    this.currentState = MoverState.PASSIVE;

                    foreach (GameObject menu in this.menus)
                    {
                        if (menu != this.activeMenu)
                        {
                            MenuBehavior menuBehavior = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
                            menuBehavior.CurrentState = MenuBehavior.MenuState.INACTIVE;
                        }
                    }
                    return;
                }

                break;
            case MoverState.FADING_OUT:
                this.fadePushPercent = Mathf.Clamp((Time.time - this.fadeStartTime) / this.FadeOutTime, 0.0f, 1.0f);

                if (!menuIsActive)
                {
                    this.fadeStartTime = Time.time;
                    this.fadeStartPercent = this.fadePushPercent;
                    this.currentState = MoverState.FADING_IN;
                    return;
                }

                foreach (GameObject menuRoot in this.menuRoots)
                {
                    GameObject menu = null;
                    MenuBehavior menuRootBehavior = null;
                    foreach (Transform child in menuRoot.transform)
                    {
                        if (child.name == "Menu")
                        {
                            menu = child.gameObject;
                            menuRootBehavior = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
                        }
                    }

                    if (menu != this.activeMenu)
                    {
                        Vector3 awayVector = new Vector3(
                            menuRoot.transform.position.x - this.activeMenu.transform.position.x,
                            menuRoot.transform.position.y - this.activeMenu.transform.position.y,
                            0).normalized * this.FadeCurve.Evaluate(this.fadePushPercent) * this.FadeThrowDistance;
                        if (menuRootBehavior != null)
                        {
                            menuRoot.transform.position = menuRootBehavior.BaseLocation + awayVector;
                            menuRootBehavior.SetOpacity(1.0f - this.fadePushPercent);
                        }
                    }
                }
                break;
            case MoverState.PASSIVE:
                if (menuIsActive && !menuIsClosing)
                {
                    this.fadeStartTime = Time.time;
                    this.currentState = MoverState.FADING_OUT;

                    foreach (GameObject menu in this.menus)
                    {
                        if (menu != this.activeMenu)
                        {
                            MenuBehavior menuBehavior = menu.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
                            menuBehavior.CurrentState = MenuBehavior.MenuState.DISABLED;
                        }
                    }
                    return;
                }

                if (this.HandSweepEnabled)
                {
                    this.throwLocation = Mathf.Clamp(this.throwLocation, 0, 1.0f);

                    foreach (GameObject menuRoot in this.menuRoots)
                    {
                        MenuBehavior menuRootBehavior = null;
                        foreach (Transform child in menuRoot.transform)
                        {
                            if (child.name == "Menu")
                            {
                                menuRootBehavior = child.gameObject.GetComponent(typeof(MenuBehavior)) as MenuBehavior;
                            }
                        }

                        Vector3 awayVector = new Vector3(
                            menuRoot.transform.position.x - gameObject.transform.position.x,
                            menuRoot.transform.position.y - gameObject.transform.position.y,
                            0).normalized * this.ThrowFilter.Evaluate(this.throwLocation) * this.FullThrowDistance;
                        if (menuRootBehavior != null)
                        {
                            menuRoot.transform.position = menuRootBehavior.BaseLocation + awayVector;
                        }
                    }
                }
                break;
        }
    }
}
