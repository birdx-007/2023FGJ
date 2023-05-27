using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [SerializeField] private Text dialogText;
    [SerializeField] public GameObject dialogPanel;

    public void SetDialogText(string message)
    {
        dialogText.text = message;
        dialogPanel.SetActive(true);
    }

    public void HideDialogPanel()
    {
        dialogPanel.SetActive(false);
    }
}
