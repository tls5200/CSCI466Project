// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// RandomTurner is a DestructableObject that moves that turns left then right for a
/// random amount of time and continually moves forward. It damages any enemy DestructableObject
/// that it collids with.
/// </summary>
public class RandomTurner : DestructableObject
{
    public float damage = 20f;
    public float acceleration = 0.4f;
    public float turnSpeed = 0.3f;
    private int nextRandTimer = 0;
    public float maxSecsInADirection = 10;
    public float minSecsInADirection = 1;
    private bool turnLeft = false;

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
        nextRandTimer--;

        //if timer is up, start turning in the opposite direction and reset the timer
        if (nextRandTimer <= 0)
        {
            turnLeft = !turnLeft;

            nextRandTimer = level.random.Next((int)(minSecsInADirection * level.updatesPerSec), (int)(maxSecsInADirection * level.updatesPerSec));
        }

        //turn left or right
        if (turnLeft)
        {
            angle = angle - turnSpeed;
        }
        else
        {
            angle = angle + turnSpeed;
        }

        MoveForward(acceleration * difficultyModifier);
    }
}
