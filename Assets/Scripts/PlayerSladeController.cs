using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSladeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCharacter enemy = other.GetComponent<PlayerCharacter>();
        if (enemy != null)
        {
            enemy.TakeDamage(1f);
        }
    }
}
