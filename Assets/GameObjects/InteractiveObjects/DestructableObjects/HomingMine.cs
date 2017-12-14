// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// HomingMines are DestructableObjects that move towards enemy DestructableObjects that
/// are close enough to it. When it collides with a DestructableObject, it deals damage to it. 
/// </summary>
public class HomingMine : DestructableObject
{
    public float acceleration = 0.2f;
    public float targetAngularVelocity = 3;
    public float angularAcceleration = 0.1f;
    public float targetFindProximity = 6f;
    public float targetLossProximity = 12f;
    public SpaceObject target;
    public float damage = 10f;

    protected override void DestroyDestructableObject()
    {

    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        other.DamageThis(damage);
        DestroyThis();
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        DestroyThis();
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
        other.DamageThis(damage);
        DestroyThis();
    }

    protected override void StartDestructableObject()
    {
        
    }

    protected override void UpdateDestructableObject()
    {
        //if the target is too far away, make it no longer the target
        if (target != null && DistanceFrom(target) > targetLossProximity)
            target = null;

        if (target == null || !target.active)
        {
            //set the target to the closest enemy DestructableObject
            target = ClosestObject(level.GetTypes(true, true, false, false), false);

            //if the target is too far away, make it no longer the target
            if (target != null && DistanceFrom(target) > targetFindProximity)
                target = null;
        }

        //if there is no target, slow down to a stop
        if (target == null || !target.active) 
        {
            if (speed > acceleration)
                speed = speed - acceleration;
            else
                speed = 0;

            if (angularVelocity > 0 && angularVelocity > angularAcceleration)
                angularVelocity = angularVelocity - angularAcceleration;
            else if (angularVelocity < 0 && angularVelocity < -angularAcceleration)
                angularVelocity = angularVelocity + angularAcceleration;    
            else
                angularVelocity = 0;
        }
        //if there is a target, accelerate towards it
        else 
        {
            if (angularVelocity > targetAngularVelocity)
            {
                if (angularVelocity - angularAcceleration < targetAngularVelocity)
                    angularVelocity = targetAngularVelocity;
                else
                    angularVelocity -= angularAcceleration;
            }
            else if (angularVelocity < targetAngularVelocity)
            {
                if (angularVelocity + angularAcceleration > targetAngularVelocity)
                    angularVelocity = targetAngularVelocity;
                else
                    angularVelocity += angularAcceleration;
            }

            MoveTowards(target, acceleration * difficultyModifier);
        }
    }
}
