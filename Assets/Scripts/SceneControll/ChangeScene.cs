using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public Button changeSceneButton;
    public string targetScene;

    void Start()
    {
        changeSceneButton.onClick.AddListener(ChangeToTestScene);
    }

    void ChangeToTestScene()
    {
        SceneManager.LoadScene(targetScene); 
    }
}
