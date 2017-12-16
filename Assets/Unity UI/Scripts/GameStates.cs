// written by: Metin Erman, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Metin Erman, Thomas Stewart

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    private static GameState theGameState = GameState.Main;
    public static GameState gameState
    {
        get
        {
            return theGameState;
        }
        set
        {
            if (theGameState == GameState.Playing || theGameState == GameState.Replay)
                previousPlay = theGameState;
            else
                previousMenu = theGameState;
            previousGameState = theGameState;
               
            theGameState = value;
        }
    }
    public static GameState previousGameState = GameState.Main, previousMenu = GameState.Main, previousPlay = GameState.Playing;

    //this will be used to control the state of the game we are in.
    public enum GameState { Main, NewGame, LoadGame, Playing, Paused,
        LevelComplete, LostGame, WonGame, LoadReplay, Options, Replay, About, Exit} 
}
