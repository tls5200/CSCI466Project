// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level7 : Level
{
    public override int levelNumber
    {
        get
        {
            return 7;
        }
    }

    public override string levelName
    {
        get
        {
            return "Seven";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(100, 75); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 2; i++)
        {
            RedBlob current = (RedBlob)CreateObject("RedBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }
        for (int i = 0; i < 2; i++)
        {
            GreenBlob current = (GreenBlob)CreateObject("GreenBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }
        for (int i = 0; i < 2; i++)
        {
            BlueBlob current = (BlueBlob)CreateObject("BlueBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        Accelerant accelerant = (Accelerant)CreateObject("AccelerantFP");
    }

    protected override void UpdateLevel()
    {

    }

    protected override void EndLevel()
    {
        
    }
    
    /*
    protected override bool Won()
    {
        //add win conditinos here, default is when all enimes die    

        return win;
    }

    
    protected override bool Lost()
    {
        //add loss conditions here, if player dies then its always loss

        return false;
    }
    */
}
