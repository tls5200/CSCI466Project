// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level8 : Level
{
    public override int levelNumber
    {
        get
        {
            return 8;
        }
    }

    public override string levelName
    {
        get
        {
            return "Eight";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        IngameInterface.DisplayMessage("Survive for 5 minutes", 3);

        for (int i = 0; i < 2; i++)
        {
            Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            Rammer current = (Rammer)CreateObject("RammerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            SputteringDebris current = (SputteringDebris)CreateObject("SputteringDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        ChargedShots charged = (ChargedShots)CreateObject("ChargedShotPF");

        spawnTimer = (int)(spawnTimeSecs * updatesPerSec);
    }

    private const float MESSAGE_SECS = 5;
    private TimeSpan levelTime = new TimeSpan(0, 5, 0);
    private int remaining = 0;
    private int wave = 0;
    private int spawnTimer = 0;
    private float spawnTimeSecs = 60;

    protected override void UpdateLevel()
    {
        remaining = 0;

        foreach (DestructableObject item in destructables)
        {
            if (item.team <= 0)
            {
                remaining++;
            }
        }

        spawnTimer--;

        if (spawnTimer <= 0)
        {
            spawnTimer = (int)(spawnTimeSecs * updatesPerSec);

            wave++;
            switch (wave)
            {
                case 1:
                    IngameInterface.DisplayMessage("Wave 1", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)CreateObject("SlowTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RandomTurner current = (RandomTurner)CreateObject("RandomTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)CreateObject("RotatingLazerSentryPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerEmitter current = (LazerEmitter)CreateObject("LazerEmitterPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 2:
                    IngameInterface.DisplayMessage("Wave 2", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)CreateObject("SlowTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RandomTurner current = (RandomTurner)CreateObject("RandomTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        HomingMine current = (HomingMine)CreateObject("HomingMinePF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)CreateObject("RotatingLazerSentryPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 3:
                    IngameInterface.DisplayMessage("Wave 3", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)CreateObject("RotatingLazerSentryPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)CreateObject("SlowTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        LazerShooter current = (LazerShooter)CreateObject("LazerShooterPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    break;
                case 4:
                    IngameInterface.DisplayMessage("Wave 4", MESSAGE_SECS);
                    for (int i = 0; i < 1; i++)
                    {
                        MineLayer current = (MineLayer)CreateObject("MineLayerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        RotatingLazerSentry current = (RotatingLazerSentry)CreateObject("RotatingLazerSentryPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }

                    for (int i = 0; i < 1; i++)
                    {
                        SlowTurner current = (SlowTurner)CreateObject("SlowTurnerPF", GetRandomGameEdge(), GetRandomAngle());
                        current.velocity = GetRandomVelocity(current.maxSpeed);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public override string progress
    {
        get
        {
            return base.progress + ": " + (levelTime - duration).TotalSeconds.ToString() + " seconds remaining.";
        }
    }

    protected override void EndLevel()
    {
        
    }
    
    /*
    protected override bool Won()
    {
        
    }
    */
    
    protected override bool Lost()
    {
        if ((levelTime - duration).TotalMilliseconds <= 0)
            return true;
        else
            return false;
    }
}
