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
/// LazerShooter is a DestructableObject that finds a target enemy DestructableObject and fires LazerShots
/// at the target every so often while trying to keep a certain distance from the target
/// </summary>
class LazerShooter : DestructableObject
{
    private SpaceObject target;

    public float damage = 15;
    public float shootTimeSecs = 0.5f;
    private float shootTimer;
    public float shotSpeed = 20;
    public float maxShotLiveSecs = 0.5f;
    public float acceleration = 0.3f;
    public float targetDistanceFrom = 10f;

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
        //wait awhile before starting to fire
        shootTimer = 4 * level.updatesPerSec;
    }

    protected override void UpdateDestructableObject()
    {
        //try to find a target if it doesn't have one
        if (target == null || !target.active)
        {
            target = ClosestObject(level.GetTypes(true, true, false, false), false);
        }
        else
        {
            shootTimer--;

            //find where this needs to shoot to hit the target given their current positions, the LazerShot's speed and the Target's position
            Vector3 intersect = IntersectPosTime(target, shotSpeed, new Vector2(0, 3).Rotate(angle) + position);

            //if the target can be hit, turn towards where this would need to shoot
            if (intersect.z >= 0)
            {
                TurnTowards(intersect);
            }

            //shoot if it is time to
            if (shootTimer <= 0)
            {
                //reset the shootTimer
                shootTimer = shootTimeSecs * level.updatesPerSec / difficultyModifier;

                //if the target cannot be hit by the LazerShot, stop targeting the target
                if (intersect.z < 0 || intersect.z > maxShotLiveSecs)
                {
                    target = null;
                }
                //create a LazerShot aimed to hit the target
                else
                {
                    LazerShot shot = (LazerShot)level.CreateObject("LazerShotPF", 
                        new Vector2(0, 3 + shotSpeed * level.secsPerUpdate).Rotate(angle) + position, angle);

                    shot.maxSpeed = shotSpeed;
                    shot.speed = shotSpeed;
                    shot.color = Color.red;
                    shot.timeToLiveSecs = (int)(maxShotLiveSecs * level.updatesPerSec + 1);
                    shot.damage = damage;
                }
            }
            else
            {
                //try to keep a certain distance from the target
                if (DistanceFrom(target) > targetDistanceFrom)
                    MoveTowards(target, acceleration);
                else
                    MoveTowards(target, -acceleration);
            }
        }
    }
}
