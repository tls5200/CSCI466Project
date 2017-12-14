// written by: Diane Gregory
// tested by: Michael Quinn
// debugged by: Diane Gregory, Shane Barry
// balanced by: Diane Gregory, Metin Erman, Thomas Stewart

using UnityEngine;
using System.Collections;
using System;
using static GameStates;

public class Level2 : Level
{
    public override int levelNumber
    {
        get
        {
            return 2;
        }
    }

    public override string levelName
    {
        get
        {
            return "Two";
        }
    }

    protected override void CreateLevel()
    {
        MusicPlay("sounds/level1Loop");
  
        levelSize = new Vector2(80, 60); //set the level size
        
        CreateObject("SpaceDustPF", gameBounds.center, 0);

        for (int i = 0; i < 2; i++)
        {
            GravityWell current = (GravityWell)CreateObject("GravityWellPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 4; i++)
        {
            RandomTurner current = (RandomTurner)CreateObject("RandomTurnerPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 2; i++)
        {
            RubberyDebris current = (RubberyDebris)CreateObject("RubberyDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 4; i++)
        {
            SputteringDebris current = (SputteringDebris)CreateObject("SputteringDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        for (int i = 0; i < 1; i++)
        {
            IndestructableDebris current = (IndestructableDebris)CreateObject("IndestructableDebrisPF", GetRandomPosition(), GetRandomAngle());
            current.velocity = GetRandomVelocity(current.maxSpeed);
        }

        HomingMissiles homing = (HomingMissiles)CreateObject("HomingMissilesPF");
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
