using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBulletController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            // 在消失的地方生成攻击效果
            //SetOnBulletEffect(transform.position);
        }
        else if (other.CompareTag("Player"))
        {
            Character_Player player = other.GetComponent<PlayerController>().character;
            player.TakeDamage(25f);
            Destroy(gameObject);
        }
    }
}
