// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// OptionsGame is a MonoBehavior that controls the Game Options menu and has methods
/// that are called when its input UI items are changed, which edit their corropsonding Options values
/// </summary>
public class OptionsGame : MonoBehaviour
{
    //initilzed in editor
    public Slider autoSaves;
    public Slider interfaceAlpha;
    public Slider healthbarAlpha;
    public Slider cameraEdgeBuffer;
    public Slider zoomSpeed;
    public Slider keyDeadzone;
    public Slider keyActivationThreshold;
    public Toggle fixedGameBounds;
    public Toggle xboxNames;

    private Options options
    {
        get
        {
            return Options.Get();
        }
    }

    /// <summary>
    /// Set values in the menu to the values in Options
    /// </summary>
    private void Refresh()
    {
        autoSaves.value = options.levelMaxAutosaves;
        interfaceAlpha.value = options.ingameInterfaceAlpha;
        healthbarAlpha.value = options.healthBarAlpha;
        cameraEdgeBuffer.value = options.cameraEdgeBufferSize;
        zoomSpeed.value = options.cameraZoomSpeed;
        keyDeadzone.value = options.keyDeadZone;
        keyActivationThreshold.value = options.keyActivationThreshold;
        fixedGameBounds.isOn = options.levelStatic;
        xboxNames.isOn = options.keyXboxNames;
    }

    void Start()
    {
        Refresh();
    }

    private void OnEnable()
    {
        Refresh();
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Options value and refresh this menu.
    public void AutoSaves()
    {
        options.levelMaxAutosaves = (int)autoSaves.value;
        Refresh();
    }
    public void InterfaceAlpha()
    {
        options.ingameInterfaceAlpha = interfaceAlpha.value;
        Refresh();
    }
    public void HealthbarAlpha()
    {
        options.healthBarAlpha = healthbarAlpha.value;
        Refresh();
    }
    public void CameraEdgeBuffer()
    {
        options.cameraEdgeBufferSize = cameraEdgeBuffer.value;
        Refresh();
    }
    public void ZoomSpeed()
    {
        options.cameraZoomSpeed = zoomSpeed.value;
        Refresh();
    }
    public void KeyDeadzone()
    {
        options.keyDeadZone = keyDeadzone.value;
        Refresh();
    }
    public void KeyActivationThreshold()
    {
        options.keyActivationThreshold = keyActivationThreshold.value;
        Refresh();
    }
    public void FixedGameBounds()
    {
        options.levelStatic = fixedGameBounds.isOn;
        Refresh();
    }
    public void XBox360Names()
    {
        options.keyXboxNames = xboxNames.isOn;
        Refresh();
    }

    /// <summary>
    /// Method called by the Reset button, resets the Options to their default values and 
    /// refreshes this menu
    /// </summary>
    public void ResetButton()
    {
        options.SetDefaultOptions();
        Refresh();
    }

    /// <summary>
    /// Method called by the Back button, changes the screen to OptionsHub screen
    /// </summary>
    public void Back()
    {
        GameStates.gameState = GameStates.GameState.OptionsHub;
    }


}
