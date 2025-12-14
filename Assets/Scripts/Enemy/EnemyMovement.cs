using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;
    Rigidbody2D rb;

    Vector2 knockbackVelocity;
    float knockbackDuration;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody2D>();

        // Enforce Physics 2D settings for Top-Down view
        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Better for moving objects

        // AUTOMATIC FIX: Apply Zero Friction Material so enemies don't get stuck on walls
        PhysicsMaterial2D slipperyMaterial = new PhysicsMaterial2D("EnemySlippery");
        slipperyMaterial.friction = 0f;
        slipperyMaterial.bounciness = 0f;
        rb.sharedMaterial = slipperyMaterial;
        
        // Also apply to all attached Colliders to be safe
        Collider2D[] cols = GetComponents<Collider2D>();
        foreach(var c in cols) c.sharedMaterial = slipperyMaterial;
    }

    void FixedUpdate()
    {
        // Physics calculations should be done in FixedUpdate
        if (knockbackDuration > 0)
        {
            // Apply knockback
            rb.MovePosition(rb.position + knockbackVelocity * Time.fixedDeltaTime);
            knockbackDuration -= Time.fixedDeltaTime;
        }
        else if (player != null)
        {
            // Move towards player using Physics
            // Vector2.MoveTowards returns a point, so we can use it directly with MovePosition
            Vector2 targetPosition = Vector2.MoveTowards(rb.position, player.position, enemy.currentMoveSpeed * Time.fixedDeltaTime);
            rb.MovePosition(targetPosition);
        }
    }

    // This is meant to be called from other scripts to create knockback.
    public void Knockback(Vector2 velocity, float duration)
    {
        // Ignore the knockback if the duration is greater than 0.
        if (knockbackDuration > 0) return;

        // Begins the knockback.
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
