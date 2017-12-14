// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level9 : Level
{
    public override int levelNumber
    {
        get
        {
            return 9;
        }
    }

    public override string levelName
    {
        get
        {
            return "Nine";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 8; i++)
        {
            HomingMine current = (HomingMine)CreateObject("HomingMinePF", GetRandomPosition(), GetRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            SlowTurner current = (SlowTurner)CreateObject("SlowTurnerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            RotatingLazerSentry current = (RotatingLazerSentry)CreateObject("RotatingLazerSentryPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            LazerEmitter current = (LazerEmitter)CreateObject("LazerEmitterPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 4; i++)
        {
            IndestructableDebris current = (IndestructableDebris)CreateObject("IndestructableDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            RedBlob current = (RedBlob)CreateObject("RedBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            GreenBlob current = (GreenBlob)CreateObject("GreenBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            BlueBlob current = (BlueBlob)CreateObject("BlueBlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        Armor armor = (Armor)CreateObject("ArmorPF");
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
