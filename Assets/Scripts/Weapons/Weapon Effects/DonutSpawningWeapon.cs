using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns projectiles at random locations within the weapon's area.
/// Used for the Donut Uprising evolution.
/// </summary>
public class DonutSpawningWeapon : ProjectileWeapon
{
    protected override float GetSpawnAngle()
    {
        // Random rotation for the donut
        return Random.Range(0f, 360f);
    }

    protected override Vector2 GetSpawnOffset(float spawnAngle = 0)
    {
        // Spawn at a random point inside the area
        // scale the area by the weapon's Area stat
        float radius = currentStats.area;
        return Random.insideUnitCircle * radius;
    }
}
