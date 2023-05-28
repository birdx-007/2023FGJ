using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    StraightLine,
    Parabola,
    Vertical,
    Spiral,
};

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    public BulletType bulletType;
    public float speed = 2f;
    public float turnSpeed = 2f;
    public Vector2 direction = Vector2.zero;
    public float gravity = 0f;
    public float lifeTime = 0f;
    public Vector2 inertia; //发射时角色移动速度

    public GameObject attackEffectPrefab;
    private Rigidbody2D attackEffectRb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetOnBulletEffect(Vector2 position)
    {
        if (bulletType == BulletType.Vertical)
        {
            GameObject effect = Instantiate(attackEffectPrefab, transform.position, Quaternion.identity);
            attackEffectRb = effect.GetComponent<Rigidbody2D>();

            // 设置效果类型
            AttackEffectController attackEffectController = effect.GetComponent<AttackEffectController>();
            attackEffectController.effectType = EffectType.PoisonousGas;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
            // 在消失的地方生成攻击效果
            SetOnBulletEffect(transform.position);
        }
        else if (other.CompareTag("Player"))
        {
            Character_Player player = other.GetComponent<PlayerController>().character;
            player.TakeDamage(15f);
            Destroy(gameObject);
        }
    }

    IEnumerator SpiralBullet(Rigidbody2D rb)
    {
        // 设置初始速度
        rb.velocity = transform.forward * speed;

        // 每帧更新速度产生螺旋线轨迹
        float t = 0;
        while (true)
        {
            rb.velocity = new Vector2(Mathf.Sin(t), Mathf.Cos(t)) * speed + direction * speed;
            t += Time.deltaTime * turnSpeed;
            yield return null;
        }
    }

    public void setInertia(Vector2 inertia)
    {
        this.inertia = inertia;
    }

    private void Update()
    {
        if (bulletType == BulletType.Spiral)
        {
            StartCoroutine(SpiralBullet(rb));
        }
    }
}
