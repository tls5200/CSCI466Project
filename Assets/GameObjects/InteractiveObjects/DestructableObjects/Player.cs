﻿// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Players are DestructableObjects that are the user's ingame representation and is controlled by the user's inputs
/// Players can always shoot a LazerShot but can also pickup and use Items
/// </summary>
public class Player : DestructableObject
{
    //how long, in seconds, the Players can't be damaged at teh begining of the Level
    public const float INVULNERABLE_SECS = 2.0f;

    protected const float SHOT_POINTS = -1f;
    protected const float DAMAGE_POINT_MULTIPLIER = -10f;

    public byte playerNum = 0;
    public float accelerationPerSec = 20f;
    public float turnSpeed = 100f;

    private float shootTimer = 0;
    public float shootTimeSecs = 0.5f;
    public float shotSpeed = 15f;

    protected PlayerControls input;

    private Item[] theItems = new Item[PlayerInput.NUM_ITEMS];
    public Item[] items
    {
        get
        {
            return theItems;
        }
    }

    protected override void StartDestructableObject()
    {
        if (team < 1)
        {
            Debug.Log("Player had a negative team value of: " + team + ". Setting team to 1.");
            team = 1;
        }

        switch (playerNum)
        {
            case 0:
                color = Color.white;
                break;
            case 1:
                color = Color.cyan;
                break;
            case 2:
                color = Color.yellow;
                break;
            case 3:
                color = Color.magenta;
                break;
            default:
                color = Color.grey;
                Debug.Log("Player number not between 0-3, playerNum: " + playerNum);
                break;
        }
    }

    protected override void UpdateDestructableObject()
    {
        input = Controls.Get().players[playerNum];

        Vector2 move = new Vector2();

        //find which direction to move this Player
        move.y += input.forward;
        move.y -= input.backward;
        move.x -= input.straifL;
        move.x += input.straifR;
        move.Normalize();

        //find out how much to more this Player in the direction
        move *= accelerationPerSec * level.secsPerUpdate;

        //move this Player relative to its current angle
        if (input.relativeMovement)
            ModifyVelocityRelative(move);

        //move this Player relative to the screen
        else
            ModifyVelocityAbsolute(move);

        //turn this Player left or right
        if (input.turns)
        {
            angularVelocity += turnSpeed * input.turnL;
            angularVelocity -= turnSpeed * input.turnR;
        }
        //point this Player in a direction on the screen
        else
        {
            double toTurn = new Vector2(input.turnL - input.turnR, input.turnUp - input.turnDown).GetAngle();

            if (!double.IsNaN(toTurn))
                angle = (float)toTurn;
        }

        if (shootTimer > 0)
        {
            shootTimer--;
        }
        else if (input.shoot)
        {
            //reset shootTimer
            shootTimer = shootTimeSecs * level.updatesPerSec;

            //create a new lazerShot infront of this Player
            SpaceObject shot = level.CreateObject("LazerShotPF", new Vector2(0, 2).Rotate(angle) + position, angle);
            shot.velocity = velocity;
            shot.MoveForward(shotSpeed);
            shot.color = color;
            shot.team = team;

            level.score += SHOT_POINTS;
        }

        //update Items held by this Player and check to see if any were dropped
        for (int i = 0; i < theItems.Length; i++)
        {
            if (theItems[i] != null)
            {
                theItems[i].Holding(input.items(i));
                if (input.pickupDrop && input.items(i))
                    theItems[i].Drop();
            }
        }
    }

    protected override void DestroyDestructableObject()
    {
        //when this Player is destroyed, drop all of its Items
        for (int i = 0; i < theItems.Length; i++)
        {
            if (theItems[i] != null)
            {
                theItems[i].Drop();
            }
        }
    }

    protected override void DestructableObjectCollision(DestructableObject other, Collision2D collision)
    {
        
    }

    protected override void IndestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
        
    }

    protected override void NonInteractiveObjectCollision(NonInteractiveObject other)
    {
        //if this Player is colliding with an Item then check to see if it should be picked up
        if (input.pickupDrop && other.GetType().IsSubclassOf(typeof(Item)))
        {
            Item item = (Item)other;
            for (int i = 0; i < PlayerInput.NUM_ITEMS; i++)
            {
                if (input.items(i))
                {
                    item.Pickup(this, i);
                    return;
                }
            }
            item.Pickup(this);
        }
    }

    protected override void PlayerCollision(Player other, Collision2D collision)
    {
        
    }

    public override void DamageThis(float damage)
    {
        //don't let this take damage during the begining seconds of the Level
        if (level.duration.Seconds > INVULNERABLE_SECS)
        {
            base.DamageThis(damage);

            if (damage > armor)
                level.score += (damage - armor) * DAMAGE_POINT_MULTIPLIER;
        }
    }

    public Player Clone()
    {
        Player clone = (Player)this.MemberwiseClone();
        clone.theItems = new Item[PlayerInput.NUM_ITEMS];
        for (int i = 0; i < theItems.Length; i++)
        {
            clone.theItems[i] = theItems[i];
        }
        return clone;
    }
}
