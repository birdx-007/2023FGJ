using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TagChangeScene : MonoBehaviour
{
    public string sceneName;   // 要跳转的场景的名称
    public string triggerTag;  // 触发跳转的标签名称

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // 监听鼠标左键
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))  // 监听点击的物体
            {
                if (hit.collider.CompareTag(triggerTag))  // 判断标签名称是否匹配
                {
                    SceneManager.LoadSceneAsync(sceneName);  // 切换场景
                }
            }
        }
    }
}
