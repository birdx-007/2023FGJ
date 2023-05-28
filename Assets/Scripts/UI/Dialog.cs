using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance;

    // 使用SerializeField暴露字段至Inspector
    [SerializeField] private TextAsset dialogTextAsset;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private string nextSceneName;

    private List<string[]> _dialogData = new List<string[]>();
    private int _currentIndex;
    private bool _isChatting;

    private void Awake()
    {
        Instance = this;
    }

    public void SetDialogTextAsset(TextAsset textAsset)
    {
        dialogTextAsset = textAsset;
    }

    public void StartDialogue()
    {
        if (_isChatting) return;

        _isChatting = true;
        ParseDialogData();
        DisplayNextDialogue();
    }

    private void ParseDialogData()
    {
        // // release
        // string[] dialogLines = dialogTextAsset.text.Split('\n');
        // for (int i = 0; i < dialogLines.Length; i++)
        // {
        //     string[] splitData = dialogLines[i].Split('%');
        //     _dialogData.Add(splitData);
        // }
        // debug
        for (int i = 0; i < 10; i++)
        {
            string[] dialogLine = new string[2];
            dialogLine[0] = "name" + i;
            dialogLine[1] = "content" + i;
            _dialogData.Add(dialogLine);
        }
    }

    private void DisplayNextDialogue()
    {
        if (_currentIndex >= _dialogData.Count)
        {
            EndDialogue();
            return;
        }

        nameText.text = _dialogData[_currentIndex][0];
        contentText.text = _dialogData[_currentIndex][1];
        _currentIndex++;
    }

    private void EndDialogue()
    {
        _isChatting = false;
        nameText.text = "";
        contentText.text = "";
        // 切换到下一幕场景
        // SceneManager.LoadScene(nextSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isChatting)
        {
            DisplayNextDialogue();
        }
    }
}