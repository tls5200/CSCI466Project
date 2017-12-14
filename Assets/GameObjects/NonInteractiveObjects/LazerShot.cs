// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// LazerShot is a NonInteractiveObject that moves forward with its initial speed, unless it is 
/// modifed by something else, until it collides with an InteractiveObject or its life timer runs
/// out, which will cause it to be destroyed. When it collides with an enemy DestructableObject
/// it will damage it.
/// </summary>
public class LazerShot : NonInteractiveObject
{
    public float damage = 10;
    private int timeAlive = 0;
    public float timeToLiveSecs = 2;

    protected override void StartNonInteractiveObject()
    {
        
    }

    public void ResetTimeAlive()
    {
        timeAlive = 0;
    }

    /// <summary>
    /// If the timeToLiveSecs is set to a positive number and this has been alive for
    /// longer than that many seconds, destroy this
    /// </summary>
    protected override void UpdateNonInteractiveObject()
    {
        if (timeToLiveSecs > 0)
        {
            timeAlive++;
            if (timeAlive > timeToLiveSecs * level.updatesPerSec)
            {
                DestroyThis();
            }
        }
    }

    protected override void DestroyNonInteractiveObject()
    {
        
    }

    protected override void PlayerCollision(Player other)
    {
        if (team != other.team)
        {
            other.DamageThis(damage);
        }
        DestroyThis();
    }

    protected override void DestructableObjectCollision(DestructableObject other)
    {
        if (team != other.team)
        {
            other.DamageThis(damage);
        }
        DestroyThis();
    }

    protected override void IndestructableObjectCollision(IndestructableObject other)
    {
        DestroyThis();
    }
}
