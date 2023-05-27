using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class DialogController : MonoBehaviour
{
    public DialogData dialogEmpty;
    public DialogData dialogFinish;

    public DialogUI dialogui;

    private Stack<string> dialogEmptyStack = new Stack<string>();
    private Stack<string> dialogFinishStack = new Stack<string>();

    public bool isTalking;


//DialogController.cs
    private void Awake()
    {
        FillDialogStack();
    }

    public void FillDialogStack()
    {
        for (int i = dialogEmpty.dialogList.Count - 1; i >= 0; i--)
        {
            dialogEmptyStack.Push(dialogEmpty.dialogList[i]);
        }

        for (int i = dialogFinish.dialogList.Count - 1; i >= 0; i--)
        {
            dialogFinishStack.Push(dialogFinish.dialogList[i]);
        }
    }

    public void ShowDialogEmpty()
    {
        if (!isTalking)
            StartCoroutine(DialogRoutine(dialogEmptyStack));
    }

    public void ShowDialogFinish()
    {
        if (!isTalking)
            StartCoroutine(DialogRoutine(dialogFinishStack));
    }

    private IEnumerator DialogRoutine(Stack<string> data)
    {
        isTalking = true;

        // 循环弹出对话并显示
        while (data.TryPop(out string result))
        {
            dialogui.SetDialogText(result);
            // 等待玩家确认对话，按下空格键
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        isTalking = false;

        // 结束对话（修改组件后这里页需要修改）
        dialogui.HideDialogPanel();
    }

}