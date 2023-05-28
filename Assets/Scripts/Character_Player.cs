using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Player : MonoBehaviour
{
    public GameObject deathModel;
    public GameObject playerBulletPrefab;
    public ParticleSystem teleportParticle;
    //速度
    public float speed = 10f;
    public float sprintSpeedMultiplier = 2f;
    public float teleportSpeedMultiplier = 5f;

    public float jumpForce = 40f;
    public float maxStamina = 100f; // 最大体力值
    public float currentStamina;
    public float staminaRecoveryRate = 20f; // 恢复速率
    public float sprintStaminaCostPerSecond = 40f; // 加速消耗
    public float teleportStaminaCostPerSecond = 100f;

    private Rigidbody2D rb;

    public bool isJumping = false;
    public bool isSprinting = false;
    public bool isTeleporting = false;

    public bool canTeleport = false;
    public float maxTeleportTime = 0.5f;
    private float teleportTimer;

    //攻击
    //设计攻击冷却时间
    public bool attacking = false;

    private int atttackmode;

    public int bulletNum = 5;

    //生命
    public float currentHealth;
    public float maxHealth = 100f;
    public bool isVunerable = true;
    bool isAlive;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        isAlive = true;
        teleportTimer = 0;
        teleportParticle.Stop();
    }
    //受伤
    public void TakeDamage(float amount)
    {
        if (!isVunerable)
        {
            return;
        }
        currentHealth -= amount;
        if (currentHealth <= 0f && isAlive)
        {
            currentHealth = 0f;
            Death();
        }
    }

    //死亡
    public void Death()
    {
        isAlive = false;
        GetComponent<Renderer>().enabled = false;
        rb.isKinematic = true;

        // 激活死亡形象
        deathModel.SetActive(true);

    }

//
    public void stand_attack()
    {
        if (!isAlive) return;
        if (attacking) return;

        //攻击
    }

// 减少当前剩余体力值，同时根据需要停止加速。
    private void ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
        }
        else
        {
            currentStamina = 0;
            isSprinting = false; // 停止加速
        }
    }

    // 增加当前剩余的体能，如果超过最大值将其去到 maximum。
    private void RecoverStamina(float amount)
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += amount;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
    public void Move(float moveHorizontal) // movehorizontal横向，sprinting是否奔跑
    {
        float currentSpeed = speed;
        if (isSprinting)
        {
            if (currentStamina > 0)
            {
                isSprinting = true;
                currentSpeed = speed * sprintSpeedMultiplier;
                ConsumeStamina(sprintStaminaCostPerSecond * Time.fixedDeltaTime);
            }
            else
            {
                currentStamina = 0;
                isSprinting = false;
            }
        }

        if (isTeleporting)
        {
            if (currentStamina > 0 && teleportTimer < maxTeleportTime)
            {
                isTeleporting = true;
                isVunerable = false;
                currentSpeed = speed * teleportSpeedMultiplier;
                ConsumeStamina(teleportStaminaCostPerSecond * Time.fixedDeltaTime);
                teleportTimer += Time.fixedDeltaTime;
            }
            else
            {
                currentStamina = 0;
                isTeleporting = false;
                isVunerable = true;
                teleportTimer = 0;
                DisableTeleport();//作为技能只能用一次
            }
        }

        // 控制角色的运动
        rb.velocity = new Vector2(moveHorizontal * currentSpeed, rb.velocity.y);

        // 回复体力（如果不加速）
        if (!isSprinting && !isTeleporting)
        {
            RecoverStamina(staminaRecoveryRate * Time.fixedDeltaTime);
        }
    }

    public void Jump(float forceRatio = 1f)
    {
        if (isJumping)
        {
            return;
        }
        rb.AddForce(Vector2.up * (jumpForce * forceRatio), ForceMode2D.Impulse);
        isJumping = true;
    }

    public void EnableTeleport()
    {
        teleportParticle.Play();
        canTeleport = true;
    }
    public void DisableTeleport()
    {
        teleportParticle.Stop();
        canTeleport = false;
    }
    public void Teleport()
    {
        if (isTeleporting || !canTeleport)
        {
            return;
        }
        isTeleporting = true;
    }

    public void FireBullets()
    {
        // Calculate the angle between bullets
        float angleStep = Mathf.PI / (bulletNum - 1);

        // Spawn multiple bullets
        for (int i = 0; i < bulletNum; i++)
        {
            Vector2 direction = Vector2.up * Mathf.Sin(i * angleStep) + Vector2.right * Mathf.Cos(i * angleStep);
            // Create a bullet instance
            GameObject bullet = Instantiate(playerBulletPrefab, rb.position + Vector2.up*0.5f, Quaternion.identity);
            bullet.GetComponent<PlayerBulletController>().SetDirection(direction);
        }

        StartCoroutine(KeepInvunerable(1f));
    }

    IEnumerator KeepInvunerable(float time)
    {
        isVunerable = false;
        yield return new WaitForSeconds(time);
        isVunerable = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
