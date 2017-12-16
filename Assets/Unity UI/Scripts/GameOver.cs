// written by: Shane Barry, Thomas Stewart, Metin Erman
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// GameOver is a MonoBehavior that controls the Game Complete and Game Over menus
/// has methods that are called when the menus' buttons are pressed. The Game Complete menu
/// doesn't use the Restart() method.
/// </summary>
public class GameOver : MonoBehaviour
{
    //initilized in editor
    public InputField replayName;
    public Button restartButton;

    private void OnEnable()
    {
        restartButton.Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel"))
        {
            Quit();
        }
    }

    /// <summary>
    /// Method called by the Restart button on the gameover menu to set level to its beginning
    /// </summary>
    public void Restart() 
    {
        //if there is not a current Level, display error message
        if (Level.current == null) 
        {
            DialogBox.Create("Can't restart level, current level is set to null", "Error");
        }
        else
        {
            Level.current.RestartLevel();
            GameStates.gameState = GameStates.GameState.Playing;
        }
	}

    /// <summary>
    /// Method called by the Save Replay button, saves a replay of the current Level
    /// to a file of the replayName.
    /// </summary>
    public void SaveReplay()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            DialogBox.Create("CurrentLevel is null when trying to saveReplay", "Error");
        //if there is an error with the replay file name then throw an exception
        else if (replayName == null || replayName.text == null)
            throw new Exception("Problem with replayName InputField");
        //if user does not enter name in input field then desplay a message to him
        else if (replayName.text == "")
            DialogBox.Create("You need to enter a name before saving the replay", "Error");
        //else create save the replay
        else
        {
            if (Level.current.SaveReplay(replayName.text))
                DialogBox.Create("Replay has been saved successfully!", "Success");
            else
                DialogBox.Create("There was a problem saving the replay!", "Error");
        }
    }

    /// <summary>
    /// Method called by the Quit button, destroys the current Level and changes the screen to the Main menu 
    /// </summary>
    public void Quit()
    {
        if (Level.current != null)
        {
            Destroy(Level.current.gameObject);
        }
        GameStates.gameState = GameStates.GameState.Main;
	}
}
