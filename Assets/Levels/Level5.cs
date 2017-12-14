// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level5 : Level
{
    public override int levelNumber
    {
        get
        {
            return 5;
        }
    }

    public override string levelName
    {
        get
        {
            return "Five";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.DisplayMessage("Survive the onslaught!", 3);

        for (int i = 0; i < 6; i++)
        {
            Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        HomingMines mines = (HomingMines)CreateObject("HomingMinesPF");
    }

    private int spawnTimer = 0;
    private float spawnTimeSecs = 60;

    protected override void UpdateLevel()
    {
        spawnTimer--;

        if (spawnTimer <= 0)
        {
            spawnTimer = (int)(spawnTimeSecs * updatesPerSec);

            for (int i = 0; i < 1; i++)
            {
                Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomGameEdge(), GetRandomAngle());
                current.velocity = GetRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomGameEdge(), GetRandomAngle());
                current.velocity = GetRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomGameEdge(), GetRandomAngle());
                current.velocity = GetRandomVelocity(current.maxSpeed);
            }

            for (int i = 0; i < 1; i++)
            {
                LazerEmitter current = (LazerEmitter)CreateObject("LazerEmitterPF", GetRandomGameEdge(), GetRandomAngle());
                current.velocity = GetRandomVelocity(current.maxSpeed);
            }
        }

    }

    public override string progress
    {
        get
        {
            return base.progress + "  Seconds until next spawn: " + (int)(spawnTimer / secsPerUpdate);
        }
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
