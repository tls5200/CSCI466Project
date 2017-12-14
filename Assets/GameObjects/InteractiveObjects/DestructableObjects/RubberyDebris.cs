// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// RubberyDebris is a DestructableObject that perfectly bounces off any InteractiveObject (not losing any speed)
/// and damages any DestructableObject not on the same team that it collides with
/// </summary>
public class RubberyDebris : DestructableObject
{
    public float damage = 5;
    private Vector2 previousVelocity;

    protected override void DestroyDestructableObject()
    {

    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.DamageThis(damage * difficultyModifier);
        }

        Bounce(collision);
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        Bounce(collision);
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
        
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
        if (other.team != team)
        {
            other.DamageThis(damage * difficultyModifier);
        }

        Bounce(collision);
    }

    protected override void StartDestructableObject()
    {
        
    }

    protected override void UpdateDestructableObject()
    {
        previousVelocity = velocity;
    }

    private void Bounce(Collision2D collision)
    {
        // Normal
        Vector2 N = collision.contacts[0].normal;

        //Direction
        Vector2 V = previousVelocity.normalized;

        // Reflection
        Vector2 R = Vector2.Reflect(V, N).normalized;

        // Assign normalized reflection with the constant speed
        velocity = new Vector2(R.x, R.y) * previousVelocity.magnitude;
    }
}
