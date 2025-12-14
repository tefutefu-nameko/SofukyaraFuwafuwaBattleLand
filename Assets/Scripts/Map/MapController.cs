using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public GameObject player;

    [Header("Finite Map Settings")]
    public string mapResourceName = "AiMapImage";
    public float boundaryThickness = 2f;
    public float mapScale = 5f; // Added scale
    
    [Tooltip("Assign the Map object here if you placed it manually in the scene.")]
    public GameObject manualMapObject;

    private GameObject mapObject;
    private SpriteRenderer mapRenderer;

    void Start()
    {
        CleanupOldMap();
        InitializeMap();
    }

    void CleanupOldMap()
    {
        // Disable old Grid objects that might contain Tilemaps
        // Only disable if they are NOT our manual map object
        Grid[] grids = FindObjectsOfType<Grid>();
        foreach (var grid in grids)
        {
            if (grid.gameObject != this.gameObject && (manualMapObject == null || grid.gameObject != manualMapObject))
            {
                grid.gameObject.SetActive(false);
                Debug.Log($"[MapController] Disabled old map object: {grid.gameObject.name}");
            }
        }
    }

    void InitializeMap()
    {
        Debug.Log("[MapController] Initializing Finite Map...");
        
        if (manualMapObject != null)
        {
             // Use the manually placed map
             mapObject = manualMapObject;
             mapRenderer = mapObject.GetComponent<SpriteRenderer>();
             if (mapRenderer == null)
             {
                 Debug.LogError("[MapController] Manual Map Object must have a SpriteRenderer!");
                 return;
             }
             
             Debug.Log($"[MapController] Using Manual Map Object: {mapObject.name}");
             // Note: We respect the manual object's scale. 
             // If you want to force override scale, uncomment below, but usually manual means manual control.
             // mapObject.transform.localScale = Vector3.one * mapScale; 
        }
        else
        {
            // Fallback: Load from Resources (Dynamic instantiation)
            
            // Load the map sprite from Resources
            Sprite mapSprite = Resources.Load<Sprite>(mapResourceName);
            if (mapSprite == null)
            {
                Debug.LogError($"[MapController] Failed to load map sprite from Resources/{mapResourceName}. Please ensure the file exists in a Resources folder.");
                return;
            }

            // Create the map object
            mapObject = new GameObject("FiniteMapController_Map");
            mapObject.transform.position = Vector3.zero;
            mapObject.transform.SetParent(this.transform); // Set as child of MapController
            mapObject.transform.localScale = Vector3.one * mapScale; // Apply scale

            // Add SpriteRenderer
            mapRenderer = mapObject.AddComponent<SpriteRenderer>();
            mapRenderer.sprite = mapSprite;
            // Set sorting order to a low value but likely visible. 
            // If the old map was removed, -10 is a good standard for background.
            mapRenderer.sortingOrder = -10; 
            
            Debug.Log($"[MapController] Created Dynamic Map Object with scale: {mapScale}");
        }

        // Create Boundaries using the Renderer's bounds (which accounts for scale)
        CreateBoundaries(mapRenderer.bounds);
        
        Debug.Log($"[MapController] Map initialized with size: {mapRenderer.bounds.size}");
    }

    void CreateBoundaries(Bounds bounds)
    {
        // Top
        CreateBoundary("TopBoundary",
            new Vector3(bounds.center.x, bounds.max.y + boundaryThickness / 2, 0),
            new Vector2(bounds.size.x + boundaryThickness * 2, boundaryThickness));

        // Bottom
        CreateBoundary("BottomBoundary",
            new Vector3(bounds.center.x, bounds.min.y - boundaryThickness / 2, 0),
            new Vector2(bounds.size.x + boundaryThickness * 2, boundaryThickness));

        // Left
        CreateBoundary("LeftBoundary",
            new Vector3(bounds.min.x - boundaryThickness / 2, bounds.center.y, 0),
            new Vector2(boundaryThickness, bounds.size.y));

        // Right
        CreateBoundary("RightBoundary",
            new Vector3(bounds.max.x + boundaryThickness / 2, bounds.center.y, 0),
            new Vector2(boundaryThickness, bounds.size.y));
    }

    void CreateBoundary(string name, Vector3 position, Vector2 size)
    {
        GameObject boundary = new GameObject(name);
        boundary.transform.position = position;
        // Parent to MapController directly, not the scaled mapObject, so size/scale logic is simpler
        boundary.transform.SetParent(this.transform);

        BoxCollider2D collider = boundary.AddComponent<BoxCollider2D>();
        collider.size = size;
        
        // Static collider by default (no Rigidbody) will block the player.
    }
}
