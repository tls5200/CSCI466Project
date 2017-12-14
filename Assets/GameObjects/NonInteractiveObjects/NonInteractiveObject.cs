// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Thomas Stewart

using UnityEngine;
using System.Collections;

/// <summary>
/// NonInteractiveObject is a SpaceObject that does not interact with physics.
/// Its velocity and angularVelocity are updated manually, without physics.
/// </summary>
public abstract class NonInteractiveObject : SpaceObject
{
    private Vector2 theVelocity = new Vector2(0, 0);
    private float theAngularVelocity = 0;
    private float theMass = 0;

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, the tranform is used for its position
    /// </summary>
    public override Vector2 position
    {
        get
        {
            return transform.position;
        }
        set
        {
            transform.position = new Vector3(value.x, value.y, transform.position.z);
        }
    }

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, this keeps track of its velocity itself
    /// </summary>
    public override Vector2 velocity
    {
        get
        {
            return theVelocity;
        }
        set
        {
            theVelocity = value;
        }
    }

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, the tranform is used for its angle
    /// </summary>
    public override float angle
    {
        get
        {
            return transform.rotation.eulerAngles.z;
        }
        set
        {
            Vector3 currentEuler = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(new Vector3(currentEuler.x, currentEuler.y, value));
        }
    }

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, this keeps track of its angularVelocity itself
    /// </summary>
    public override float angularVelocity
    {
        get
        {
            return theAngularVelocity;
        }

        set
        {
            theAngularVelocity = value;
        }
    }

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, its SpriteRenerer's bound is used for bounds
    /// </summary>
    public override Bounds bounds
    {
        get
        {
            return GetComponent<SpriteRenderer>().bounds;
        }
    }

    /// <summary>
    /// Since NonInteractiveObjects don't use physics, this keeps track of its mass itself
    /// </summary>
    public override float mass
    {
        get
        {
            return theMass; 
        }

        set
        {
            theMass = value;
        }
    }

    //Called shortly after this is created
    protected abstract void StartNonInteractiveObject();
    protected override void StartObject()
    {
        //if there is no level, this should not exist, so it is destoryed
        if (level == null)
        {
            Debug.Log("Destroying " + this + " since level is null when it is being created.");
            Destroy(this.gameObject);
        }
        else
        {
            StartNonInteractiveObject();

            //add this to the Level's lists of what it contains
            level.AddToGame(this);
        }
    }

    // Called every time the game is FixedUpated, 50 times a second by default
    //its position and angle are adjusted baised on its velocity and angularVelocity
    protected abstract void UpdateNonInteractiveObject();
    protected override void UpdateObject()
    {
        UpdateNonInteractiveObject();
        position += velocity * level.secsPerUpdate;
        angle += angularVelocity * level.secsPerUpdate;
    }

    // Called right before this is destroyed, removes this from the Level's lists
    protected abstract void DestroyNonInteractiveObject();
    protected override void DestroyObject()
    {
        DestroyNonInteractiveObject();
        if (level != null && level.nonInteractives != null)
        {
            level.RemoveFromGame(this);
        }
    }

    //Methods OnTriggerStay2D calls depending on what it overlapped with to catagorize the overlap
    protected abstract void PlayerCollision(Player other);
    protected abstract void DestructableObjectCollision(DestructableObject other);
    protected abstract void IndestructableObjectCollision(IndestructableObject other);

    /// <summary>
    /// Called by Unity when this GameObject's collier overlaps with with another GameObject's collider
    /// Calls another method depending on the type of object it overlapped with to catagorize it
    /// </summary>
    /// <param name="other">holds the properties of the overlap</param>
    public void OnTriggerStay2D(Collider2D other)
    {
        SpaceObject spaceObject = other.gameObject.GetComponent<SpaceObject>();

        if (spaceObject.GetType() == (typeof(Player)))
        {
            PlayerCollision((Player) spaceObject);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(DestructableObject)))
        {
            DestructableObjectCollision((DestructableObject)spaceObject);
        }
        else if (spaceObject.GetType().IsSubclassOf(typeof(IndestructableObject)))
        {
            IndestructableObjectCollision((IndestructableObject)spaceObject);
        }
    }
}
