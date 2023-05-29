using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Player : MonoBehaviour
{
    public GameObject deathModel;
    public Animator animator;
    public SpriteRenderer renderer;
    public GameObject playerBulletPrefab;
    public ParticleSystem teleportParticle;

    public PlayerSladeController sladeRight;
    public PlayerSladeController sladeLeft;
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
    private float faceDirection;

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
    public bool isAlive;

    
    // debuff
    public bool isPoisoned = false;
    public float poisonedTimeLeft = 0f;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        faceDirection = 1f;
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        isAlive = true;
        teleportTimer = 0;
        teleportParticle.Stop();
    }

    // 中毒debuff
    IEnumerator PoisonousGasEffect()
    {
        renderer.color = Color.green;
        while (poisonedTimeLeft > 0)
        {
            Debug.Log("PoisonousGasEffect: " + poisonedTimeLeft + "s left");
            TakeDamage(5f);
            poisonedTimeLeft -= 1;
            yield return new WaitForSeconds(1);
        }
        isPoisoned = false;
        renderer.color = Color.white;
    }

    public void PoisonousGasEffectOn()
    {
        if (!isVunerable)
        {
            return;
        }
        poisonedTimeLeft = 3f;
        if (isPoisoned)
            return;
        StartCoroutine(PoisonousGasEffect());
        Debug.Log("Get Poisoned!");
        isPoisoned = true;
    }
    //受伤
    public void TakeDamage(float amount)
    {
        if (!isVunerable)
        {
            Debug.Log("Player is invunerable!");
            return;
        }
        renderer.color = Color.red;

        IEnumerator RecoverColor()
        {
            yield return new WaitForSeconds(0.2f);
            if (isPoisoned)
            {
                renderer.color = Color.green;
            }
            else
            {
                renderer.color = Color.white;
            }
        }

        StartCoroutine(RecoverColor());
        animator.SetTrigger("hurt");
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
        renderer.enabled = false;
        rb.isKinematic = true;

        // 激活死亡形象
        //deathModel.SetActive(true);
        Manager.instance.GoToBadEnding();
    }

//
    public void Attack()
    {
        if (!isAlive) return;
        if (attacking) return;

        //攻击
        animator.SetTrigger("punch");
        attacking = true;
        StartCoroutine(ReleasePunch());
    }
    IEnumerator ReleasePunch(float time = 1f)
    {
        if (faceDirection > 0f)
        {
            sladeLeft.isValid = false;
            sladeRight.isValid = true;
        }
        else
        {
            sladeLeft.isValid = true;
            sladeRight.isValid = false;
        }
        yield return new WaitForSeconds(time);
        sladeLeft.isValid = false;
        sladeRight.isValid = false;
        attacking = false;
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
        if (!Mathf.Approximately(moveHorizontal, 0f))
        {
            faceDirection = moveHorizontal / Mathf.Abs(moveHorizontal);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        animator.SetFloat("direction", faceDirection);
        
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
        if (isTeleporting)
        {
            rb.velocity = new Vector2(faceDirection * currentSpeed, rb.velocity.y);
        }
        

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
        animator.SetTrigger("jump");
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
        animator.SetTrigger("teleport");
        isTeleporting = true;
        StartCoroutine(ReleasePunch(2f));
    }

    public void FireBullets()
    {
        animator.SetTrigger("fire");
        // Calculate the angle between bullets
        float angleStep = Mathf.PI / (bulletNum - 1);

        // Spawn multiple bullets
        for (int i = 0; i < bulletNum; i++)
        {
            Vector2 direction = Vector2.up * Mathf.Sin(i * angleStep) + Vector2.right * Mathf.Cos(i * angleStep);
            // Create a bullet instance
            GameObject bullet = Instantiate(playerBulletPrefab, rb.position + Vector2.up*0.5f, Quaternion.identity);
            var controller = bullet.GetComponent<PlayerBulletController>();
            controller.SetDirection(direction);
            controller.SetAngle(i*angleStep);
        }

        StartCoroutine(KeepInvunerable(2f));
    }

    IEnumerator KeepInvunerable(float time)
    {
        teleportParticle.Play();
        isVunerable = false;
        yield return new WaitForSeconds(time);
        isVunerable = true;
        teleportParticle.Stop();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
