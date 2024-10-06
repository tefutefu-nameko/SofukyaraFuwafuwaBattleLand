using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("This will be replaced by the WeaponData class")]

public class KashiwamochiBehaviour : ProjectileWeaponBehaviour
{


    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime;    //Set the movement of the knife
    }
}