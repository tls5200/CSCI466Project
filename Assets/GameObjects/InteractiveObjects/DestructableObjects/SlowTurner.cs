// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// SlowTurner is a DestructableObject that slowly turns towars its target, always moving forward.
/// The target is assigned to the closest enemy DestructableObject. SlowTurners damage any enemy DestructableObject
/// they collide with. 
/// </summary>
public class SlowTurner : DestructableObject
{
    public float turnSpeed = 0.01f;
    public float damage = 10f;
    public float acceleration = 0.1f;
    private SpaceObject target;

    protected override void DestroyDestructableObject()
    {

    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.DamageThis(damage);
        }
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.DamageThis(damage);
        }
    }

    protected override void StartDestructableObject()
    {
        
    }

    protected override void UpdateDestructableObject()
    {
        //find a target if there is not currently one
        if (target == null || !target.active)
        {
            target = ClosestObject(level.GetTypes(true, true, false, false));
        }
        //if there is a target, turn towards it
        else
        {
            TurnTowards(target, turnSpeed * difficultyModifier);
        }

        MoveForward(acceleration);
    }
}
