// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DG.Tweening;

/// <summary>
/// LoadGame is a MonoBehavior that controls the Load Game menu and has methods 
/// that are called when the menu's buttons are pressed. It displays to the user 
/// the saved game files that are locally saved. 
/// </summary>
public class LoadGame : MonoBehaviour
{
    //initilzied in editor
    public GameObject gameSavesList;
    public ToggleGroup group;
    public Color selectedColor = Color.green;
    public Color notSelectedColor = Color.gray;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;
    public Button loadButton;
    public Toggle hideAutoSavesToggle;

    public List<SavedGameItem> saves = new List<SavedGameItem>();

   
    void Start()
    {
        Refresh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("InterfaceCancel"))
        {
            Back();
        }
        else if (Input.GetKeyDown(KeyCode.Delete))
        {
            Delete();
        }
    }

    private void OnGUI()
    {
        //When a SavedGameItem is clicked, highlight that one
        foreach (SavedGameItem item in saves)
        {
            ColorBlock colors = item.toggle.colors;
            if (item.toggle.isOn)
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
            item.toggle.colors = colors;
        }
    }

    /// <summary>
    /// Clears and repopulats the list of saved games
    /// </summary>
    private void Refresh()
    {
        //clear the list
        saves.Clear();
        foreach (Transform t in gameSavesList.transform)
        {
            Destroy(t.gameObject);
        }

        //find the saved games in the save directory
        Directory.CreateDirectory(Level.SAVE_PATH);
        List<FileInfo> files = new DirectoryInfo(Level.SAVE_PATH).GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();

        //create a SavedGameItem for each saved game and add it to the saved game list
        foreach (FileInfo item in files)
        {
            GameObject saveItem = SavedGameItem.getFromFile(item);
            if (saveItem != null)
            {
                saveItem.GetComponent<Toggle>().group = group;
                saveItem.GetComponent<Toggle>().isOn = false;
                saves.Add(saveItem.GetComponent<SavedGameItem>());
            }
        }

        SortByDate();
    }

    /// <summary>
    /// Resets the parent of each save in the list,
    /// this reorders the saves in the UI to the order they are in the list
    /// </summary>
    private void RefreshList()
    {
        foreach (SavedGameItem item in saves)
        {
            item.transform.parent = null;

            if (!(hideAutoSavesToggle.isOn && item.autoSave))
            {
                item.transform.parent = gameSavesList.transform;
                item.transform.localScale = Vector3.one;
            }
        }
        OnGUI();
    }

    private void OnEnable()
    {
        Refresh();
        loadButton.Select();
    }

    public void ToggleHideAutoSaves()
    {
        RefreshList();
    }

    private bool sortDateForward = false;
    public void SortByDate()
    {
        sortDateForward = !sortDateForward;

        if (sortDateForward)
        {
            saves.Sort((x, y) => x.date.text.CompareTo(y.date.text));
        }
        else
        {
            saves.Sort((x, y) => y.date.text.CompareTo(x.date.text));
        }

        RefreshList();
    }

    private bool sortNameForward = false;
    public void SortByName()
    {
        sortNameForward = !sortNameForward;

        if (sortNameForward)
        {
            saves.Sort((x, y) => x.saveName.text.CompareTo(y.saveName.text));
        }
        else
        {
            saves.Sort((x, y) => y.saveName.text.CompareTo(x.saveName.text));
        }

        RefreshList();
    }

    private bool sortLevelForward = false;
    public void SortByLevel()
    {
        sortLevelForward = !sortLevelForward;

        if (sortLevelForward)
        {
            saves.Sort((x, y) => x.level.text.CompareTo(y.level.text));
        }
        else
        {
            saves.Sort((x, y) => y.level.text.CompareTo(x.level.text));
        }

        RefreshList();
    }

    private bool sortPlayersForward = false;
    public void SortyByPlayers()
    {
        sortPlayersForward = !sortPlayersForward;

        if (sortPlayersForward)
        {
            saves.Sort((x, y) => x.players.text.CompareTo(y.players.text));
        }
        else
        {
            saves.Sort((x, y) => y.players.text.CompareTo(x.players.text));
        }

        RefreshList();
    }

    private bool sortDifficultyForward = false;
    public void SortyByDifficulty()
    {
        sortDifficultyForward = !sortDifficultyForward;

        if (sortDifficultyForward)
        {
            saves.Sort((x, y) => x.difficulty.text.CompareTo(y.difficulty.text));
        }
        else
        {
            saves.Sort((x, y) => y.difficulty.text.CompareTo(x.difficulty.text));
        }

        RefreshList();
    }

    private bool sortPvpForward = false;
    public void SortByPvp()
    {
        sortPvpForward = !sortPvpForward;

        if (sortPvpForward)
        {
            saves.Sort((x, y) => x.pvp.text.CompareTo(y.pvp.text));
        }
        else
        {
            saves.Sort((x, y) => y.pvp.text.CompareTo(x.pvp.text));
        }

        RefreshList();
    }

    /// <summary>
    /// creates a SavedGameItem for each saved game and add it to the saved game list
    /// </summary>
    public void Load()
    {
        //find the selected saved gae and try to load it
        foreach (SavedGameItem item in saves)
        {
            if (item.toggle.isOn)
            {
                //if load was successful, change the screen to Playing
                if (item.LoadSave())
                {
                    GameStates.gameState = GameStates.GameState.Playing;
                    
                }
                //if it wasn't successful, show an error
                else
                {
                    ShowErrorMenu("Problem loading save!");
                }
                return;
            }
        }

        //if no saved game was selected, show an error
        ShowErrorMenu("No save selected.");
    }

    /// <summary>
    /// method the back button calls, changes the screen to the Main menu
    /// </summary>
    public void Back()
    {
        GameStates.gameState = GameStates.GameState.Main;
    }

    public void Delete()
    {
        loadButton.onClick.AddListener(delegate { Back(); });
        //find the selected saved and try to delete it
        foreach (SavedGameItem item in saves)
        {
            if (item.toggle.isOn)
            {
                //if delete was successful, refresh the saves
                if (item.DeleteSave())
                {
                    Refresh();
                }
                //if it wasn't successful, show an error
                else
                {
                    ShowErrorMenu("Problem deleting save!");
                }
                return;
            }
        }

        //if no saved game was selected, show an error
        ShowErrorMenu("No save selected.");
    }

    public void ShowErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
