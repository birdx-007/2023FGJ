using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private PlayerController player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        
    }

    public void RestartLevel()
    {
        Debug.Log("Restart level.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
