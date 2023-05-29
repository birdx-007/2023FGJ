using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(EnemyShootController))]
public class OctopusController : MonoBehaviour
{
    public PropertyBarController healthBar;
    EnemyShootController enemyShoot;
    PlayerCharacter character;
    Transform targetTrans;
    public float direct;
    

    //记录攻击方式进行的次数
    public int attackCount = 0;

    // 当前所处的阶段
    public MonsterPhase phase = MonsterPhase.Normal;
    public float interval = 1f;

    void Start()
    {
        enemyShoot = GetComponent<EnemyShootController>();
        character = GetComponent<PlayerCharacter>();
        targetTrans = GameObject.FindGameObjectWithTag("Player").transform; //玩家名
        //攻击循环
        StartCoroutine(AttackLoop());
    }

    public void Attack()
    {
        enemyShoot.ShootBullet_Parabola(targetTrans.position - transform.position,80f);
        character.animator.SetTrigger("bubble");
        attackCount++;
        if (attackCount % 3 == 0)
        {
            interval = 7f;
        }
        else
        {
            interval = 2.1f;
        }
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            if (!character.isAlive)
            {
                break;
            }
            yield return new WaitForSeconds(interval);
            if (!character.isAlive)
            {
                break;
            }
            Attack();
        }
    }

    void ChangeSprite(int idx)
    {
        Debug.Log("ChangeSprite");
        character.ChangeSprite(idx);
    }

    void Update()
    {
        healthBar.SetValue(character.currentHealth/character.maxHealth);
    }
}
