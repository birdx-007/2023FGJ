using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
* 设置人物基础基础速度、加速和体力消耗（加速移动消耗，其他情况回复体力）
* 加速键设置
* 
*/
[RequireComponent(typeof(PlayerCharacter))]
public class PlayerController : MonoBehaviour
{
    PlayerCharacter character;

    void Start()
    {
        character = GetComponent<PlayerCharacter>();
    }

    // 检测用户是否按下“Z”键以加快角色速度（如果其体力充足）
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            character.isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            character.isSprinting = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            character.Jump();
        }
    }

    // 处理基于物理引擎的角色运动逻辑，包括控制基础或加强模式速度以及更新剩余体力。
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        character.Move(moveHorizontal,character.isSprinting);
    }
}
