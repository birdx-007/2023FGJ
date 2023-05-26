using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* 设置人物基础基础速度、加速和体力消耗（加速移动消耗，其他情况回复体力）
* 加速键设置
* 
*/
public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // 基础移动速度
    public float sprintSpeedMultiplier = 2f; // 加速时的倍数
    public int maxStamina = 100; // 最大体力值
    public int staminaRecoveryRate = 3; // 恢复速率
    public int sprintStaminaCostPerSecond = 5; // 加速消耗

    private Rigidbody2D rb;
    private bool isSprinting = false;
    private int currentStamina;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    // 检测用户是否按下“Z”键以加快角色速度（如果其体力充足）
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && currentStamina > 0)
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isSprinting = false;
        }
    }

    // 处理基于物理引擎的角色运动逻辑，包括控制基础或加强模式速度以及更新剩余体力。
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // 控制基础和加强速度之间的转换
        float currentSpeed = isSprinting ? speed * sprintSpeedMultiplier : speed;

        // 控制角色的运动
        rb.velocity = movement * currentSpeed;

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

    // 减少当前剩余体力值，同时根据需要停止加速。
    private void ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= (int)amount;
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
            currentStamina += (int)amount;

            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }
}
