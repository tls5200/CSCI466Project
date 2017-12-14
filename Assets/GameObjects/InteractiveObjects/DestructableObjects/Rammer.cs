// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Rammer is a DestructableObject that finds the closest enemy DestructableObject 
/// then tries to ram into it. It damages any enemy DestructableObjects it collides with. 
/// </summary>
public class Rammer : DestructableObject
{
    public float damage = 15;

    public SpaceObject target;
    private float ramTime = 0;
    public float overRunSecs = 2;

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
        //find new target if there is not currently one
        if (target == null || !target.active)
        {
            target = ClosestObject(level.GetTypes(true, true, false, false), false);
        }
        //continue moving forward until ramTime runs out
        else if (ramTime > 0)
        {
            ramTime--;
        }
        //try to ram target
        else
        {
            //find position to ram baised on this and target's current position, ram speed and target's current velocity
            Vector3 temp = IntersectPosTime(target, maxSpeed);
            if (temp.z < 0)
            {
                //remove target if it cannot be rammed
                target = null;
            }
            else
            {
                //set how long to move towards target then move towards target
                ramTime = (temp.z + overRunSecs) * level.updatesPerSec;
                TurnTowards(temp);
                angularVelocity = 0;
                velocity = Vector2.zero;
                MoveTowards(temp, maxSpeed);
            }
        }
    }

    /// <summary>
    /// takes less damage baised on difficulty
    /// </summary>
    /// <param name="damage"></param>
    public override void DamageThis(float damage)
    {
        float temp = armor;
        armor *= difficultyModifier;

        base.DamageThis(damage);

        armor = temp;
    }
}
