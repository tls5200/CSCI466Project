// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// IndestructableObjects are InteractiveObjects which have no health and thus can't
/// be destroyed though damage (as opposed to DestructableObjects)
/// </summary>
public abstract class IndestructableObject : InteractiveObject
{
    //called shortly after this IndestructableObject is created
    protected abstract void StartIndestructableObject();
    protected override void StartInteractiveObject()
    {
        //if there is no level, this should not exist, so it is destoryed 
        if (level == null)
        {
            Debug.Log("Destroying " + this + " since level is null when it is being created.");
            Destroy(this.gameObject);
        }
        else
        {
            StartIndestructableObject();

            //add this to the Level's lists of what it contains
            level.AddToGame(this);
        }
    }

    // Called every time the game is FixedUpated, 50 times a second by default
    protected abstract void UpdateIndestructableObject();
    protected override void UpdateInteractiveObject()
    {
        UpdateIndestructableObject();
    }

    // Called right before this IndestructableObject is destroyed
    // removes this from the Level's lists
    protected abstract void DestroyIndestructableObject();
    protected override void DestroyInteractiveObject()
    {
        DestroyIndestructableObject();
        if (level != null && level.indestructables != null)
        {
            level.RemoveFromGame(this);
        }
    }

    /// <summary>
    /// Called by Unity when this GameObject collides with another GameObject
    /// Calls another method depending on the collision type to categorize the collision
    /// </summary>
    /// <param name="collision">holds the properties of the collision</param>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        SpaceObject spaceObject = collision.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType() == (typeof(Player)))
        {
            PlayerCollision((Player)spaceObject, collision);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(DestructableObject)))
        {
            DestructableObjectCollision((DestructableObject)spaceObject, collision);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(IndestructableObject)))
        {
            IndestructableObjectCollision((IndestructableObject)spaceObject, collision);
        }
    }
}
