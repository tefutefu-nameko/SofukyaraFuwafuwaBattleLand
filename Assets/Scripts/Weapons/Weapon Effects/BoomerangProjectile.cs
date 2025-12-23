using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    [Header("Boomerang Settings")]
    public float maxDistance = 10f; // Distance before returning
    public float returnSpeedMultiplier = 1.5f; // Speed when returning
    public float acceleration = 5f; // Deceleration/Acceleration for turning

    [Header("Stun Settings")]
    [Range(0f, 1f)]
    public float stunChance = 0.2f;
    public float stunDuration = 1f;

    private Vector3 startPosition;
    private bool returning = false;
    private float currentSpeed;

    protected override void Start()
    {
        base.Start();
        startPosition = transform.position;
        currentSpeed = weapon.GetStats().speed;

        // If simple Rigidbody movement is used in base, we might need to override or adjust
        // But base.Start sets velocity for Dynamic bodies.
        // We probably want Kinematic for controlled boomerang movement or manual velocity control.
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.velocity = transform.right * currentSpeed;
        }
    }

    protected override void FixedUpdate()
    {
        // If kinematic, we drive movement.
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            float step = currentSpeed * Time.fixedDeltaTime;
            
            if (!returning)
            {
                // Move forward
                transform.position += transform.right * step;
                
                // Check distance
                if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
                {
                    returning = true;
                }
            }
            else
            {
                // Return to owner
                if (owner != null)
                {
                    Vector3 direction = (owner.transform.position - transform.position).normalized;
                    transform.position += direction * step * returnSpeedMultiplier;

                    // Rotate to face owner (optional, for visual)
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angle), 360f * Time.fixedDeltaTime);

                    // Destroy if returned to owner
                    if (Vector3.Distance(transform.position, owner.transform.position) < 0.5f)
                    {
                        Destroy(gameObject);
                    }
                }
                else
                {
                    Destroy(gameObject); // Owner dead/gone
                }
            }
            
            rb.MovePosition(transform.position);
            transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Custom collision logic to add Stun
        EnemyStats es = other.GetComponent<EnemyStats>();
        
        if (es)
        {
            // Apply Stun Chance
            if (Random.value < stunChance)
            {
                es.ApplyStun(stunDuration);
            }
        }

        // Call base to handle damage and piercing reduction
        base.OnTriggerEnter2D(other);
    }
}
