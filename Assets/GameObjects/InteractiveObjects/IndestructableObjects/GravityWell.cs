// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// GravityWell is an IndestructableObject that pull all other InteractiveObjects that are not GravityWells
/// towards it baised on the distance between it and them. When it collides with any InteractiveObject, it pushes 
/// it away. When it collides with any DestructableObject, it deals damage to it. 
/// </summary>
class GravityWell : IndestructableObject
{
    public float scaleMultipleOfGravity = 1f/8f;
    public float gravity = 1f;
    public float damage = 10f;

    protected override void DestroyIndestructableObject()
    {

    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        //push the other away from this, as fast as it can go
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;

        other.DamageThis(damage);
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        //push the other away from this, as fast as it can go
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
            
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
        //push the other away from this, as fast as it can go
        other.velocity = -collision.relativeVelocity.normalized * other.maxSpeed;

        other.DamageThis(damage);
    }

    protected override void StartIndestructableObject()
    {
        mass = 1000 * gravity;
    }

    protected override void UpdateIndestructableObject()
    {
        //change the size of this if the gravity was changed
        if (scale.x != scaleMultipleOfGravity * gravity)
        {
            float targetScale = scaleMultipleOfGravity * gravity;
            scale = new Vector2(targetScale, targetScale);
        }

        //pull all Players towards this baised on the distance squared between them
        foreach (Player item in level.players)
        {
            Vector2 pull = gravity / (float)Math.Pow(DistanceFrom(item), 2) * -Vector2From(item).normalized;
            item.ModifyVelocityAbsolute(pull);
        }

        //pull all non-Players DestructableObjects towards this baised on the distance squared between them
        foreach (DestructableObject item in level.destructables)
        {
            Vector2 pull = gravity / (float)Math.Pow(DistanceFrom(item), 2) * -Vector2From(item).normalized;
            item.ModifyVelocityAbsolute(pull);
        }

        //pull all IndestructableObjects that are not GravityWells towards this baised on the distance squared between them
        foreach (IndestructableObject item in level.indestructables)
        {
            if (item.GetType() != typeof(GravityWell))
            {
                Vector2 pull = gravity / (float)Math.Pow(DistanceFrom(item), 2) * -Vector2From(item).normalized;
                item.ModifyVelocityAbsolute(pull);
            }
        }
    }
}
