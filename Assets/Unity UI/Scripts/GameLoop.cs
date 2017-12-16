// written by: Metin Erman, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Thomas Stewart, Metin Erman

using System.Collections;
using System.Collections.Generic;
using static GameStates;
using UnityEngine;
using System;

/// <summary>
/// GameLoop is a MonoBehaviour that controls the state the of game. It switches the screen
/// the program is on depending on enumerator GameState. So, other classes can simply change the 
/// GameState to switch between screens.
/// </summary>
public class GameLoop : MonoBehaviour
{
    private static GameLoop gameLoop;
    private GameState lastGameState = GameState.Exit;
    private bool pause = false;
    
    //initilzed in editor
    public GameObject mainMenu;
    public GameObject newGameMenu;
    public GameObject loadGameMenu;
    public GameObject ingameInterface;
    public GameObject pauseMenu;
    public GameObject levelCompleteMenu;
    public GameObject gameOverMenu;
    public GameObject gameCompleteMenu;
    public GameObject loadReplayMenu;
    public GameObject optionsMenu;
    public GameObject aboutMenu;

    //here we ensure that this stays as a singleton---if any other user object is instantiated after the initial one, it is destroyed
    private void Awake() 
    {
        if (gameLoop == null)
        {
            gameLoop = this;
        }
        else
        {
            Destroy(this); //destroy if another one is attempted to be created.
        }
    }

    private void Update()
    {
        try
        {
            //if the gamestate has changed disable all menus then in the switch below,
            //only enable the one that it is set to
            if (gameState != lastGameState)
            {
                mainMenu.SetActive(false);
                newGameMenu.SetActive(false);
                loadGameMenu.SetActive(false);
                ingameInterface.SetActive(false);
                levelCompleteMenu.SetActive(false);
                pauseMenu.SetActive(false);
                gameOverMenu.SetActive(false);
                gameCompleteMenu.SetActive(false);
                loadReplayMenu.SetActive(false);
                optionsMenu.SetActive(false);
                aboutMenu.SetActive(false);
            }
            lastGameState = gameState;

            pause = false;
            //make sure to check each Player's pause Key to update their prevous pause
            foreach (PlayerControls item in Controls.Get().players)
            {
                if (item.Pause)
                    pause = true;
            }

            //set to 0, then set to 1 only when playing
            Time.timeScale = 0;

                //enable the menu that the screen is currently set to and 
                //set the timeScale to 1 if the Level is being played or replayed
            switch (gameState)
            {
                case GameState.Main:
                    mainMenu.SetActive(true);
                    break;
                case GameState.NewGame:
                    newGameMenu.SetActive(true);
                    break;
                case GameState.LoadGame:
                    loadGameMenu.SetActive(true);
                    break;
                case GameState.Playing:
                    ingameInterface.SetActive(true);
                    Time.timeScale = 1;
                    if (pause)
                        gameState = GameState.Paused;
                    break;
                case GameState.Paused:
                    ingameInterface.SetActive(true);
                    pauseMenu.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel"))
                        pause = true;
                    if (pause)
                        gameState = previousPlay;
                    break;
                case GameState.LevelComplete:
                    levelCompleteMenu.SetActive(true);
                    break;
                case GameState.LostGame:
                    gameOverMenu.SetActive(true);
                    break;
                case GameState.WonGame:
                    gameCompleteMenu.SetActive(true);
                    break;
                case GameState.LoadReplay:
                    loadReplayMenu.SetActive(true);
                    break;
                case GameState.Replay:
                    ingameInterface.SetActive(true);
                    Time.timeScale = 1;
                    if (pause)
                        gameState = GameState.Paused;
                    break;
                case GameState.Options:
                    optionsMenu.SetActive(true);
                    break;
                case GameState.About:
                    aboutMenu.SetActive(true);
                    break;
                case GameState.Exit:
                    Quit();
                    break;
                default:
                    throw new Exception("Invalid GameState: " + gameState.ToString());
            }
        }
        catch (Exception e)
        {
            throw new Exception("There was an error in the GameState Loop! StackTrace: " + e.StackTrace +
                " Message: " + e.Message);
        }
    }

    private void Quit()
    { 
        //save Controls and Options settings to their default files
        Controls.Get().SaveControls();
        Options.Get().SaveOptions();

        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif

    }
}
        

