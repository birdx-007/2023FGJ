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
            case EffectType.PoisonousGas:
                StartCoroutine(FlameEffect(5));
                break;

            default:
                break;
        }
    }

    IEnumerator FlameEffect(float duration = 3f)
    {
        while (duration > 0)
        {
            // 检查与玩家碰撞


            duration -= 1;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            player.PoisonousGasEffectOn();
        }
    }
}
