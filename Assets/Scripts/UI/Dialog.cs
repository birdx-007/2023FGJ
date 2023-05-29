using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance;

    // 使用SerializeField暴露字段至Inspector
    [SerializeField] private TextAsset dialogTextAsset;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI contentText;
    [SerializeField] private string nextSceneName;
    [SerializeField] private Image avatar;
    [SerializeField] private Sprite[] avatarImages; //所有头像集合

    private List<string[]> _dialogData = new List<string[]>();
    private int _currentIndex;
    private bool _isChatting;

    private void Awake()
    {
        Instance = this;
        avatarImages = Resources.LoadAll<Sprite>("Icon");
        nextSceneName = "TestScene";
    }

    public void SetDialogTextAsset(TextAsset textAsset)
    {
        dialogTextAsset = textAsset;
    }

    public int ParseIconIndex(string name)
    {
        for (int i = 0; i < avatarImages.Length; i++)
        {
            if (avatarImages[i].name == name)
            {
                return i;
            }
        }

        return -1;
    }

    /// <summary>
    /// 开始对话
    /// </summary>
    public void StartDialogue()
    {
        if (_isChatting) return;

        _isChatting = true;
        ParseDialogData();
        DisplayNextDialogue();
    }

    /// <summary>
    /// 解析对话数据
    /// </summary>
    private void ParseDialogData()
    {
        // release
        string[] dialogLines = dialogTextAsset.text.Split('\n');
        for (int i = 0; i < dialogLines.Length; i++)
        {
            string[] splitData = dialogLines[i].Split('%');
            _dialogData.Add(splitData);
        }
        // debug
        // for (int i = 0; i < 10; i++)
        // {
        //     string[] dialogLine = new string[2];
        //     dialogLine[0] = "name" + i;
        //     dialogLine[1] = "content" + i;
        //     _dialogData.Add(dialogLine);
        // }
    }

    /// <summary>
    /// 切换下一条内容
    /// </summary>
    private void DisplayNextDialogue()
    {
        if (_currentIndex >= _dialogData.Count)
        {
            EndDialogue();
            return;
        }

        nameText.text = _dialogData[_currentIndex][0];
        contentText.text = _dialogData[_currentIndex][1];
        //头像
        // avatar.sprite = avatarImages[ParseIconIndex(_dialogData[_currentIndex][2])];
        avatar.sprite = avatarImages[int.Parse(_dialogData[_currentIndex][2])];
        _currentIndex++;
    }

    /// <summary>
    /// 结束对话
    /// </summary>
    private void EndDialogue()
    {
        _isChatting = false;
        nameText.text = "Player";
        contentText.text = "...";
        // 切换到下一幕场景
        SceneManager.LoadScene(nextSceneName);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isChatting)
        {
            DisplayNextDialogue();
        }
    }
}