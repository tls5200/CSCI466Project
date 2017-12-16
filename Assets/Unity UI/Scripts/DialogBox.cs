using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogBox : MonoBehaviour
{
    //initilize in editor
    public Text titleText;
    public Text messageText;
    public Button acceptButton;
    public Text acceptText;
    public Button rejectButton;
    public Text rejectText;

    public void Close()
    {
        Destroy(this.gameObject);
    }

    public static DialogBox Create()
    {
        GameObject obj = Instantiate(Resources.Load("DialogBoxPF"), Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject;
        obj.SetActive(true);
        return obj.GetComponent<DialogBox>();
    }

    public static DialogBox Create(string message)
    {
        DialogBox dialog = Create();

        dialog.messageText.text = message;

        return dialog;
    }

    public static DialogBox Create(string message, string title)
    {
        DialogBox dialog = Create();

        dialog.titleText.text = title;
        dialog.messageText.text = message;

        return dialog;
    }

    public static DialogBox Create(string message, string title, string acceptText)
    {
        DialogBox dialog = Create(title, message);

        dialog.acceptText.text = acceptText;

        return dialog;
    }

    public static DialogBox Create(string message, string title, UnityEngine.Events.UnityAction onAccept)
    {
        DialogBox dialog = Create(title, message);

        dialog.acceptButton.onClick.AddListener(onAccept);

        return dialog;
    }

    public static DialogBox Create(string message, string title, string acceptText, UnityEngine.Events.UnityAction onAccept)
    {
        DialogBox dialog = Create(title, message, acceptText);

        dialog.acceptButton.onClick.AddListener(onAccept);

        return dialog;
    }

    public static DialogBox Create(string message, string title, string acceptText, UnityEngine.Events.UnityAction onAccept, 
        string rejectText)
    {
        DialogBox dialog = Create(title, message, acceptText, onAccept);

        dialog.rejectButton.gameObject.SetActive(true);

        dialog.rejectText.text = rejectText;

        return dialog;
    }

    public static DialogBox Create(string message, string title, string acceptText, UnityEngine.Events.UnityAction onAccept,
        UnityEngine.Events.UnityAction onReject)
    {
        DialogBox dialog = Create(title, message, acceptText, onAccept);

        dialog.rejectButton.gameObject.SetActive(true);

        dialog.rejectButton.onClick.AddListener(onReject);

        return dialog;
    }

    public static DialogBox Create(string message, string title, UnityEngine.Events.UnityAction onAccept, 
        UnityEngine.Events.UnityAction onReject)
    {
        DialogBox dialog = Create(title, message, onAccept);

        dialog.rejectButton.onClick.AddListener(onReject);

        return dialog;
    }

    public static DialogBox Create(string message, string title, string acceptText, UnityEngine.Events.UnityAction onAccept,
        string rejectText, UnityEngine.Events.UnityAction onReject)
    {
        DialogBox dialog = Create(title, message, acceptText, onAccept, rejectText);

        dialog.rejectButton.onClick.AddListener(onReject);

        return dialog;
    }
}
