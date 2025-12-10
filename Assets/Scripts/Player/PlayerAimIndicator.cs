using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimIndicator : MonoBehaviour
{
    // References
    PlayerMovement pm;
    GameStateService gameStateService;

    [Header("Settings")]
    public float distanceFromPlayer = 1.0f; // Distance from the center of the player
    public float rotationOffset = 0f; // Degrees to rotate the sprite to make it face outwards
    
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        if (pm == null)
        {
            Debug.LogError("PlayerAimIndicator must be a child of an object with PlayerMovement script.");
            enabled = false;
        }

        gameStateService = FindObjectOfType<GameStateService>();
    }

    void Update()
    {
        // Check for Game Over using GameStateService if available, otherwise fallback (or just run)
        if (gameStateService != null && gameStateService.IsGameOver)
        {
            gameObject.SetActive(false);
            return;
        }

        if (pm.mouseDir != Vector2.zero)
        {
            UpdatePositionAndRotation();
        }
    }

    void UpdatePositionAndRotation()
    {
        // 1. Calculate angle from mouse direction
        float angle = Mathf.Atan2(pm.mouseDir.y, pm.mouseDir.x) * Mathf.Rad2Deg;

        // 2. Rotate to face outwards with offset
        transform.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);

        // 3. Position: Moves in circle
        transform.localPosition = pm.mouseDir * distanceFromPlayer;
    }
}
