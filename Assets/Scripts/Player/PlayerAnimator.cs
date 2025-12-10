using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //References
    Animator am;
    PlayerMovement pm;
    SpriteRenderer sr;

    void Start()
    {
        am = GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Always update sprite direction based on mouse position
        SpriteDirectionChecker();

        if (pm.moveDir.x != 0 || pm.moveDir.y != 0)
        {
            // Moving animation logic if needed, currently just direction checks are here but split later if needed
        }
        else
        {
            am.SetBool("MoveLeft", false);
            am.SetBool("MoveRight", false);
        }
    }
    void SpriteDirectionChecker()
    {
        if (pm.mouseDir.x < 0)
        {
            am.SetBool("MoveLeft", true);
            am.SetBool("MoveRight", false);
            am.SetBool("lastHorizontalVectorIsRight", false);
        }
        else
        {
            am.SetBool("MoveRight", true);
            am.SetBool("MoveLeft", false);
            am.SetBool("lastHorizontalVectorIsRight", true);
        }
    }

    public void SetAnimator(RuntimeAnimatorController c)
    {
        if (!am) am = GetComponent<Animator>();
        am.runtimeAnimatorController = c;
    }

}
