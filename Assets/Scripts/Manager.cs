using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private PlayerController player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
