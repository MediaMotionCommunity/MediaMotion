using MediaMotion.Core.Models.Scripts;
using MediaMotion.Core.Services.Input.Interfaces;
using MediaMotion.Modules.Explorer.Services.CursorManager.Interfaces;
using MediaMotion.Core.Models.FileManager.Interfaces;
using UnityEngine;

/// <summary>
/// Menu script
/// base is from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
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

	private ICursorManagerService CursorManagerService;

		/// <summary>
		/// Menu type enumeration
		/// </summary>
		public enum MenuType
		{
				ICON,
				TEXT,
				TEXTURE
		}

		/// <summary>
		/// Menu state enumeration
		/// </summary>
		public enum MenuState
		{
				INACTIVE,
				ACTIVATING,
				ACTIVE,
				SELECTION,
				DEACTIVATION,
				DISABLED
		}

		/// <summary>
		/// Button action enumeration
		/// </summary>
		public enum ButtonAction
		{
				NONE,
				ONE,
				TWO,
				THREE,
				FOUR,
				FIVE,
				SIX,
				SEVEN,
				EIGHT,
				NINE,
				TEN,
				COPY,
				PASTE,
				DELETE,
				CANCEL
		}

		/// <summary>
		/// Gets or sets the state of the current.
		/// </summary>
		/// <value>
		/// The state of the current.
		/// </value>
		public MenuState CurrentState {
				get { return this.currentState; }
				set { this.currentState = value; }
		}

		/// <summary>
		/// Gets or sets the base location.
		/// </summary>
		/// <value>
		/// The base location.
		/// </value>
		public Vector3 BaseLocation {
				get { 
						return this.baseLocation; 
				}
				set {
						this.baseLocation = value;
						gameObject.transform.parent.position = value;
				}
		}

		/// <summary>
		/// Sets the opacity.
		/// </summary>
		/// <param name="opacity">The opacity.</param>
		public void SetOpacity (float opacity)
		{
				opacity = Mathf.Clamp (opacity, 0.0f, 1.0f);

				Color current = gameObject.transform.parent.GetComponent<Renderer> ().material.color;
				gameObject.transform.parent.GetComponent<Renderer> ().material.color = new Color (current.r, current.g, current.b, opacity);

				if (this.hasSubLabel) {
						current = this.subLabel.GetComponent<Renderer> ().material.color;
						this.subLabel.GetComponent<Renderer> ().material.color = new Color (current.r, current.g, current.b, opacity);
				}
		}

		/// <summary>
		/// Initializes the specified input.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <param name="cursorManageService">The cursor manager service.</param>
	public void Init (IInputService input, ICursorManagerService cursorManageService)
		{
				this.CursorManagerService = cursorManageService;
				this.uiCam = GameObject.Find ("Cameras/Main").GetComponent (typeof(Camera)) as Camera;
				this.baseLocation = gameObject.transform.parent.position;
				this.Input = input;

				foreach (Transform child in gameObject.transform.parent) {
						if (child.name == "menuSub") {
								this.subLabel = child.gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
								this.hasSubLabel = true;
						}
				}

				float segmentSweep;

				this.buttonCount = this.ButtonActions.Length;

				this.buttons = new GameObject[this.buttonCount];
				segmentSweep = 360.0f / (float)this.buttonCount;

				for (int i = 0; i < this.buttonCount; i++) {
						this.buttons [i] = Instantiate (this.ButtonPrefab, gameObject.transform.position, Quaternion.identity) as GameObject;
						this.buttons [i].transform.parent = gameObject.transform;
						ArcMaker buttonScript = this.buttons [i].GetComponent (typeof(ArcMaker)) as ArcMaker;
						buttonScript.CreateMesh (50, (i * segmentSweep) + this.AngleOffset, (i * segmentSweep) + segmentSweep + this.AngleOffset, this.Radius - (this.Thickness / 2.0f), this.Radius + (this.Thickness / 2.0f));

						if (this.ButtonActions [i] != ButtonAction.NONE) {
								switch (this.EMenuType) {
								case MenuType.ICON:
										if (i < this.IconsActive.Length && this.IconsActive [i] != null) {
												if (i < this.IconsInactive.Length && this.IconsInactive [i] != null) {
														buttonScript.SetContent (this.IconsActive [i], this.IconsInactive [i], this.SpriteScalingFactor);
												} else {
														buttonScript.SetContent (this.IconsActive [i], null, this.SpriteScalingFactor);
												}
										} else {
												Debug.LogError ("Active icon missing for: " + i);
										}
										break;
								case MenuType.TEXT:
										if (i < this.Text.Length && this.Text [i] != null) {
												buttonScript.SetContent (this.Text [i]);
										} else {
												Debug.LogError ("Text missing for: " + i);
										}
										break;
								case MenuType.TEXTURE:
										if (i < this.Textures.Length && this.Textures [i] != null) {
												buttonScript.SetContent (this.Textures [i]);
										} else {
												Debug.LogError ("Texture missing for: " + i);
										}
										break;
								}
						}
				}

				gameObject.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
				gameObject.transform.localRotation = new Quaternion (60, 0, 0, 180);
				this.currentState = MenuState.ACTIVE;
		}

		/// <summary>
		/// Updates this instance.
		/// </summary>
		private void Update ()
		{

				if (CursorManagerService.GetMainCursor() != null) {
						CursorIsPresent ();
				} else {
						CursorIsAbscent ();
				}
		}

		private void CursorIsPresent ()
		{

				Vector3 cursorPosition = CursorManagerService.GetMainCursor().transform.position;

				Vector2 leapScreen = new Vector2 (
						                     this.uiCam.WorldToScreenPoint (cursorPosition).x,
						                     this.uiCam.WorldToScreenPoint (cursorPosition).y);

				Vector2 parentScreen = new Vector2 (
						                       this.uiCam.WorldToScreenPoint (gameObject.transform.parent.position).x,
						                       this.uiCam.WorldToScreenPoint (gameObject.transform.parent.position).y);

				Vector2 menuScreen = new Vector2 (
						                     this.uiCam.WorldToScreenPoint (gameObject.transform.position).x,
						                     this.uiCam.WorldToScreenPoint (gameObject.transform.position).y);

				Vector2 parentToFinger = leapScreen - parentScreen;

				// Menu Wide Updates per state
				switch (this.currentState) {
				case MenuState.INACTIVE:
						MenuIsInactive (parentToFinger, cursorPosition);
						break;
				case MenuState.ACTIVATING:
						MenuIsActivating ();
						break;
				case MenuState.ACTIVE:
						MenuIsActive (cursorPosition);
						break;
				case MenuState.SELECTION:
						MenuIsSelection ();
						break;
				case MenuState.DEACTIVATION:
						MenuIsDeactivation ();
						break;
				}

				// Per Button Updates per state
				for (int i = 0; i < this.buttonCount; i++) {
						ArcMaker current = RetrieveAndInitArcMaker (i);

						if (i != this.closest) {
								current.MakeInactive ();
								this.buttons [i].GetComponent<Renderer> ().material.color = new Color (0.204f, 0.349f, 0.698f, 1f);
						}
								
						if (i == this.currentSelection) { 
								current.MakeActive (); 
						}

						switch (this.currentState) {
						case MenuState.INACTIVE:
								this.buttons [i].SetActive (false);
								break;
						case MenuState.ACTIVATING:
								this.buttons [i].SetActive (true);
								current.ContentScaleFactor = 1.0f;
								break;
						case MenuState.ACTIVE:
								ButtonMenuIsActive (i, leapScreen, current);
								break;
						case MenuState.SELECTION:
								ButtonMenuIsSelection (i, current);
								break;
						case MenuState.DEACTIVATION:
								ButtonMenuIsDeactivation (i, current);
								break;
						}
				}

				if (this.currentState == MenuState.ACTIVE) {
						if (this.closest != this.lastClosest) {
								this.lastClosest = this.closest;
								this.selectionCooldownTime = Time.time + this.SelectionCooldown;
						}

						if (this.closest != -1) {
								DrawClosestMenu (menuScreen, leapScreen);
						}
				}
		}

		private void CursorIsAbscent ()
		{
				// If there is no cursor, there is no hand. We deactivate the wheeltool.
				if (this.currentState != MenuState.INACTIVE) {
						this.CurrentState = MenuState.DEACTIVATION;	
						CursorManagerService.EnabledCursors ();
				}
				switch (this.currentState) {
				case MenuState.DEACTIVATION:
						MenuIsDeactivation ();
						for (int i = 0; i < this.buttonCount; i++) {
								ButtonMenuIsDeactivation (i, RetrieveAndInitArcMaker (i));
						}
						break;
				case MenuState.SELECTION:
						MenuIsSelection ();
						for (int i = 0; i < this.buttonCount; i++) {
								ButtonMenuIsSelection (i, RetrieveAndInitArcMaker (i));
						}
						break;
				}
		}

		private ArcMaker RetrieveAndInitArcMaker (int i)
		{
				ArcMaker current = this.buttons [i].GetComponent (typeof(ArcMaker)) as ArcMaker;
				current.Bottom = this.Radius - (this.Thickness / 2.0f);
				current.Top = this.Radius + (this.Thickness / 2.0f);
				return current;
		}

	private IElement selectedElement;

	public void ActiveWheelTool(IElement element) {
		this.selectedElement = element;
		this.activationStartTime = Time.time;
		this.currentState = MenuState.ACTIVATING;
		CursorManagerService.DisabledCursors ();
	}

	public void DeactiveWheelTool() {
		this.selectionMade = false;
		this.scalingFactor = 1.0f;
		this.currentState = MenuState.DEACTIVATION;
		CursorManagerService.EnabledCursors ();
	}

		private void MenuIsInactive (Vector2 parentToFinger, Vector3 cursorPosition)
		{
				if (parentToFinger.magnitude < this.ActivationRadius && this.uiCam.WorldToScreenPoint (cursorPosition).z > this.DeactivateZ) {

						this.activationStartTime = Time.time;
						this.currentState = MenuState.ACTIVATING;
						CursorManagerService.DisabledCursors ();
				}

				if (this.hasSubLabel && this.currentSelection != -1 && this.currentSelection < this.Text.Length && this.Text [this.currentSelection] != null) {
						this.subLabel.text = this.Text [this.currentSelection];
				}
		}

		private void MenuIsActivating ()
		{
				this.selectionMade = false;
				if (Time.time <= this.activationStartTime + this.ActivationTime) {
						float currentScale = this.ActivationCurve.Evaluate ((Time.time - this.activationStartTime) / (this.ActivationTime));
						float percent = currentScale * 100f;
						currentScale = (0.2f * percent) / 100f;
						gameObject.transform.localScale = new Vector3 (
								currentScale,
								currentScale,
								0.2f);
				} else {
						gameObject.transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
						currentState = MenuState.ACTIVE;
						return;
				}
		}

		private void MenuIsActive (Vector3 cursorPosition)
		{
				if (this.uiCam.WorldToScreenPoint (cursorPosition).z < this.DeactivateZ) {
						this.selectionMade = false;
						this.scalingFactor = 1.0f;
						this.currentState = MenuState.DEACTIVATION;
						CursorManagerService.EnabledCursors ();
						return;
				}

				if (Time.time >= this.selectionCooldownTime) {
						this.closest = -1;
						this.closestDistance = float.MaxValue;
				}
		}

		private void MenuIsSelection ()
		{
				this.scalingFactor = Mathf.Clamp (this.scalingFactor - (this.ScaleDownSpeed * Time.deltaTime), 0.0f, 1.0f);
				this.currentSelectionOffset = Mathf.Clamp ((Time.time - this.selectionEndTime + this.SelectionDelayTime) / this.SelectionSnapTime, 0.0f, 1.0f) * this.SelectionSnapDistance;
				if (Time.time >= this.selectionEndTime) {
						this.selectionMade = true;
						this.currentState = MenuState.DEACTIVATION;
						CursorManagerService.EnabledCursors ();
						return;
				}
		}

		private void MenuIsDeactivation ()
		{
				if (gameObject.transform.localScale.x > 0) {
						gameObject.transform.localScale = new Vector3 (
								gameObject.transform.localScale.x - (this.DeactivationSpeed * Time.deltaTime),
								gameObject.transform.localScale.y - (this.DeactivationSpeed * Time.deltaTime),
								0.2f);
				} else {
						gameObject.transform.localScale = new Vector3 (0, 0, 1);
						currentState = MenuState.INACTIVE;
						return;
				}
		}

		private void ButtonMenuIsActive (int i, Vector2 leapScreen, ArcMaker current)
		{
				Vector2 buttonCenter = this.uiCam.WorldToScreenPoint (this.buttons [i].GetComponent<Renderer> ().bounds.center);
				Vector2 toButton = leapScreen - buttonCenter;

				if (Time.time >= this.selectionCooldownTime && toButton.magnitude < this.closestDistance) {
						this.closestDistance = toButton.magnitude;
						this.closest = i;
				}

				current.ContentScaleFactor = 1.0f;
		}

		private void ButtonMenuIsSelection (int i, ArcMaker current)
		{
				if (i != this.closest) {
						current.Bottom *= this.scalingFactor;
						current.Top *= this.scalingFactor;
						current.ContentScaleFactor = this.scalingFactor;
				} else {
						current.Bottom = this.SelectionRadius + currentSelectionOffset - (this.Thickness / 2.0f);
						current.Top = this.SelectionRadius + currentSelectionOffset + (this.Thickness / 2.0f);
				}
		}

		private void ButtonMenuIsDeactivation (int i, ArcMaker current)
		{
				if (i != this.closest || !this.selectionMade) {
						current.Bottom *= this.scalingFactor;
						current.Top *= this.scalingFactor;
				} else if (selectionMade) {
						current.Bottom = this.SelectionRadius - (this.Thickness / 2.0f);
						current.Top = this.SelectionRadius + (this.Thickness / 2.0f);
				}
		}

		private void DrawClosestMenu (Vector2 menuScreen, Vector2 leapScreen)
		{
				ArcMaker selected = this.buttons [this.closest].GetComponent (typeof(ArcMaker)) as ArcMaker;

				float pixelDistance = (menuScreen - leapScreen).magnitude;

				// convert world distance from pixels to world units.
				float worldDistance = pixelDistance * ((this.uiCam.orthographicSize * 2.0f) / (float)this.uiCam.pixelHeight);

				if (this.ButtonActions [this.closest] == ButtonAction.NONE && worldDistance > this.Radius + (this.Thickness / 2.0f)) {
						this.selectionMade = false;
						this.scalingFactor = 1.0f;
						this.currentState = MenuState.DEACTIVATION;
						CursorManagerService.EnabledCursors ();
						return;
				} else {
						selected.MakeActive ();

						if (this.hasSubLabel && this.closest != -1 && this.closest < this.Text.Length && this.Text [this.closest] != null) {
								this.subLabel.text = this.Text [this.closest];
						}


						// pull out wedge                                           
						if (worldDistance - this.CaptureOffset > this.Radius) {

								selected.Bottom = worldDistance - this.CaptureOffset - (this.Thickness / 2.0f);
								selected.Top = worldDistance - this.CaptureOffset + (this.Thickness / 2.0f);

								if (worldDistance - this.CaptureOffset > this.SelectionRadius) {

										this.selectionEndTime = Time.time + this.SelectionDelayTime;
										this.currentSelection = this.closest;
										this.scalingFactor = 0.2f;

										if (this.EventHandler != null && this.closest < this.ButtonActions.Length) {
						this.EventHandler.ReceiveMenuEvent (this.ButtonActions [this.closest], this.selectedElement);
										}

										this.currentState = MenuState.SELECTION;
								}
						}

						float highlightPercent = Mathf.Clamp ((worldDistance - this.FullHighlight) / (this.StartHighlight - this.FullHighlight), 0.0f, 1.0f);
						this.buttons [this.closest].GetComponent<Renderer> ().material.color = new Color (0.298f, 0.502f, 1f, 1f);
						selected.ContentScaleFactor = 1.0f + (highlightPercent * this.HighlightPercentGrowth);
				}
		}
}

