using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneControl : MonoBehaviour
{

public void GoToTestScene() // 这里你需要将 GoToNextScene 定义为 Public 类型，否则 Button 无法调用
{
    SceneManager.LoadScene("TestScene");
}

}
