using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            Destroy(gameObject);
        }
    }
}
