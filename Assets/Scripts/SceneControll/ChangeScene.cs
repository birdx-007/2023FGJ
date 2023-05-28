using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public GameObject imageObject; //指定该脚本所控制的图片对象
    public string sceneToLoad;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    public void GoToScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
