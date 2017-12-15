using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHotkeySelect : MonoBehaviour
{
    private List<Selectable> m_orderedSelectables;

    private void Awake()
    {
        m_orderedSelectables = new List<Selectable>();
    }

    private float previousYAxis = 0;
    private void Update()
    {
        float yAxis = Input.GetAxis("InterfaceYAxis");

        if (Input.GetKeyDown(KeyCode.Tab) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            || yAxis > 0 && previousYAxis <= 0 || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            HandleHotkeySelect(true, true, false);
        }
        else if (Input.GetKeyDown(KeyCode.Tab) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            || yAxis < 0 && previousYAxis >= 0 || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            HandleHotkeySelect(false, true, false);
        }

        if (Input.GetButtonDown("InterfaceSubmit") || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            HandleHotkeySelect(false, false, true);
        }

        previousYAxis = yAxis;
    }

    private void HandleHotkeySelect(bool _isNavigateBackward, bool _isWrapAround, bool _isEnterSelect)
    {
        SortSelectables();

        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject != null && selectedObject.activeInHierarchy) // Ensure a selection exists and is not an inactive object.
        {
            Selectable currentSelection = selectedObject.GetComponent<Selectable>();
            if (currentSelection != null)
            {
                if (_isEnterSelect)
                {
                    ApplyEnterSelect(currentSelection);
                }
                else // Tab select.
                {
                    Selectable nextSelection = FindNextSelectable(m_orderedSelectables.IndexOf(currentSelection), _isNavigateBackward, _isWrapAround);
                    if (nextSelection != null)
                    {
                        nextSelection.Select();
                    }
                }
            }
            else
            {
                SelectFirstSelectable(_isEnterSelect);
            }
        }
        else
        {
            SelectFirstSelectable(_isEnterSelect);
        }
    }

    ///<summary> Selects an input field or button, activating the button if one is found. </summary>
    private void ApplyEnterSelect(Selectable _selectionToApply)
    {
        if (_selectionToApply != null)
        {
            _selectionToApply.Select();

            Button selectedButton = _selectionToApply.GetComponent<Button>();
            if (selectedButton != null)
            {
                selectedButton.OnPointerClick(new PointerEventData(EventSystem.current));
            }

            Toggle selectedToggle = _selectionToApply.GetComponent<Toggle>();
            if (selectedToggle != null)
            {
                selectedToggle.isOn = !selectedToggle.isOn;
            }

        }
    }

    private void SelectFirstSelectable(bool _isEnterSelect)
    {
        if (m_orderedSelectables.Count > 0)
        {
            Selectable firstSelectable = m_orderedSelectables[0];
            if (_isEnterSelect)
            {
                ApplyEnterSelect(firstSelectable);
            }
            else
            {
                firstSelectable.Select();
            }
        }
    }

    private Selectable FindNextSelectable(int _currentSelectableIndex, bool _isNavigateBackward, bool _isWrapAround)
    {
        Selectable nextSelection = null;

        int totalSelectables = m_orderedSelectables.Count;

        int numTried = 0;
        do
        {
            if (totalSelectables > 1)
            {
                if (_isNavigateBackward)
                {
                    if (_currentSelectableIndex == 0)
                    {
                        _currentSelectableIndex = totalSelectables - 1;
                        nextSelection = (_isWrapAround) ? m_orderedSelectables[_currentSelectableIndex] : null;
                    }
                    else
                    {
                        _currentSelectableIndex--;
                        nextSelection = m_orderedSelectables[_currentSelectableIndex];
                    }
                }
                else // Navigate forward.
                {
                    if (_currentSelectableIndex == (totalSelectables - 1))
                    {
                        _currentSelectableIndex = 0;
                        nextSelection = (_isWrapAround) ? m_orderedSelectables[_currentSelectableIndex] : null;
                    }
                    else
                    {
                        _currentSelectableIndex++;
                        nextSelection = m_orderedSelectables[_currentSelectableIndex];
                    }
                }
            }
            numTried++;
        } while (!nextSelection.IsInteractable() && numTried < totalSelectables);

        return (nextSelection);
    }

    private void SortSelectables()
    {
        List<Selectable> originalSelectables = Selectable.allSelectables;
        int totalSelectables = originalSelectables.Count;
        m_orderedSelectables = new List<Selectable>(totalSelectables);
        for (int index = 0; index < totalSelectables; ++index)
        {
            Selectable selectable = originalSelectables[index];
            m_orderedSelectables.Insert(FindSortedIndexForSelectable(index, selectable), selectable);
        }
    }

    ///<summary> Recursively finds the sorted index by positional order within m_orderedSelectables (positional order is determined from left-to-right followed by top-to-bottom). </summary>
    private int FindSortedIndexForSelectable(int _selectableIndex, Selectable _selectableToSort)
    {
        int sortedIndex = _selectableIndex;
        if (_selectableIndex > 0)
        {
            int previousIndex = _selectableIndex - 1;
            Vector3 previousSelectablePosition = m_orderedSelectables[previousIndex].transform.position;
            Vector3 selectablePositionToSort = _selectableToSort.transform.position;

            if (previousSelectablePosition.y == selectablePositionToSort.y)
            {
                if (previousSelectablePosition.x > selectablePositionToSort.x)
                {
                    // Previous selectable is in front, try the previous index:
                    sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
                }
            }
            else if (previousSelectablePosition.y < selectablePositionToSort.y)
            {
                // Previous selectable is in front, try the previous index:
                sortedIndex = FindSortedIndexForSelectable(previousIndex, _selectableToSort);
            }
        }

        return (sortedIndex);
    }
}

