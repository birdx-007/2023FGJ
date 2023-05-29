using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    Transform targetTrans;
    public float beforeTrackSpeed = 30f;
    public float trackSpeed = 40f;
    public float maxTimeBeforeTrack = 0.5f;
    private float beforeTrackTimer;
    public bool isTracking = false;
    private Rigidbody2D rb;
    private Vector2 direction;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        targetTrans = GameObject.FindGameObjectWithTag("Enemy").transform; //玩家名
        beforeTrackTimer = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isTracking)
        {
            beforeTrackTimer += Time.deltaTime;
            rb.velocity = direction * ((1f - beforeTrackTimer / (float)maxTimeBeforeTrack) * beforeTrackSpeed);
            if (beforeTrackTimer >= maxTimeBeforeTrack)
            {
                beforeTrackTimer = 0;
                isTracking = true;
            }
        }
        else
        {
            Vector2 position = rb.position;
            Vector3 targetPosition = targetTrans.position;
            Vector2 delta = new Vector2(targetPosition.x - position.x, targetPosition.y - position.y);
            rb.velocity = delta.normalized * trackSpeed;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter enemy = other.GetComponent<PlayerCharacter>();
        if (enemy != null)
        {
            enemy.TakeDamage(1f);
            Destroy(gameObject);
        }
    }
}
