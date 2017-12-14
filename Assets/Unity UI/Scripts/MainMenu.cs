// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using static GameStates;

/// <summary>
/// MainMenu is a MonoBehavior that controls the Main menu and has methods
/// that are called when the menu's buttons are pressed. 
/// </summary>
public class MainMenu : MonoBehaviour
{
    //initilized in editor
    public UnityEngine.UI.Button loadGame;
    public UnityEngine.UI.Button watchReplay;

    /// <summary>
    /// Method the New Game button calls, changes the screen to the New Game screen
    /// </summary>
    public void NewGame()
    {
        gameState = GameState.NewGame;
    }

    /// <summary>
    /// Method the Load Game button calls, changes the screen to the Load Game screen
    /// </summary>
    public void LoadGame()
    {
        gameState = GameState.LoadGame;
    }

    /// <summary>
    /// Method the Watch Replay button calls, changes the screen to the Watch Replay screen
    /// </summary>
    public void WatchReplay() 
    {
        gameState = GameState.LoadReplay;
    }

    /// <summary>
    /// Method the Options button calls, changes the screen to the Options Hub screen
    /// </summary>
    public void Options()
    {
        gameState = GameState.OptionsHub;
    }

    /// <summary>
    /// Method the About button calls, changes the screen to the About screen
    /// </summary>
    public void About()
    {
        gameState = GameState.About;
    }

    /// <summary>
    /// Method the Quit button calls, changes the GameState to Exit which will cause GameLoop to close the program
    /// </summary>
    public void Quit()
    {
        gameState = GameState.Exit;
    }
}
