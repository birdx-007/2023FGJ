using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EffectType
{
    PoisonousGas,
    Explosion,
    Flame,
    ICE,
}

public class AttackEffectController : MonoBehaviour
{
    private Rigidbody2D rb;
    public EffectType effectType;
    public float lifeTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        switch (effectType)
        {
            case EffectType.Explosion:
                Debug.Log("Set explosion effect");
                lifeTime = 1.5f;
                StartCoroutine(AttackEffect());
                break;
            case EffectType.PoisonousGas:
                Debug.Log("Set poisonous gas effect");
                lifeTime = 5f;
                StartCoroutine(AttackEffect());
                break;
            default:
                break;
        }
    }

    IEnumerator AttackEffect()
    {
        while (lifeTime > 0)
        {
            // 检查与玩家碰撞
            lifeTime -= 1;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Character_Player player = other.GetComponent<PlayerController>().character;
            if (effectType == EffectType.PoisonousGas)
            {
                player.PoisonousGasEffectOn();
            }
        }
    }
}
