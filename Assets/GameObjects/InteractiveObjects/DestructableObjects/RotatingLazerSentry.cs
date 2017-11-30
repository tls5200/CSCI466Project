﻿using UnityEngine;
using System.Collections;

public class RotatingLazerSentry : DestructableObject
{
    public float damage = 1.1f;
    public float extendSpeed = 0.2f;
    public float maxLength = 10f;
    public float turnSpeed = 4f;
    public Color lazerColor = Color.green;

    private DestructableObject target = null;
    private Lazer lazer = null;

    protected override void destroyDestructableObject()
    {
       
    }

    protected override void destructableObjectCollision(DestructableObject other, Collision2D collision)
    {
       
    }

    protected override void indestructableObjectCollision(IndestructableObject other, Collision2D collision)
    {
       
    }

    protected override void nonInteractiveObjectCollision(NonInteractiveObject other)
    {
       
    }

    protected override void playerCollision(Player other, Collision2D collision)
    {
       
    }

    protected override void startDestructableObject()
    {
       
    }

    protected override void updateDestructableObject()
    {
        if (target != null && distanceFrom(target) > maxLength * difficultyModifier)
        {
            target = null;
        }

        if (target == null)
        {
            target = closestObjectInDirection(level.getTypes(true, true, false, false), angle, false) as DestructableObject;

            if (target != null && distanceFrom(target) > maxLength * difficultyModifier)
            {
                target = null;
            }
        }

        if (target == null)
        {
            angle += turnSpeed;

            if (lazer != null)
            {
                lazer.destroyThis();
                lazer = null;
            }
        }
        else
        {
            angularVelocity = 0f;
            turnTowards(target, Mathf.Abs(turnSpeed));

            if (lazer == null)
            {
                lazer = (Lazer)level.createObject("LazerPF", position, angle);
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
