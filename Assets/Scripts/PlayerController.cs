using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
* ����������������ٶȡ����ٺ��������ģ������ƶ����ģ���������ظ�������
* ���ټ�����
* 
*/
public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // �����ƶ��ٶ�
    public float sprintSpeedMultiplier = 2f; // ����ʱ�ı���
    public int maxStamina = 100; // �������ֵ
    public int staminaRecoveryRate = 3; // �ָ�����
    public int sprintStaminaCostPerSecond = 5; // ��������

    private Rigidbody2D rb;
    private bool isSprinting = false;
    private int currentStamina;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentStamina = maxStamina;
    }

    // ����û��Ƿ��¡�Z�����Լӿ��ɫ�ٶȣ�������������㣩
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

    // ���������������Ľ�ɫ�˶��߼����������ƻ������ǿģʽ�ٶ��Լ�����ʣ��������
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // ���ƻ����ͼ�ǿ�ٶ�֮���ת��
        float currentSpeed = isSprinting ? speed * sprintSpeedMultiplier : speed;

        // ���ƽ�ɫ���˶�
        rb.velocity = movement * currentSpeed;

        // ����������������٣���ظ���������������٣�
        if (isSprinting)
        {
            ConsumeStamina(sprintStaminaCostPerSecond * Time.fixedDeltaTime);
        }
        else
        {
            RecoverStamina(staminaRecoveryRate * Time.fixedDeltaTime);
        }
    }

    // ���ٵ�ǰʣ������ֵ��ͬʱ������Ҫֹͣ���١�
    private void ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= (int)amount;
        }
        else
        {
            currentStamina = 0;
            isSprinting = false; // ֹͣ����
        }
    }

    // ���ӵ�ǰʣ������ܣ�����������ֵ����ȥ�� maximum��
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
