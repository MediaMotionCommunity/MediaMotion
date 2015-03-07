using System.Collections;
using MediaMotion.Core;
using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Motion.Actions;
using UnityEngine;

/// <summary>
/// Menu script
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class MenuBehavior : BaseUnityScript<MenuBehavior>
{
    /*
     * Public Configuration Options
     */

    /// <summary>
    /// This enumeration defines what kind of content your menu is using. 
 	/// > TEXT will create menus with text labels on each button. 
 	/// > ICON will allow you to specify Sprite icons that appear on each button. text provided will show up in a sub-label if available. 
 	/// > TEXTURE will allow you to specify a square Texture2D for each button that will be stretched across the button area.
    /// > Each of the menu types can be seen in the example scene.
    /// </summary>
    public MenuType EMenuType;

    /// <summary>
    /// The Game Object containing a MenuEventHandler script. 
    /// This script will accept the button actions sent from menus on selection. 
    /// The logic after this is up to you.
    /// </summary>
    public MenuEventHandler EventHandler;

    /// <summary>
    /// An array of strings. The text label associated with each button option.
    /// </summary>
    public string[] Text;

    /// <summary>
    /// Used in ICON mode only.
    /// An array of Sprites. 
    /// The icon used when the menu option is  selected. 
    /// If an inactive icon is not provided, a reduced alpha version of this icon will be displayed when the menu option is inactive.
    /// </summary>
    public Sprite[] IconsActive;
 
    /// <summary>
    /// Used in ICON mode only. An array of Sprites. The icon used when the menu option is inactive.
    /// </summary>
    public Sprite[] IconsInactive;

    /// <summary>
    /// Used in Texture mode only. The Texture2D applied to the button.
    /// </summary>
    public Texture2D[] Textures;

    /// <summary>
    /// Array of ButtonAction’s. The length of this array determines how many slices the menu circle 
    /// is split into. If a slice is given an action, that slice can interact and will have content.
    /// If the slice is given NONE, it will be an inactive zone. This allows you to create menus that
    /// only use certain wedges of the total circle.
    /// </summary>
    public ButtonAction[] ButtonActions;

    /// <summary>
    /// Often when a menu only uses a certain wedge of the total circle, 
    /// you will want to orient the menu so the wedge is facing the proper direction. 
    /// The angle offset rotates the menu’s orientation. Angles are 0-360 counter clockwise. 
    /// </summary>
    public float AngleOffset; 

    /// <summary>
    /// The radius of the fully activated menu. The radius is the center of the strip.
    /// </summary>
    public float Radius; 

    /// <summary>
    /// The thickness of the menu strip.
    /// </summary>
    public float Thickness;

    /// <summary>
    /// By the menu wedge is captured when the user reaches the center of the wedge. 
    /// The offset allows you to offset this control point from the radius. 
    /// A negative number will move the capture point towards the menu’s center.
    /// </summary>
    public float CaptureOffset;

    /// <summary>
    /// The prefab to use to create each menu wedge.
    /// It is not recommended that you modify this value. 
    /// </summary>
    public GameObject ButtonPrefab;

    /// <summary>
    /// The radius from the menu center (in pixels) where the menu will activate.
    /// </summary>
    public float ActivationRadius;

    /// <summary>
    /// The radius from the menu center where a menu button will be selected.
    /// </summary>
    public float SelectionRadius; 

    /// <summary>
    /// The world space Z of the Leap’s finger at which the menu will deactivate.
    /// </summary>
    public float DeactivateZ;

    /// <summary>
    /// The speed at which the menu will scale down on deactivation.
    /// </summary>
    public float DeactivationSpeed;

    /// <summary>
    /// The easing curve for the menu activation action.
    /// </summary>
    public AnimationCurve ActivationCurve;

    /// <summary>
    /// The total time of the activation animation.
    /// </summary>
    public float ActivationTime;

    /// <summary>
    /// The distance from the wedge center when the highlight color transition will begin.
    /// </summary>
    public float StartHighlight;

    /// <summary>
    /// The distance from the wedge center when the highlight color transition will complete.
    /// </summary>
    public float FullHighlight;

    /// <summary>
    /// The un-activated color of the menu wedges. 
    /// </summary>
    public Color BaseColor; 

    /// <summary>
    /// The color of a previously selected menu wedge.
    /// </summary>
    public Color SelectedColor;

    /// <summary>
    /// The color of a highlighted menu wedge.
    /// </summary>
    public Color HighlightColor;

    /// <summary>
    /// The scale of ICON and TEXT content when the wedge becomes highlighted.
    /// </summary>
    public float HighlightPercentGrowth;

    /// <summary>
    /// Similar to Deactivation Speed. 
    /// The Scale down speed is the speed the other wedges scale down when another menu wedge is selected.
    /// </summary>
    public float ScaleDownSpeed;

    /// <summary>
    /// The _selection delay time
    /// </summary>
    public float SelectionDelayTime;

    /// <summary>
    /// The _selection snap distance
    /// </summary>
    public float SelectionSnapDistance;

    /// <summary>
    /// When a wedge is selected, it snaps back towards the menu center a small amount. 
    /// This is how long that snap takes.
    /// </summary>
    public float SelectionSnapTime;

    /// <summary>
    /// When a wedge is selected, it snaps back towards the menu center a small amount. 
    /// This is how far it snaps back.
    /// </summary>
    public float SpriteScalingFactor;

    /// <summary>
    /// This is how long the menu will wait to select another wedge after a wedge has been selected. 
    /// </summary>
    public float SelectionCooldown;

    /// <summary>
    /// The _input
    /// </summary>
    public IInputService Input;

    /// <summary>
    /// The input
    /// </summary>
    private IInputService input;

    /// <summary>
    /// The button count
    /// </summary>
    private int buttonCount;

    /// <summary>
    /// The buttons
    /// </summary>
    private GameObject[] buttons;

    /// <summary>
    /// The current state
    /// </summary>
    private MenuState currentState;

    /// <summary>
    /// The current selection
    /// </summary>
    private int currentSelection = -1;

    /// <summary>
    /// The main cam
    /// </summary>
    private Camera mainCam;

    /// <summary>
    /// The UI cam
    /// </summary>
    private Camera uiCam;

    /// <summary>
    /// The closest
    /// </summary>
    private int closest = -1;

    /// <summary>
    /// The last closest
    /// </summary>
    private int lastClosest = -1;

    /// <summary>
    /// The activation start time
    /// </summary>
    private float activationStartTime;

    /// <summary>
    /// The closest distance
    /// </summary>
    private float closestDistance = float.MaxValue;

    /// <summary>
    /// The scaling factor
    /// </summary>
    private float scalingFactor = 1.0f;

    /// <summary>
    /// The selection end time
    /// </summary>
    private float selectionEndTime;

    /// <summary>
    /// The current selection offset
    /// </summary>
    private float currentSelectionOffset;

    /// <summary>
    /// The selection made
    /// </summary>
    private bool selectionMade = false;

    /// <summary>
    /// The sub label
    /// </summary>
    private TextMesh subLabel;

    /// <summary>
    /// The has sub label
    /// </summary>
    private bool hasSubLabel;

    /// <summary>
    /// The selection cool down time
    /// </summary>
    private float selectionCooldownTime = 0;

    /// <summary>
    /// The base location
    /// </summary>
    private Vector3 baseLocation;

    /// <summary>
    /// Menu type enumeration
    /// </summary>
    public enum MenuType { 
        ICON, TEXT, TEXTURE 
    }

    /// <summary>
    /// Menu state enumeration
    /// </summary>
    public enum MenuState { 
        INACTIVE, ACTIVATING, ACTIVE, SELECTION, DEACTIVATION, DISABLED 
    }

    /// <summary>
    /// Button action enumeration
    /// </summary>
    public enum ButtonAction
    {
        NONE,
        ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN,
        STRENGTH_LOW, STRENGTH_MEDIUM, STRENGTH_HIGH,
        TOOL_FLATTEN, TOOL_GROW, TOOL_PAINT, TOOL_PRESS, TOOL_REPEL, TOOL_SMOOTH, TOOL_SWEEP,
        ENV_ARCTIC, ENV_DESERT, ENV_ISLAND, ENV_JUNGLE, ENV_CLIFF, ENV_REDWOOD, ENV_RIVER,
        MAT_CLAY, MAT_GLASS, MAT_PLASTIC, MAT_PORCELAIN, MAT_STEEL
    }

    /// <summary>
    /// Gets or sets the state of the current.
    /// </summary>
    /// <value>
    /// The state of the current.
    /// </value>
    public MenuState CurrentState
    {
        get { return this.currentState; }
        set { this.currentState = value; }
    }

    /// <summary>
    /// Gets or sets the base location.
    /// </summary>
    /// <value>
    /// The base location.
    /// </value>
    public Vector3 BaseLocation
    {
        get { 
            return this.baseLocation; 
        }
        set
        {
            this.baseLocation = value;
            gameObject.transform.parent.position = value;
        }
    }

    /// <summary>
    /// Sets the opacity.
    /// </summary>
    /// <param name="opacity">The opacity.</param>
    public void SetOpacity(float opacity)
    {
        opacity = Mathf.Clamp(opacity, 0.0f, 1.0f);

        Color current = gameObject.transform.parent.GetComponent<Renderer>().material.color;
        gameObject.transform.parent.GetComponent<Renderer>().material.color = new Color(current.r, current.g, current.b, opacity);

        if (this.hasSubLabel)
        {
            current = this.subLabel.GetComponent<Renderer>().material.color;
            this.subLabel.GetComponent<Renderer>().material.color = new Color(current.r, current.g, current.b, opacity);
        }
    }

    /// <summary>
    /// Initializes the specified input.
    /// </summary>
    /// <param name="input">The input.</param>
    public void Init(IInputService input)
    {
        //// Get references to the main scene and UI cameras.
        this.uiCam = this.mainCam = (GameObject.Find("Cameras/Main") as GameObject).GetComponent(typeof(Camera)) as Camera;
        this.baseLocation = gameObject.transform.parent.position;
        this.Input = input;

        //// Get a reference to the subLabel
        foreach (Transform child in gameObject.transform.parent)
        {
            if (child.name == "menuSub")
            {
                this.subLabel = child.gameObject.GetComponent(typeof(TextMesh)) as TextMesh;
                this.hasSubLabel = true;
            }
        }

        float segmentSweep; //// how large is each button segment

        this.buttonCount = this.ButtonActions.Length;

        this.buttons = new GameObject[this.buttonCount];
        segmentSweep = 360.0f / (float)this.buttonCount;

        //// Create the buttons, fill in their content, etc.
        for (int i = 0; i < this.buttonCount; i++)
        {
            this.buttons[i] = Instantiate(this.ButtonPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
            this.buttons[i].transform.parent = gameObject.transform;
            ArcMaker buttonScript = this.buttons[i].GetComponent(typeof(ArcMaker)) as ArcMaker;
            buttonScript.CreateMesh(50, (i * segmentSweep) + this.AngleOffset, (i * segmentSweep) + segmentSweep + this.AngleOffset, this.Radius - (this.Thickness / 2.0f), this.Radius + (this.Thickness / 2.0f));

            //// Setup the button content
            if (this.ButtonActions[i] != ButtonAction.NONE)
            {
                switch (this.EMenuType)
                {
                    case MenuType.ICON:
                        if (i < this.IconsActive.Length && this.IconsActive[i] != null)
                        {
                            if (i < this.IconsInactive.Length && this.IconsInactive[i] != null)
                            {
                                buttonScript.SetContent(this.IconsActive[i], this.IconsInactive[i], this.SpriteScalingFactor);
                            }
                            else
                            {
                                buttonScript.SetContent(this.IconsActive[i], null, this.SpriteScalingFactor);
                            }
                        }
                        else
                        {
                            Debug.LogError("Active icon missing for: " + i);
                        }
                        break;
                    case MenuType.TEXT:
                        if (i < this.Text.Length && this.Text[i] != null)
                        {
                            buttonScript.SetContent(this.Text[i]);
                        }
                        else
                        {
                            Debug.LogError("Text missing for: " + i);
                        }
                        break;
                    case MenuType.TEXTURE:
                        if (i < this.Textures.Length && this.Textures[i] != null)
                        {
                            buttonScript.SetContent(this.Textures[i]);
                        }
                        else
                        {
                            Debug.LogError("Texture missing for: " + i);
                        }
                        break;
                }
            }
        }

        gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        gameObject.transform.localRotation = new Quaternion(60, 0, 0, 180);
        this.currentState = MenuState.INACTIVE;
    }

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        foreach (IAction Action in this.Input.GetMovements())
        {
            if (Action.Type == ActionType.Cursor)
            {
                float x = (Action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Pos.X;
                float y = (Action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Pos.Y;

                Vector2 leapScreen = new Vector2(x, y);

                Vector2 parentScreen = new Vector2(
                    this.uiCam.WorldToScreenPoint(gameObject.transform.parent.position).x,
                    this.uiCam.WorldToScreenPoint(gameObject.transform.parent.position).y);

                Vector2 menuScreen = new Vector2(
                    this.uiCam.WorldToScreenPoint(gameObject.transform.position).x,
                    this.uiCam.WorldToScreenPoint(gameObject.transform.position).y);

                Vector2 parentToFinger = leapScreen - parentScreen;
                Vector2 toFrontFinger = leapScreen - menuScreen;

                //// Menu Wide Updates per state
                switch (this.currentState)
                {
                    case MenuState.INACTIVE:

                        if (parentToFinger.magnitude < this.ActivationRadius && (Action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Pos.Z > this.DeactivateZ)
                        {
                            this.activationStartTime = Time.time;
                            this.currentState = MenuState.ACTIVATING;
                        }

                        if (this.hasSubLabel && this.currentSelection != -1 && this.currentSelection < this.Text.Length && this.Text[this.currentSelection] != null)
                        {
                            this.subLabel.text = this.Text[this.currentSelection];
                        }
                        break;
                    case MenuState.ACTIVATING:
                        this.selectionMade = false;
                        if (Time.time <= this.activationStartTime + this.ActivationTime)
                        {
                            float currentScale = this.ActivationCurve.Evaluate((Time.time - this.activationStartTime) / (this.ActivationTime));
                            gameObject.transform.localScale = new Vector3(
                                currentScale,
                                currentScale,
                                1);
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(1,
                                                                          1,
                                                                          1);
                            currentState = MenuState.ACTIVE;
                            return;
                        }
                        break;
                    case MenuState.ACTIVE:
                        if ((Action.Parameter as MediaMotion.Motion.Actions.Parameters.Object3).Pos.Z < this.DeactivateZ)
                        {
                            this.selectionMade = false;
                            this.scalingFactor = 1.0f;
                            this.currentState = MenuState.DEACTIVATION;
                            return;
                        }

                        if (Time.time >= this.selectionCooldownTime)
                        {
                            this.closest = -1;
                            this.closestDistance = float.MaxValue;
                        }
                        break;
                    case MenuState.SELECTION:

                        this.scalingFactor = Mathf.Clamp(this.scalingFactor - (this.ScaleDownSpeed * Time.deltaTime), 0.0f, 1.0f);
                        this.currentSelectionOffset = Mathf.Clamp((float)(Time.time - this.selectionEndTime + this.SelectionDelayTime) / (float)(this.SelectionSnapTime), 0.0f, 1.0f) * this.SelectionSnapDistance;
                        if (Time.time >= this.selectionEndTime)
                        {
                            this.selectionMade = true;
                            this.currentState = MenuState.DEACTIVATION;
                            return;
                        }
                        break;
                    case MenuState.DEACTIVATION:
                        if (gameObject.transform.localScale.x > 0)
                        {
                            gameObject.transform.localScale = new Vector3(
                                gameObject.transform.localScale.x - (this.DeactivationSpeed * Time.deltaTime),
                                gameObject.transform.localScale.y - (this.DeactivationSpeed * Time.deltaTime),
                                1);
                        }
                        else
                        {
                            gameObject.transform.localScale = new Vector3(0, 0, 1);
                            currentState = MenuState.INACTIVE;
                            return;
                        }
                        break;
                }

                //// Per Button Updates per state
                for (int i = 0; i < this.buttonCount; i++)
                {
                    ArcMaker current = this.buttons[i].GetComponent(typeof(ArcMaker)) as ArcMaker;
                    current.Bottom = this.Radius - (this.Thickness / 2.0f);
                    current.Top = this.Radius + (this.Thickness / 2.0f);

                    if (i != this.closest)
                    {
                        current.MakeInactive();
                        if (i == this.currentSelection)
                        {
                            this.buttons[i].GetComponent<Renderer>().material.color = Color.Lerp(this.buttons[i].GetComponent<Renderer>().material.color, this.SelectedColor, 0.25f); 
                        } else {
                            this.buttons[i].GetComponent<Renderer>().material.color = Color.Lerp(this.buttons[i].GetComponent<Renderer>().material.color, this.BaseColor, 0.25f);
                        }
                    }

                    if (i == this.currentSelection) { 
                        current.MakeActive(); 
                    }

                    switch (this.currentState)
                    {
                        case MenuState.INACTIVE:
                            this.buttons[i].SetActive(false);
                            break;
                        case MenuState.ACTIVATING:
                            this.buttons[i].SetActive(true);
                            current.ContentScaleFactor = 1.0f;
                            break;
                        case MenuState.ACTIVE:
                            Vector2 buttonCenter = this.uiCam.WorldToScreenPoint(this.buttons[i].GetComponent<Renderer>().bounds.center);
                            Vector2 toButton = (Vector2)leapScreen - (Vector2)buttonCenter;

                            if (Time.time >= this.selectionCooldownTime && toButton.magnitude < this.closestDistance)
                            {
                                this.closestDistance = toButton.magnitude;
                                this.closest = i;
                            }

                            current.ContentScaleFactor = 1.0f;
                            break;
                        case MenuState.SELECTION:
                            if (i != this.closest)
                            {
                                current.Bottom *= this.scalingFactor;
                                current.Top *= this.scalingFactor;
                                current.ContentScaleFactor = this.scalingFactor;
                            }
                            else
                            {
                                current.Bottom = this.SelectionRadius + currentSelectionOffset - (this.Thickness / 2.0f);
                                current.Top = this.SelectionRadius + currentSelectionOffset + (this.Thickness / 2.0f);
                            }
                            break;
                        case MenuState.DEACTIVATION:
                            if (i != this.closest || !this.selectionMade)
                            {
                                current.Bottom *= this.scalingFactor;
                                current.Top *= this.scalingFactor;
                            }
                            else if (selectionMade)
                            {
                                current.Bottom = this.SelectionRadius - (this.Thickness / 2.0f);
                                current.Top = this.SelectionRadius + (this.Thickness / 2.0f);
                            }
                            break;
                    }
                }

                //// Behavior for selected item
                if (this.currentState == MenuState.ACTIVE)
                {
                    if (this.closest != this.lastClosest)
                    {
                        this.lastClosest = this.closest;
                        this.selectionCooldownTime = Time.time + this.SelectionCooldown;
                    }

                    //// do things with the closest menu
                    if (this.closest != -1)
                    {
                        ArcMaker selected = this.buttons[this.closest].GetComponent(typeof(ArcMaker)) as ArcMaker;

                        float pixelDistance = (menuScreen - leapScreen).magnitude;

                        //// convert world distance from pixels to world units.
                        float worldDistance = pixelDistance * ((this.uiCam.orthographicSize * 2.0f) / (float)this.uiCam.pixelHeight);

                        if (this.ButtonActions[this.closest] == ButtonAction.NONE)
                        {
                            if (worldDistance > this.Radius + (this.Thickness / 2.0f))
                            {
                                this.selectionMade = false;
                                this.scalingFactor = 1.0f;
                                this.currentState = MenuState.DEACTIVATION;
                                return;
                            }
                        }
                        else
                        {
                            selected.MakeActive();

                            if (this.hasSubLabel && this.closest != -1 && this.closest < this.Text.Length && this.Text[this.closest] != null)
                            {
                                this.subLabel.text = this.Text[this.closest];
                            }

                            //// pull out wedge                                           
                            if (worldDistance - this.CaptureOffset > this.Radius)
                            {
                                selected.Bottom = worldDistance - this.CaptureOffset - (this.Thickness / 2.0f);
                                selected.Top = worldDistance - this.CaptureOffset + (this.Thickness / 2.0f);

                                if (worldDistance - this.CaptureOffset > this.SelectionRadius)
                                {
                                    this.selectionEndTime = Time.time + this.SelectionDelayTime;
                                    this.currentSelection = this.closest;
                                    this.scalingFactor = 1.0f;

                                    if (this.EventHandler != null && this.closest < this.ButtonActions.Length)
                                    {
                                        this.EventHandler.ReceiveMenuEvent(this.ButtonActions[this.closest]);
                                    }

                                    this.currentState = MenuState.SELECTION;
                                }
                            }

                            float highlightPercent = Mathf.Clamp((worldDistance - this.FullHighlight) / (this.StartHighlight - this.FullHighlight), 0.0f, 1.0f);
                            if (this.closest == this.currentSelection) {
                                this.buttons[this.closest].GetComponent<Renderer>().material.color = Color.Lerp(this.SelectedColor, this.HighlightColor, highlightPercent); 
                            } else { 
                                this.buttons[this.closest].GetComponent<Renderer>().material.color = Color.Lerp(this.BaseColor, this.HighlightColor, highlightPercent); 
                            }
                            selected.ContentScaleFactor = 1.0f + (highlightPercent * this.HighlightPercentGrowth);
                        }
                    }
                }
            }
        }
    }
}
