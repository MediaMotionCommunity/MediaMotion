using System.Collections;
using UnityEngine;

/// <summary>
/// ArcMaker
/// from: <see href="https://github.com/leapmotion-examples/unity/tree/master/v1/freeform-menus">LeapMotion examples</see>
/// </summary>
public class ArcMaker : MonoBehaviour
{
    /// <summary>
    /// The start angle
    /// </summary>
    public float StartAngle;

    /// <summary>
    /// The end angle
    /// </summary>
    public float EndAngle;

    /// <summary>
    /// The bottom
    /// </summary>
    public float Bottom;

    /// <summary>
    /// The top
    /// </summary>
    public float Top;

    /// <summary>
    /// The quad count
    /// </summary>
    public int QuadCount;

    /// <summary>
    /// The draw mesh
    /// </summary>
    public bool DrawMesh = false;

    /// <summary>
    /// The content scale factor
    /// </summary>
    public float ContentScaleFactor = 1.0f;

    /// <summary>
    /// The active sprite
    /// </summary>
    private GameObject activeSprite;

    /// <summary>
    /// The inactive sprite
    /// </summary>
    private GameObject inactiveSprite;

    /// <summary>
    /// The text label
    /// </summary>
    private GameObject textLabel;

    /// <summary>
    /// The arc type
    /// </summary>
    private MenuBehavior.MenuType arcType;

    /// <summary>
    /// The is active
    /// </summary>
    private bool isActive = false;

    /// <summary>
    /// The inactive sprite available
    /// </summary>
    private bool inactiveSpriteAvailable = false;

    /// <summary>
    /// The sprite scaling factor
    /// </summary>
    private float spriteScalingFactor = 1.0f;

    /// <summary>
    /// The mf
    /// </summary>
    private MeshFilter mf;

    /// <summary>
    /// The mesh
    /// </summary>
    private Mesh mesh;

    /// <summary>
    /// The vertices
    /// </summary>
    private Vector3[] vertices;

    /// <summary>
    /// An array of integer
    /// </summary>
    private int[] tris;

    /// <summary>
    /// An array of normal
    /// </summary>
    private Vector3[] normals;

    /// <summary>
    /// An array of vector
    /// </summary>
    private Vector2[] uvs;

    /// <summary>
    /// Gets a value indicating whether this instance is active.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
    /// </value>
    public bool IsActive
    {
        get { return this.isActive; }
    }

    /// <summary>
    /// Creates the mesh.
    /// </summary>
    /// <param name="quadCount">The quad count.</param>
    /// <param name="startAngle">The start angle.</param>
    /// <param name="endAngle">The end angle.</param>
    /// <param name="bottom">The bottom.</param>
    /// <param name="top">The top.</param>
    public void CreateMesh(int quadCount = 50, float startAngle = 0, float endAngle = 360, float bottom = 1, float top = 5)
    {
        if (startAngle != -1) {
            this.StartAngle = startAngle; 
        }
        if (endAngle != -1) {
            this.EndAngle = endAngle; 
        }
        if (bottom != -1) {
            this.Bottom = bottom; 
        }
        if (top != -1) {
            this.Top = top; 
        }
        this.QuadCount = quadCount;

        this.DrawMesh = true;
    }

    /// <summary>
    /// Sets the content.
    /// </summary>
    /// <param name="text">The text.</param>
    public void SetContent(string text)
    {
        if (this.textLabel == null)
        {
            this.FindChildren(); 
        }
        this.arcType = MenuBehavior.MenuType.TEXT;
        (this.textLabel.GetComponent(typeof(TextMesh)) as TextMesh).text = text;
        this.textLabel.GetComponent<Renderer>().enabled = true;
    }

