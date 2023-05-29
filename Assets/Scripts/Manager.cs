using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager instance;
    private Character_Player player;
    private PlayerCharacter enemy;
    private GroundController ground;

    public bool isWin = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        GetPlayerAndEnemy();
    }

    public void GetPlayerAndEnemy()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Character_Player>();
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PlayerCharacter>();
        ground = GameObject.FindGameObjectWithTag("Ground").GetComponent<GroundController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    IEnumerator GoToEnding()
    {
        yield return new WaitForSeconds(3f);
        if (isWin)
        {
            SceneManager.LoadScene("GoodendingAnimation");
        }
        else
        {
            SceneManager.LoadScene("BadendingAnimation");
        }
    }

    public void GoToBadEnding()
    {
        SceneManager.LoadScene("BadendingAnimation");
    }

    public void GoToGoodEnding()
    {
        isWin = true;
        ground.SetRendererSprite(true);
        StartCoroutine(GoToEnding());
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
