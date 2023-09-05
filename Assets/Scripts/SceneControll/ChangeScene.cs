using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToLoad;
    private bool _isLoading = false;

    private void Update()
    {
        if (!_isLoading && Input.GetMouseButtonDown(0))
        {
            _isLoading = true;
            GoToScene();
        }
    }
    public void GoToScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
