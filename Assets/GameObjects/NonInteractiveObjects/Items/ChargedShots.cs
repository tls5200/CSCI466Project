// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// ChargedShots is an Item that allows it's holder to create a LazerShot that can be charged
/// before being shot to increase the shot's damage and size. 
/// </summary>
public class ChargedShots : Item
{
    protected const float USE_POINTS = -0.01f;

    public float shotMinDamage = 10f;
    public float shotMaxDamage = 100f;
    public float shotSpeed = 20f;
    public float shotLifeSecs = 4f;
    public Vector2 offset = new Vector2(0, 1f);
    public Vector2 shotScaleStart = new Vector2(0.05f, 0.5f);

    public float toFullChargeSecs = 5f;
    private float damageChargeSpeed;

    private LazerShot chargingShot;

    //when dropped, destroy any shot that is being charged. 
    protected override void DropItem()
    {
        if (chargingShot != null)
        {
            chargingShot.DestroyThis();
            chargingShot = null;
        }
    }

    /// <summary>
    /// To be called when the shot is being charged. Update's the shots position relative 
    /// to the holder and charges it if it is not fully charged already.  
    /// </summary>
    private void UpdateChargingShot()
    {
        if (chargingShot != null)
        {
            //update reletive to user
            chargingShot.position = holder.position + offset.Rotate(holder.angle);
            chargingShot.angle = holder.angle;
            chargingShot.ResetTimeAlive();

            //charge the shot
            chargingShot.damage += damageChargeSpeed;
            if (chargingShot.damage > shotMaxDamage)
            {
                chargingShot.damage = shotMaxDamage;
            }
            chargingShot.scale = shotScaleStart * Mathf.Sqrt(chargingShot.damage / shotMinDamage);

            level.score += USE_POINTS;
        }
    }

    protected override void HoldingItem(bool use, bool startUse, bool endUse, bool doubleUse)
    {
        //When the holder first presses this Item's key, create a LazerShot to charge
        if (startUse)
        {
            if (chargingShot != null)
                chargingShot.DestroyThis();

            chargingShot = (LazerShot)level.CreateObject("LazerShotPF");
            chargingShot.team = holder.team;
            chargingShot.color = color;
            chargingShot.timeToLiveSecs = shotLifeSecs;
            chargingShot.damage = shotMinDamage;
            UpdateChargingShot();
        }
        //When the holder holds down this Item's key, update the LazerShot
        else if (use)
        {
            UpdateChargingShot();
        }

        //When the holder release's this Item's key, shoot the LazerShot
        if (endUse && chargingShot != null)
        {
            chargingShot.velocity = holder.velocity;
            chargingShot.MoveForward(shotSpeed);
            chargingShot = null;
        }

    }

    protected override void PickupItem()
    {
        //calculate the charge speed
        damageChargeSpeed = (shotMaxDamage - shotMinDamage) / (toFullChargeSecs * level.updatesPerSec);
    }
}
