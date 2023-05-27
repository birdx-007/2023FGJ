using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    //死亡形象
    public GameObject deathModel;
    //速度
    public float speed = 10f;
    public float sprintSpeedMultiplier = 2f;

    public float jumpForce = 40f;
    public float maxStamina = 100f; // 最大体力值
    public float currentStamina;
    public float staminaRecoveryRate = 20f; // 恢复速率
    public float sprintStaminaCostPerSecond = 40f; // 加速消耗

    private Rigidbody2D rb;

    //跳跃
    public bool isJumping = false;
    public bool isSprinting = false;


    //攻击
    //设计攻击冷却时间
    public bool attacking = false;

    private int atttackmode;

    //生命
    public float currentHealth;
    public float maxHealth = 100f;
    bool isAlive;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        isAlive = true;
    }
//受伤
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        //TO DO
        //血量UI刷新

        if (currentHealth <= 0f && isAlive)
        {
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
    public void Move(float moveHorizontal, bool sprinting) // movehorizontal横向，sprinting是否奔跑
    {
        if (sprinting)
        {
            if (currentStamina > 0)
            {
                isSprinting = true;
                if (currentStamina <= 0)
                {
                    currentStamina = 0;
                    isSprinting = false;
                }
            }
            else
            {
                isSprinting = false;
            }
        }
        // 控制基础和加强速度之间的转换
        float currentSpeed = isSprinting ? speed * sprintSpeedMultiplier : speed;

        // 控制角色的运动
        //rb.velocity = movement * currentSpeed;
        rb.velocity = new Vector2(moveHorizontal * currentSpeed, rb.velocity.y);

        // 消耗体力（如果加速）或回复体力（如果不加速）
        if (isSprinting)
        {
            ConsumeStamina(sprintStaminaCostPerSecond * Time.fixedDeltaTime);
        }
        else
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
