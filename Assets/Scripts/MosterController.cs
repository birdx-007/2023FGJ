using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MosterController : MonoBehaviour
{
    PlayerCharacter character;
    Transform targetTrans;
    float direct ;
    //记录攻击方式进行的次数
    private int attackCount;

    void Start ()
    {
        character = GetComponent<PlayerCharacter>();

        targetTrans = GameObject.FindGameObjectWithTag("Player").transform; //玩家名
        //攻击循环
        StartCoroutine(AttackLoop());

        
        
    } 
    public void Attack()
    {
        if (attackCount >= 3)
        {
            Shoot_StraightLine();
            attackCount = 0;
        }
        else
        {
            Shoot_Parabola();
            attackCount++;
        }
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Attack();
        }
    }



    //怪物攻击
    void Shoot_StraightLine(){
        EnemyShoot_StraightLine bullet;
        bullet.ShootBullet();
    }
    void Shoot_Parabola(){
        EnemyShoot_Parabola bullet;
        bullet.ShootBullet();
    }



	void Update ()
    {
        direct = targetTrans.position.x -character.transform.position.x;
        Debug.Log(direct);
        character.Move(direct,false);

    }
}
