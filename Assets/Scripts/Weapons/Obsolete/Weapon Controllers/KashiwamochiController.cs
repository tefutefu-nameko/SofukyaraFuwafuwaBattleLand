using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("This will be replaced by the WeaponData class")]

public class KashiwamochiController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKashiwamochi = Instantiate(weaponData.Prefab);
        spawnedKashiwamochi.transform.position = transform.position; //Assign the position to be the same as this object which is parented to the player
        spawnedKashiwamochi.GetComponent<KashiwamochiBehaviour>().DirectionChecker(pm.lastMovedVector);   //Reference and set the direction
    }
}