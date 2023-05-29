using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    Normal,
    Poison,
    Fire,
    Etc,
}

class EnemyShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    private Rigidbody2D bulletRigidBody;
    public Transform firePoint;
    public float bulletSpeed = 2f;
    protected static Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        firePoint = GetComponent<Transform>();
    }

    public void ShootBullet(Vector2 characterSpeed, BulletType bulletType = BulletType.Normal, BulletTrack
        bulletTrack = BulletTrack.StraightLine)
    {
        Debug.Log("Shooting bullet" + bulletTrack + bulletType);

        Vector2 direction = playerTransform.position - transform.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        BulletController bulletController = bullet.GetComponent<BulletController>();
        bulletController.bulletTrack = bulletTrack;
        bulletController.bulletType = bulletType;

        // Set the velocity of the bullet
        if (bulletTrack == BulletTrack.StraightLine)
        {
            bulletRb.gravityScale = 0;
            bulletRb.velocity = direction.normalized * bulletSpeed;
        }
        else if (bulletTrack == BulletTrack.Parabola)
        {
            float timeOfFlight = Mathf.Abs(direction.x) / bulletSpeed;
            float initialVelocityX = direction.x / timeOfFlight;
            float initialVelocityY =
                (direction.y + 0.5f * bulletController.gravity * timeOfFlight * timeOfFlight) / timeOfFlight;
            bulletRb.gravityScale = bulletController.gravity / 10f;
            bulletRb.velocity = new Vector2(initialVelocityX, initialVelocityY);
        }
        else if (bulletTrack == BulletTrack.Vertical)
        {
            bulletRb.gravityScale = 0;
            bulletRb.velocity = new Vector2(0, -bulletSpeed);
        }
        else if (bulletTrack == BulletTrack.Spiral)
        {
            bulletController.speed = bulletSpeed;
            bulletController.turnSpeed = 10f;
            bulletController.direction = direction.normalized;
        }

        // 考虑惯性
        bulletRb.velocity += characterSpeed;
    }
}