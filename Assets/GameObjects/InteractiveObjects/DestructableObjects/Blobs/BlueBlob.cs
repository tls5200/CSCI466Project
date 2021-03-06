﻿// written by: Thomas Stewart
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;

public class PullOthers : BlobBehaviour
{
    private float pullSpeed;

    public PullOthers(float pullSpeed)
    {
        this.pullSpeed = pullSpeed;
    }

    public override bool Combine(BlobBehaviour other)
    {
        if (other.GetType() == typeof(PullOthers))
        {
            PullOthers theOther = (PullOthers)other;

            float amountThis = magnitude / (magnitude + other.magnitude);

            magnitude += other.magnitude;
            pullSpeed = pullSpeed * amountThis + theOther.pullSpeed * (1 - amountThis);

            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Pulls each other Blob in the current Level towards thisBlob baised on pullSpeed 
    /// </summary>
    /// <param name="thisBlob">The Blob this behavior is attached to.</param>
    public override void Update(Blob thisBlob)
    {
        foreach (DestructableObject item in Level.current.destructables)
        {
            if ((item.GetType().IsSubclassOf(typeof(Blob)) || item.GetType() == typeof(Blob)) && item != thisBlob)
            {
                item.MoveTowards(thisBlob, pullSpeed * magnitude * thisBlob.difficultyModifier / thisBlob.DistanceFrom(item));
            }
        }
    }
}

public class BlueBlob : Blob
{
    public float pullSpeed = 0.05f;

    protected override void StartBlob()
    {
        color = new Color(0, 0, 1, color.a);

        behaviors.AddFirst(new PullOthers(pullSpeed));
    }
}
