using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class EnemyShootController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    protected static Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public abstract void ShootBullet();
}

class EnemyShoot_StraightLine : EnemyShootController
{
    public override void ShootBullet()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = playerTransform.position - transform.position;

        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the velocity of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction.normalized * bulletSpeed;
    }
}

class EnemyShoot_Parabola : EnemyShootController
{
    public float gravity = 9.8f;

    public override void ShootBullet()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = playerTransform.position - transform.position;

        // Calculate the time of flight
        float timeOfFlight = direction.magnitude / bulletSpeed;

        // Calculate the initial velocity in x and y directions
        float initialVelocityX = direction.x / timeOfFlight;
        float initialVelocityY = (direction.y + 0.5f * gravity * timeOfFlight * timeOfFlight) / timeOfFlight;

        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the velocity of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = new Vector2(initialVelocityX, initialVelocityY);
    }
}