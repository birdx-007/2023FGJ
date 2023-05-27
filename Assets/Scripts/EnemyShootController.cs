using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ShootBullet_StraightLine()
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = playerTransform.position - transform.position;

        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the velocity of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.gravityScale = 0;
        bulletRb.velocity = direction.normalized * bulletSpeed;
    }

    public void ShootBullet_Parabola(float gravity = 10f)
    {
        // Calculate the direction from the enemy to the player
        Vector2 direction = playerTransform.position - transform.position;

        // Calculate the time of flight
        float timeOfFlight = Mathf.Abs(direction.x) / bulletSpeed;

        // Calculate the initial velocity in x and y directions
        float initialVelocityX = direction.x / timeOfFlight;
        float initialVelocityY = (direction.y + 0.5f * gravity * timeOfFlight * timeOfFlight) / timeOfFlight;

        // Create a bullet instance
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Set the velocity of the bullet
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.gravityScale = gravity / 10f;
        bulletRb.velocity = new Vector2(initialVelocityX, initialVelocityY);
    }
}
