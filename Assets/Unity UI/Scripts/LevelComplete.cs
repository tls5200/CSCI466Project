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
/// LevelComplete is a MonoBehavior that controls the Level Complete menu and
/// has methods that are called when the menus' buttons are pressed.
/// </summary>
public class LevelComplete : MonoBehaviour
{
    public UnityEngine.UI.InputField saveNameInputField; //initilzied in editor
    public UnityEngine.UI.InputField replayName; //initilized in editor
    public Button continueButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel"))
        {
            Quit();
        }

        //show current level number, Level.currentLevel.levelNumber
        //show level duration, Level.currentLevel.duration
        //show level difficulty, Level.currentLevel.difficulty
    }

    private void OnEnable()
    {
        continueButton.Select();
    }

    /// <summary>
    /// Method called by the Continue button, creates the next Level and sets the screen to Playing
    /// </summary>
    public void Contiue()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            DialogBox.Create("CurrentLevel is null when trying to go to next Level", "Error");
        //if there is a problem loading the next Level, display an error
        if (Level.current.NextLevel() == null)
            DialogBox.Create("Problem loading next Level", "Error");
            
        GameStates.gameState = GameStates.GameState.Playing;
        
    }

    /// <summary>
    /// Method called by the Save Game button, saves the information about the current Level so 
    /// it can be loaded later to bring the user back to the same point in the game
    /// </summary>
    public void SaveGame()
    {
        //if the current Level does not exist then an error message is displayed
        if (Level.current == null)
            DialogBox.Create("CurrentLevel is null when trying to save", "Error");
        //if there is an error with the replay file name then throw an exception
        else if (saveNameInputField == null || saveNameInputField.text == null)
            throw new Exception("Problem with SaveName InputField");
        //if user does not enter name in input field then desplay a message to him
        else if (saveNameInputField.text == "")
            DialogBox.Create("You must enter a name to create a save game", "Error"); 
        //create a save
        else
        {
            if (Level.current.Save(saveNameInputField.text))
                DialogBox.Create("Save game created successfully!", "Success");
            else
                DialogBox.Create("There was an error creating the save game...", "Error");
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