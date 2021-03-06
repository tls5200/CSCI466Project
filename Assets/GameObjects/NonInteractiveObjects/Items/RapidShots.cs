﻿// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// RapidShots is an Item that allow its holder to shoot small, weak 
/// LazerShots in quick succession
/// </summary>
public class RapidShots : Item
{
    protected const float USE_POINTS = -0.1f;

    public float shotLifeSecs = 3f;
    public float shotSpeed = 20f;
    public float shotDamage = 2f;
    public Vector2 offset = new Vector2(0, 2f);

    public float shotCooldownSecs = 0.2f;
    private int shotCooldown = 0;

    protected override void DropItem()
    {
        
    }

    protected override void HoldingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //update the time before the next shot
        if (shotCooldown > 0)
        {
            shotCooldown--;
        }
        //if the holder is holding down this Item's key and it is time to shoot another shot
        //then create a LazerShot infront of the holder
        else if (use)
        {
            LazerShot shot = (LazerShot)level.CreateObject("LazerShotPF", holder.position + offset.Rotate(holder.angle), holder.angle);
            shot.maxSpeed = shotSpeed + holder.velocity.magnitude;
            shot.velocity = holder.velocity;
            shot.MoveForward(shotSpeed);
            shot.damage = shotDamage;
            shot.timeToLiveSecs = shotLifeSecs;
            shot.color = color;
            shot.team = holder.team;

            //reset the time until the next shot
            shotCooldown = (int)(shotCooldownSecs * level.updatesPerSec);

            level.score += USE_POINTS;
        }
    }

    protected override void PickupItem()
    {
        
    }
}
