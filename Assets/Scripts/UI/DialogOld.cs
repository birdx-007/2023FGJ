// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
//
// //使用方法
// //调用Dialog.share.CreateDialogue(text);
// //text输入固定对话
// public class DialogOld : MonoBehaviour
// {
//
//     public static DialogOld share;
//     public TextAsset _mTextAsset;
//
//     public List<string[]> List_diaContents = new List<string[]>(); //用来存储所有的对话内容
//
// //public Image dialogueBg; //对话款背景
//     public Text _textName; //对话人物的名字
//
//     public Text _textContent; //对话人物说的话
// //public Image _imageHead; //头像
// //public Sprite[] IconSprites;//所有头像集合
//
//     private bool isChat = false; //是否在对话
//
//     private int index = 0; //对话内容的索引
// //private Tweener tweener; //对话框进入和离开屏幕的动画
//
//     private void Awake()
//     {
//         share = this;
//     }
//
//     void Start()
//     {
//         CreateDialogue(_mTextAsset);
//         //_textName =GameObject.Find("TextName").GetComponent<Text>();
//         //_textContent =GameObject.Find("TextContent").GetComponent<Text>();
//         //tweener = dialogueBg.rectTransform.DOLocalMoveY(-150, 0.5f).SetEase(Ease.InBack) .SetAutoKill(false);
//         //tweener.Pause(); //动画一开始设置为暂停
//
//         //IconSprites = Resources.LoadAll<Sprite>("Icon"); //获取所有头像集合
//     }
//
//     /// <summary>
//     /// 创建一个对话框
//     /// </summary>
//     /// <param name="_mTextAsset">文本资源</param>
//     public void CreateDialogue(TextAsset _mTextAsset)
//     {
//         if (isChat)
//         {
//             Debug.Log("在说话了");
//             return;
//         }
//
//         List_diaContents.Clear(); //每次都清空对话 List
// //初始化文本资源里的对话内容
//         isChat = true;
//         string[] textAll = _mTextAsset.text.Split('\n'); //先根据换行符切割出每一行文字
//         for (int i = 0; i < textAll.Length; i++)
//         {
//             string[] contents = textAll[i].Split('%'); //根据%切割出三个 0 名字 1说的话 2头像
//             //Debug.Log(contents.Length);
//             List_diaContents.Add(contents); //把名字 对话 头像 存进List
//             //Debug.Log(List_diaContents.Count);
//         }
// //tweener.PlayForward(); //播放对话框进入屏幕的动画
//     }
//
//     void Update()
//     {
//         if (isChat)
//         {
//             //Debug.Log(List_diaContents[index][0]);
//             _textName.text = List_diaContents[index][0]; //显示对话人物的名称
//             _textContent.text = List_diaContents[index][1]; //显示对话的内容
//             //int i = int.Parse(List_diaContents[index][2]);
//             //_imageHead.sprite = IconSprites[i];//显示头像
//             if (Input.GetKeyDown(KeyCode.T))
//             {
//                 Debug.Log("next"); //
//                 index++;
//                 if (index >= List_diaContents.Count) //当对话到了最后一步
//                 {
//                     //tweener.PlayBackwards(); //倒放对话框动画
//                     index = 0;
//                     isChat = false; //关闭
//                 }
//             }
//         }
//         else
//         {
//             _textContent.text = "";
//             _textName.text = "";
//         }
//     }
// }