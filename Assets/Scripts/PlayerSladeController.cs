using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSladeController : MonoBehaviour
{
    public bool isValid;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isValid)
        {
            return;
        }
        PlayerCharacter enemy = other.GetComponent<PlayerCharacter>();
        if (enemy != null)
        {
            enemy.TakeDamage(10f);
            isValid = false;
        }
    }
}
