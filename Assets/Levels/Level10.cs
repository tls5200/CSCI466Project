// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level10 : Level
{
    public override int levelNumber
    {
        get
        {
            return 10;
        }
    }

    public override string levelName
    {
        get
        {
            return "Ten";
        }
    }

    private Vector2 minSize;
    private Vector2 shrinkAmount;
    private const int SHRINK_SECONDS = 300;

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(100, 80); //set the level size
        minSize = new Vector2(10, 8);
        shrinkAmount = (levelSize - minSize) / (SHRINK_SECONDS * updatesPerSec);
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.DisplayMessage("Kill all enemies before space collapses!", 3);

        for (int i = 0; i < 4; i++)
        {
            HomingMine current = (HomingMine)CreateObject("HomingMinePF", GetRandomPosition(), GetRandomAngle());
        }

        for (int i = 0; i < 2; i++)
        {
            Blob current = (Blob)CreateObject("BlobPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            Rammer current = (Rammer)CreateObject("RammerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 3; i++)
        {
            RandomTurner current = (RandomTurner)CreateObject("RandomTurnerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
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

        for (int i = 0; i < 2; i++)
        {
            IndestructableDebris current = (IndestructableDebris)CreateObject("IndestructableDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        Shield shield = (Shield)CreateObject("ShieldPF");
    }

    private bool levelLost = false;

    protected override void UpdateLevel()
    {
        if (levelSize.x > minSize.x)
            levelSize -= shrinkAmount;
        else
            levelLost = true;
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
    */
    
    protected override bool Lost()
    {
        //add loss conditions here, if player dies then its always loss

        return levelLost;
    }

}
