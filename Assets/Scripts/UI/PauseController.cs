using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public Animator pauseMenuAnimator;
    public Animator instructionMenuAnimator;

    public void Pause()
    {
        StopTimeScale();
        ShowPauseMenu();
    }

    public void Resume()
    {
        RecoverTimeScale();
        HidePauseMenu();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Begin");
    }

    public void ShowInstruction()
    {
        instructionMenuAnimator.SetBool("isPausing", true);
    }

    public void HideInstruction()
    {
        instructionMenuAnimator.SetBool("isPausing", false);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void ShowPauseMenu()
    {
        if (pauseMenuAnimator == null)
        {
            Debug.LogError("PauseMenu's Animator hasn't been bound to PauseController.");
            return;
        }
        pauseMenuAnimator.SetBool("isPausing", true);
    }
    public void HidePauseMenu()
    {
        if (pauseMenuAnimator == null)
        {
            Debug.LogError("PauseMenu's Animator hasn't been bound to PauseController.");
            return;
        }
        pauseMenuAnimator.SetBool("isPausing",false);
    }
    public void StopTimeScale()
    {
        Time.timeScale = 0f;
    }
    public void RecoverTimeScale()
    {
        Time.timeScale = 1f;
    }
}
