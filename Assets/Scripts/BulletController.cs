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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
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

    private void Update()
    {
        if (bulletType == BulletType.Spiral)
        {
            StartCoroutine(SpiralBullet(rb));
        }
    }
}
