// written by: Thomas Stewart

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
    //initilzed in editor
    //controls
    public Text forward;
    public Text backward;
    public Text left;
    public Text right;
    public Text turnUp;
    public Text turnDown;
    public Text turnLeft;
    public Text turnRight;
    public Text[] items;
    public Text pickupDrop;
    public Text shoot;
    public Text pause;
    public Text zoomIn;
    public Text zoomOut;
    public Toggle relative;
    public Toggle turn;
    public Toggle mouse;
    public Text chooseInput;
    //settings
    public Slider master;
    public Slider soundEffects;
    public Slider music;
    public Dropdown resolution;
    public Toggle fullscreen;
    public Slider autoSaves;
    public Slider interfaceAlpha;
    public Slider healthbarAlpha;
    public Slider cameraEdgeBuffer;
    public Slider zoomSpeed;
    public Slider keyDeadzone;
    public Slider keyActivationThreshold;
    public Toggle fixedGameBounds;
    public Toggle xboxNames;
    //player toggles
    public Toggle[] players;
    public Color selectedColor = Color.green;
    public Color notSelectedColor = Color.gray;

    private int playerNumber = 0;
    private PlayerControls playerControls;
    private Key keyToModify;

    private Options options
    {
        get
        {
            return Options.Get();
        }
    }

    /// <summary>
    /// Set values in the menu to the values in the PlayerControls
    /// </summary>
    private void Refresh()
    {
        //sets this menu's PlayerContorls the PlayerControls for the playerNumber set
        if (playerNumber >= 0 && playerNumber < Controls.MAX_PLAYERS)
            playerControls = Controls.Get().players[playerNumber];
        else
            throw new System.Exception("Player Number in ModifyControls invalid: " + playerNumber);

        //reset controls
        forward.text = playerControls.forwardKey.ToString();
        backward.text = playerControls.backwardKey.ToString();
        left.text = playerControls.straifLKey.ToString();
        right.text = playerControls.straifRKey.ToString();
        turnUp.text = playerControls.turnUpKey.ToString();
        turnDown.text = playerControls.turnDownKey.ToString();
        turnRight.text = playerControls.turnRKey.ToString();
        turnLeft.text = playerControls.turnLKey.ToString();
        if (items.Length != playerControls.itemKeys.Length)
        {
            throw new System.Exception("Wrong number of Item keys in ModifyControls");
        }
        for (int i = 0; i < items.Length; i++)
        {
            items[i].text = playerControls.itemKeys[i].ToString();
        }
        pickupDrop.text = playerControls.pickupDropKey.ToString();
        shoot.text = playerControls.shootKey.ToString();
        pause.text = playerControls.pauseKey.ToString();
        zoomIn.text = playerControls.zoomInKey.ToString();
        zoomOut.text = playerControls.zoomOutKey.ToString();
        relative.isOn = playerControls.relativeMovement;
        turn.isOn = playerControls.turns;
        mouse.isOn = playerControls.mouseTurn;

        //reset settings
        master.value = options.volumeMaster;
        soundEffects.value = options.volumeEffects;
        music.value = options.volumeMusic;
        fullscreen.isOn = options.fullScreen;
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

        //find supported resolutions and current resolution
        Resolution[] supportedResolutions = Screen.resolutions;

        Resolution currentResolution = Screen.currentResolution;

        //Converts the resolutions to the format the Dropdown will take and finds where in the list
        //the current resolution is located
        int resolutionNumber = 0;
        Dropdown.OptionData[] resolutionOptions = new Dropdown.OptionData[supportedResolutions.Length];
        for (int i = 0; i < supportedResolutions.Length; i++)
        {
            //convert
            resolutionOptions[supportedResolutions.Length - i - 1] = new Dropdown.OptionData(supportedResolutions[i].ToString());

            //find current resolution number
            if (supportedResolutions[i].ToString().Equals(currentResolution.ToString()))
            {
                resolutionNumber = supportedResolutions.Length - i - 1;
            }
        }

        resolution.options = new List<Dropdown.OptionData>(resolutionOptions);
        //resolution.value = resolutionNumber; //causes Unity to crash, I have no idea why
    }

    private void OnGUI()
    {
        //When a SavedGameItem is clicked, highlight that one
        foreach (Toggle item in players)
        {
            ColorBlock colors = item.colors;
            if (item.isOn)
            {

                colors.normalColor = selectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = selectedColor;
            }
            else
            {
                colors.normalColor = notSelectedColor;
                colors.highlightedColor = selectedColor;
                colors.pressedColor = notSelectedColor;
            }
            item.colors = colors;
        }
    }

    private void OnEnable()
    {
        Refresh();
    }

    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel")) && keyToModify == null)
        {
            Back();
        }

        //if there is a key that needs to be modified, ask the user to activate
        //an input unil he does. Once he does, set the input to the key being modified
        if (keyToModify != null)
        {
            //display message
            chooseInput.enabled = true;

            //see if an input is activated
            Key temp = Key.ActivatedKey();

            //if one is, set the key being modifed to it
            if (temp != null)
            {
                keyToModify.ChangeValue(temp);
                keyToModify = null;
                Refresh();
            }
        }
        else
        {
            //hide message
            chooseInput.enabled = false;
        }
    }

    //The following methods are called when the corrosponding controls button is pressed. 
    //It then sets the corrosponding Key to be modifed, which will be done in Update().
    public void Forward()
    {
        keyToModify = playerControls.forwardKey;
    }
    public void Backward()
    {
        keyToModify = playerControls.backwardKey;
    }
    public void MoveLeft()
    {
        keyToModify = playerControls.straifLKey;
    }
    public void MoveRight()
    {
        keyToModify = playerControls.straifRKey;
    }
    public void TurnUp()
    {
        keyToModify = playerControls.turnUpKey;
    }
    public void TurnDown()
    {
        keyToModify = playerControls.turnDownKey;
    }
    public void TurnLeft()
    {
        keyToModify = playerControls.turnLKey;
    }
    public void TurnRight()
    {
        keyToModify = playerControls.turnRKey;
    }
    public void Item1()
    {
        keyToModify = playerControls.itemKeys[0];
    }
    public void Item2()
    {
        keyToModify = playerControls.itemKeys[1];
    }
    public void Item3()
    {
        keyToModify = playerControls.itemKeys[2];
    }
    public void Item4()
    {
        keyToModify = playerControls.itemKeys[3];
    }
    public void Item5()
    {
        keyToModify = playerControls.itemKeys[4];
    }
    public void Item6()
    {
        keyToModify = playerControls.itemKeys[5];
    }
    public void Item7()
    {
        keyToModify = playerControls.itemKeys[6];
    }
    public void Item8()
    {
        keyToModify = playerControls.itemKeys[7];
    }
    public void Item9()
    {
        keyToModify = playerControls.itemKeys[8];
    }
    public void Item10()
    {
        keyToModify = playerControls.itemKeys[9];
    }
    public void PickupDrop()
    {
        keyToModify = playerControls.pickupDropKey;
    }
    public void Shoot()
    {
        keyToModify = playerControls.shootKey;
    }
    public void Pause()
    {
        keyToModify = playerControls.pauseKey;
    }
    public void ZoomIn()
    {
        keyToModify = playerControls.zoomInKey;
    }
    public void ZoomOut()
    {
        keyToModify = playerControls.zoomOutKey;
    }

    public void Player1()
    {
        playerNumber = 0;
        Refresh();
    }
    public void Player2()
    {
        playerNumber = 1;
        Refresh();
    }
    public void Player3()
    {
        playerNumber = 2;
        Refresh();
    }
    public void Player4()
    {
        playerNumber = 3;
        Refresh();
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Controls Options value.
    public void RelativeDirect()
    {
        playerControls.SetRelativeMovement(relative.isOn);
        //refresh();
    }
    public void TurnPoint()
    {
        playerControls.SetTurns(turn.isOn);
        //refresh();
    }
    public void Mouse()
    {
        playerControls.mouseTurn = mouse.isOn;
        Refresh();
    }

    //The following methods are called when the value of the corrosponding input UI item is 
    //changed, then they change their corrosponding Options value and refresh this menu.
    public void MasterVolume()
    {
        options.volumeMaster = master.value;
        Refresh();
    }
    public void SoundEffectVolume()
    {
        options.volumeEffects = soundEffects.value;
        Refresh();
    }
    public void MusicVolume()
    {
        options.volumeMusic = music.value;
        Refresh();
    }
    public void Resolution()
    {
        options.SetResolution(resolution.options[resolution.value].text);
        Refresh();
    }
    public void FullScreen()
    {
        options.fullScreen = fullscreen.isOn;
        //Refresh();
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
    /// Method called by the Reset button, resets the controls to their default values and 
    /// refreshes this menu
    /// </summary>
    public void DefaultControls()
    {
        Controls.Get().SetDefaultControls();
        Refresh();
    }

    /// <summary>
    /// Method called by the Reset button, resets the controls to their default values and 
    /// refreshes this menu
    /// </summary>
    public void DefaultSettings()
    {
        options.SetDefaultOptions();
        Refresh();
    }

    /// <summary>
    /// Method called by the Back button, changes the screen to Main screen
    /// </summary>
    public void Back()
    {
        GameStates.gameState = GameStates.previousGameState;
    }
}
