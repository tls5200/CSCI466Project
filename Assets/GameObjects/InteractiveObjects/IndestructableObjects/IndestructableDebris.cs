// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// IndestructableDebris is an IndestructableObject that moves around only baised on physics and 
/// damages any DestructableObject it collides with baised on the collision speed
/// </summary>
public class IndestructableDebris : IndestructableObject
{
    public float damageMultiplier = 1f;
    public float minDamageSpeed = 5f;

    protected override void DestroyIndestructableObject()
    {
        
    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        //if the collision speed is larger than the minimum, deal damage to the DestructableObject
        float damageSpeed = collision.relativeVelocity.magnitude - minDamageSpeed;
        if (damageSpeed > 0)
        {
            other.DamageThis(damageSpeed * damageMultiplier * difficultyModifier);
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
        //if the collision speed is larger than the minimum, deal damage to the Player
        float damageSpeed = collision.relativeVelocity.magnitude - minDamageSpeed;
        if (damageSpeed > 0)
        {
            other.DamageThis(damageSpeed * damageMultiplier * difficultyModifier);
        }
    }

    protected override void StartIndestructableObject()
    {
        
    }

    protected override void UpdateIndestructableObject()
    {
        
    }
}
