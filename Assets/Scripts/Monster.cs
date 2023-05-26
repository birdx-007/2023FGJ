using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster
{
    private int health;
    private int attackPower;

    private int attackmode;


    public void NormalAttack(Player player)
    {
        // 普通攻击逻辑
        player.TakeDamage(attackPower);
    }

    public void SkillAttack(Player player)
    {
        // 技能攻击逻辑
        // 可以通过判断怪物与玩家之间的距离来确定攻击范围
        player.TakeDamage(attackPower * 2);
    }
}
