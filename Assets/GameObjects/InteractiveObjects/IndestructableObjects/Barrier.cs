// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// Barrier is an IndestructableObject that attaches to a SpaceObject and deflects/absorbs things 
/// to protect what it is attached to
/// </summary>
public class Barrier : IndestructableObject
{
    public float width = 8f;

    public Vector2 attachPoint = new Vector2(0, 2);
    public float attachAngle = 0;
    public SpaceObject attachedTo;

    private float originalWidth;
    public float previousWidth = 0;

    protected override void DestroyIndestructableObject()
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

    
    protected override void StartIndestructableObject()
    {
        //find and save the original width of this 
        angle = 0;
        originalWidth = gameObject.GetComponent<Collider2D>().bounds.size.x / scale.x;
        angle = attachedTo.angle + attachAngle;
    }

    protected override void UpdateIndestructableObject()
    {

        if (attachedTo != null && attachedTo.active)
        {
            //change the scale to make it the correct width if it is not
            if (originalWidth * scale.x != width)
            {
                Vector2 newScale = scale;
                newScale.x = width / originalWidth;
                scale = newScale;
            }

            //keep it attached to the correct position relative to what it is attached to
            angle = attachedTo.angle + attachAngle;
            Vector2 toRotate = attachPoint;
            position = attachedTo.position + toRotate.Rotate(angle);

            //make sure physics is not moving this, would cause it to be attached incorrectly
            velocity = Vector2.zero;
            angularVelocity = 0;
        }
        //if this is not attached to anything, destroy it
        else
        {
            DestroyThis();
        }
    }
}
