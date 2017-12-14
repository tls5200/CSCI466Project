// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// RotatingLazerSentry is a DestructableObject that rotates unil an enemy DestructableObject is
/// infront of it within a certain range that it sets as its target. It then creates a Lazer that extends towards
/// the target until the target goes out of range. 
/// </summary>
public class RotatingLazerSentry : DestructableObject
{
    public float damage = 1.1f;
    public float extendSpeed = 0.2f;
    public float maxLength = 6f;
    public float turnSpeed = 4f;
    public Color lazerColor = Color.green;

    private SpaceObject target = null;
    private Lazer lazer = null;

    protected override void DestroyDestructableObject()
    {
       
    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
       
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
       
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
       
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
       
    }

    protected override void StartDestructableObject()
    {
       
    }

    protected override void UpdateDestructableObject()
    {
        //if target is too far away, make it no longer the target
        if (target != null && DistanceFrom(target) > maxLength * difficultyModifier)
        {
            target = null;
        }

        //if there is no target, try to find one
        if (target == null)
        {
            target = ClosestObjectInDirection(level.GetTypes(true, true, false, false), angle, false);

            //make sure the target is within the correct distance
            if (target != null && DistanceFrom(target) > maxLength * difficultyModifier)
            {
                target = null;
            }
        }

        //if there is no target, turn and make sure the lazer is not made
        if (target == null)
        {
            angle += turnSpeed;

            if (lazer != null)
            {
                lazer.DestroyThis();
                lazer = null;
            }
        }
        //if there is a target, turn towards it and make sure the lazer is made
        else
        {
            angularVelocity = 0f;
            TurnTowards(target, Mathf.Abs(turnSpeed));

            if (lazer == null)
            {
                lazer = (Lazer)level.CreateObject("LazerPF", position, angle);
                lazer.attachedTo = this;
                lazer.damage = damage;
                lazer.extendSpeed = extendSpeed * difficultyModifier;
                lazer.maxLength = maxLength * difficultyModifier;
                lazer.team = team;
                lazer.color = lazerColor;
            }
        }
    }
}
