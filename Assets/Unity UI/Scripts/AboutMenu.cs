// written by: Shane Barry
// tested by: Michael Quinn
// debugged by: Shane Barry

using UnityEngine;
using System.Collections;

/// <summary>
/// AboutMenu is a MonoBehavior that controls the About menu and has methods 
/// that are called when the menu's buttons are pressed. 
/// </summary>
public class AboutMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel"))
        {
            Back();
        }
    }

        public void Email()
    {
        Application.OpenURL("mailto: t_stewart@mail.fhsu.edu");
    }

    public void Website()
    {
        Application.OpenURL("http://nebulawars.heliohost.org/");
    }
    
    /// <summary>
    /// method the back button calls, changes the screen to the Main menu
    /// </summary>
    public void Back() 
    {
        GameStates.gameState = GameStates.GameState.Main;
    }
}
