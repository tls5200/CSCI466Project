// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level1 : Level
{
    public override int levelNumber
    {
        get
        {
            return 1;
        }
    }

    public override string levelName
    {
        get
        {
            return "One";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 6; i++)
        {
            Asteroid current = (Asteroid)CreateObject("AsteroidPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            RubberyDebris current = (RubberyDebris)CreateObject("RubberyDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }


        LazerSword sword = (LazerSword)CreateObject("LazerSwordPF");
        MultiShots multiShot = (MultiShots)CreateObject("MultiShotPF");
        Heal heal = (Heal)CreateObject("HealPF");
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
