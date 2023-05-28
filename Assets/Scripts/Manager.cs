using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private PlayerController player;
    public MosterController moster;
    public QTE qte;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