    /// <summary>
    /// Sets the content.
    /// </summary>
    /// <param name="activeSprite">The active sprite.</param>
    /// <param name="inactiveSprite">The inactive sprite.</param>
    /// <param name="scalingFactor">The scaling factor.</param>
    public void SetContent(Sprite activeSprite, Sprite inactiveSprite = null, float scalingFactor = 1.0f)
    {
        this.spriteScalingFactor = scalingFactor;
        if (this.activeSprite == null || this.inactiveSprite == null)
        {
            this.FindChildren(); 
        }
        this.arcType = MenuBehavior.MenuType.ICON;
        (this.activeSprite.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = activeSprite;
        if (inactiveSprite != null)
        {
            this.inactiveSpriteAvailable = true;
            (this.inactiveSprite.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = inactiveSprite;
            this.inactiveSprite.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            this.activeSprite.GetComponent<Renderer>().enabled = true;
        }
    }

    /// <summary>
    /// Sets the content.
    /// </summary>
    /// <param name="tex">The tex.</param>
    public void SetContent(Texture2D tex)
    {
        gameObject.GetComponent<Renderer>().material.mainTexture = tex;
    }

    /// <summary>
    /// Makes the active.
    /// </summary>
    public void MakeActive()
    {
        switch (this.arcType)
        {
            case MenuBehavior.MenuType.ICON:
                if (this.inactiveSpriteAvailable)
                {
                    this.activeSprite.GetComponent<Renderer>().enabled = true;
                    this.inactiveSprite.GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    this.activeSprite.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                break;
            case MenuBehavior.MenuType.TEXT:
                break;
            case MenuBehavior.MenuType.TEXTURE:
                break;
        }
    }

    /// <summary>
    /// Makes the inactive.
    /// </summary>
    public void MakeInactive()
    {
        switch (this.arcType)
        {
            case MenuBehavior.MenuType.ICON:
                if (this.inactiveSpriteAvailable)
                {
                    this.activeSprite.GetComponent<Renderer>().enabled = false;
                    this.inactiveSprite.GetComponent<Renderer>().enabled = true;
                }
                else
                {
                    this.activeSprite.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.6f);
                }
                break;
            case MenuBehavior.MenuType.TEXT:
                break;
            case MenuBehavior.MenuType.TEXTURE:
                break;
        }
    }

    /// <summary>
    /// Starts this instance.
    /// </summary>
    private void Start()
    {
        if (this.textLabel == null || this.activeSprite == null || this.inactiveSprite == null)
        {
            this.FindChildren();
        }

        this.gameObject.layer = 8;
		this.gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Transparent/Diffuse");

        if (this.QuadCount > 0)
        {
            this.mf = GetComponent(typeof(MeshFilter)) as MeshFilter;
            this.mesh = new Mesh();
            this.vertices = new Vector3[(this.QuadCount * 2) + 2];
            this.tris = new int[this.QuadCount * 6];
            this.normals = new Vector3[(this.QuadCount * 2) + 2];
            this.uvs = new Vector2[(this.QuadCount * 2) + 2];
            this.mf.mesh = this.mesh;
        }
        else
        {
            Debug.LogError("No Quads Created, quad count is <= 0");
        }
    }

    /// <summary>
    /// Updates this instance.
    /// Currently redraws the mesh each frame. Better would be to create the verticals once and deform after that.
    /// </summary>
    private void Update()
    {
        if (this.DrawMesh && this.QuadCount > 0)
        {
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(0, 0);

            for (int i = 0; i < this.vertices.Length; i++)
            {
                float angle = this.EndAngle + ((this.StartAngle - this.EndAngle) * (float)((Mathf.Floor(i / 2.0f) / (float)Mathf.Floor((this.vertices.Length - 1) / 2.0f))));
                angle = angle * Mathf.Deg2Rad;
                Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                point = point.normalized * (i % 2 == 0 ? this.Bottom : this.Top);
                this.vertices[i] = new Vector3(point.x, point.y, 0);

                if (point.x < min.x) {
                    min.x = point.x; 
                }
                if (point.x > max.x) { 
                    max.x = point.x; 
                }
                if (point.y < min.y) {
                    min.y = point.y;
                }
                if (point.y > max.y) { 
                    max.y = point.y; 
                }
            }

            if (max.x - min.x > max.y - min.y) {
                max.y = min.y + (max.x - min.x);
            }
            else {
                max.x = min.x + (max.y - min.y);
            }

            for (int i = 0; i < this.QuadCount; i++)
            {
                this.tris[(i * 6) + 0] = (i * 2) + 0;
                this.tris[(i * 6) + 1] = (i * 2) + 1;
                this.tris[(i * 6) + 2] = (i * 2) + 2;

                this.tris[(i * 6) + 3] = (i * 2) + 1;
                this.tris[(i * 6) + 4] = (i * 2) + 3;
                this.tris[(i * 6) + 5] = (i * 2) + 2;
            }

            for (int i = 0; i < this.vertices.Length; i++)
            {
                this.normals[i] = -Vector3.forward; 
            }
            for (int i = 0; i < this.vertices.Length; i++)
            {
                Vector2 normLocation = new Vector2(Mathf.Clamp((this.vertices[i].x - min.x) / (max.x - min.x), 0.0f, 1.0f), Mathf.Clamp((this.vertices[i].y - min.y) / (max.y - min.y), 0.0f, 1.0f));
                this.uvs[i] = normLocation;
            }

            this.mesh.vertices = this.vertices;
            this.mesh.triangles = this.tris;
            this.mesh.normals = this.normals;
            this.mesh.uv = this.uvs;

            float contentAngle = this.StartAngle + ((this.EndAngle - this.StartAngle) / 2.0f);
            contentAngle = contentAngle * Mathf.Deg2Rad;
            float contentDist = this.Bottom + ((this.Top - this.Bottom) * 0.5f);
            Vector2 contentPoint = new Vector2(Mathf.Cos(contentAngle), Mathf.Sin(contentAngle));
            contentPoint = contentPoint.normalized * contentDist;

            switch (this.arcType)
            {
                case MenuBehavior.MenuType.ICON:
                    this.activeSprite.transform.localPosition = new Vector3(contentPoint.x, contentPoint.y, -1f);
                    this.inactiveSprite.transform.localPosition = new Vector3(contentPoint.x, contentPoint.y, -1f);
                    this.activeSprite.transform.localScale = new Vector3(
                        1.0f * this.ContentScaleFactor * this.spriteScalingFactor,
                        1.0f * this.ContentScaleFactor * this.spriteScalingFactor,
                        1.0f);
                    this.inactiveSprite.transform.localScale = new Vector3(
                        1.0f * this.ContentScaleFactor * this.spriteScalingFactor,
                        1.0f * this.ContentScaleFactor * this.spriteScalingFactor,
                        1.0f);
                    break;
                case MenuBehavior.MenuType.TEXT:
                    this.textLabel.transform.localPosition = new Vector3(contentPoint.x, contentPoint.y, -1f);
                    this.textLabel.transform.localScale = new Vector3(
                        1.0f * this.ContentScaleFactor,
                        1.0f * this.ContentScaleFactor,
                        1.0f);
                    break;
            }
        }
    }

    /// <summary>
    /// Finds the children.
    /// </summary>
    private void FindChildren()
    {
        foreach (Transform child in gameObject.transform)
        {
            switch (child.name)
            {
                case "activeSprite":
                    this.activeSprite = child.gameObject;
                    break;
                case "inactiveSprite":
                    this.inactiveSprite = child.gameObject;
                    break;
                case "textLabel":
                    this.textLabel = child.gameObject;
                    break;
                default:
                    Debug.LogError("Unknown child name: " + child.name);
                    break;
            }
        }

        this.activeSprite.GetComponent<Renderer>().enabled = false;
        this.inactiveSprite.GetComponent<Renderer>().enabled = false;
        this.textLabel.GetComponent<Renderer>().enabled = false;
    }
}
