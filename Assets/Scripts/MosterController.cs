using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(EnemyShootController))]
public class MosterController : MonoBehaviour
{
    EnemyShootController enemyShoot;
    PlayerCharacter character;
    Transform targetTrans;
    float direct ;
    //记录攻击方式进行的次数
    private int attackCount = 0;

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
        if (attackCount >= 3)
        {
            enemyShoot.ShootBullet_StraightLine();
            attackCount = 0;
        }
        else
        {
            enemyShoot.ShootBullet_Parabola();
            attackCount++;
        }
    }
    
    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Attack();
        }
    }

    void Update ()
    {
        direct = targetTrans.position.x -character.transform.position.x;
        //Debug.Log(direct);
        character.Move(direct,false);
    }
}
