// written by: Shane Barry, Thomas Stewart
// tested by: Michael Quinn
// debugged by: Shane Barry, Thomas Stewart

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using DG.Tweening;

/// <summary>
/// LoadReplay is a MonoBehavior that controls the Load Replay menu and has methods 
/// that are called when the menu's buttons are pressed. It displays to the user 
/// the replay files that are locally saved. 
/// </summary>
public class LoadReplay : MonoBehaviour
{
    //initilzied in editor
    public GameObject replayList;
    public ToggleGroup group;
    public Color selectedColor = Color.green;
    public Color notSelectedColor = Color.gray;
    public GameObject errorPanel;
    public CanvasGroup canvasGroup;
    public Text errorText;
    public Button loadButton;

    public List<ReplayItem> replays = new List<ReplayItem>();

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
        //When a ReplayItem is clicked, highlight that one
        foreach (ReplayItem item in replays)
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
    /// Clears and repopulats the list of replays
    /// </summary>
    private void Refresh()
    {
        //clear the list
        replays.Clear();
        foreach (Transform t in replayList.transform)
        {
            Destroy(t.gameObject);
        }

        //find the replays in the save directory
        System.IO.Directory.CreateDirectory(Level.SAVE_PATH);
        List<FileInfo> files = new DirectoryInfo(Level.SAVE_PATH).GetFiles().OrderByDescending(f => f.LastWriteTime).ToList();

        //create a ReplayItem for each replay and add it to the replay list
        foreach (FileInfo item in files)
        {
            GameObject replay = ReplayItem.GetFromFile(item);
            if (replay != null)
            {
                replay.GetComponent<Toggle>().group = group;
                replay.GetComponent<Toggle>().isOn = false;
                replays.Add(replay.GetComponent<ReplayItem>());
            }
        }

        SortByDate();
    }

    /// <summary>
    /// Resets the parent of each replay in the list,
    /// this reorders the replays in the UI to the order they are in the list
    /// </summary>
    private void RefreshList()
    {
        foreach (ReplayItem item in replays)
        {
            item.transform.parent = null;
            item.transform.parent = replayList.transform;
            item.transform.localScale = Vector3.one;
        }

        OnGUI();
    }

    private void OnEnable()
    {
        Refresh();

        loadButton.Select();
    }

    private bool sortDateForward = false;
    public void SortByDate()
    {
        sortDateForward = !sortDateForward;

        if (sortDateForward)
        {
            replays.Sort((x, y) => x.date.text.CompareTo(y.date.text));
        }
        else
        {
            replays.Sort((x, y) => y.date.text.CompareTo(x.date.text));
        }

        RefreshList();
    }

    private bool sortNameForward = false;
    public void SortByName()
    {
        sortNameForward = !sortNameForward;

        if (sortNameForward)
        {
            replays.Sort((x, y) => x.saveName.text.CompareTo(y.saveName.text));
        }
        else
        {
            replays.Sort((x, y) => y.saveName.text.CompareTo(x.saveName.text));
        }

        RefreshList();
    }

    private bool sortLevelForward = false;
    public void SortByLevel()
    {
        sortLevelForward = !sortLevelForward;

        if (sortLevelForward)
        {
            replays.Sort((x, y) => x.level.text.CompareTo(y.level.text));
        }
        else
        {
            replays.Sort((x, y) => y.level.text.CompareTo(x.level.text));
        }

        RefreshList();
    }

    private bool sortPlayersForward = false;
    public void SortyByPlayers()
    {
        sortPlayersForward = !sortPlayersForward;

        if (sortPlayersForward)
        {
            replays.Sort((x, y) => x.players.text.CompareTo(y.players.text));
        }
        else
        {
            replays.Sort((x, y) => y.players.text.CompareTo(x.players.text));
        }

        RefreshList();
    }

    private bool sortDifficultyForward = false;
    public void SortyByDifficulty()
    {
        sortDifficultyForward = !sortDifficultyForward;

        if (sortDifficultyForward)
        {
            replays.Sort((x, y) => x.difficulty.text.CompareTo(y.difficulty.text));
        }
        else
        {
            replays.Sort((x, y) => y.difficulty.text.CompareTo(x.difficulty.text));
        }

        RefreshList();
    }

    private bool sortPvpForward = false;
    public void SortByPvp()
    {
        sortPvpForward = !sortPvpForward;

        if (sortPvpForward)
        {
            replays.Sort((x, y) => x.pvp.text.CompareTo(y.pvp.text));
        }
        else
        {
            replays.Sort((x, y) => y.pvp.text.CompareTo(x.pvp.text));
        }

        RefreshList();
    }

    /// <summary>
    /// creates a ReplayItem for each replay and add it to the replay list
    /// </summary>
    public void Load()
    {
        //find the selected replay and try to load it
        foreach (ReplayItem item in replays)
        {
            if (item.toggle.isOn)
            {
                //if load was successful, change the screen to Replay
                if (item.LoadReplay())
                {
                    GameStates.gameState = GameStates.GameState.Replay;
                }
                //if it wasn't successful, show an error
                else
                {
                    ShowErrorMenu("Problem loading replay!");
                }
                return;
            }
        }

        //if no replay was selected, show an error
        ShowErrorMenu("No replay selected.");
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
        //find the selected replay and try to delete it
        foreach (ReplayItem item in replays)
        {
            if (item.toggle.isOn)
            {
                //if delete was successful, refresh the replays
                if (item.DeleteReplay())
                {
                    Refresh();
                }
                //if it wasn't successful, show an error
                else
                {
                    ShowErrorMenu("Problem deleting replay!");
                }
                return;
            }
        }

        //if no replay was selected, show an error
        //ShowErrorMenu("No replay selected.");
        DialogBox.Create("Test message", "Test Title", delegate { Back(); });
    }

    public void ShowErrorMenu(string errorMsg)
    {
        errorText.text = errorMsg;
        errorPanel.SetActive(true);
        canvasGroup.DOFade(1.0f, 2.0f);
    }
}
