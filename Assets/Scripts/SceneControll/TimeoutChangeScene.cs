using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimeoutChangeScene : MonoBehaviour
{
    public string nextScene;

    // 跳转延迟的秒数
    public float delay = 10f;

    void Start()
    {
        // 开始协程
        StartCoroutine(SwitchSceneAfterDelay());
    }

    IEnumerator SwitchSceneAfterDelay()
    {
        // 等待一段时间之后跳转到下一个场景
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);

}}
