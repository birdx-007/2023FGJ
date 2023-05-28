using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterPhase
{
    Normal,
    Phase_1,
    Phase_2,
    Phase_3,
    IntoPhase_1,
    OutofPhase_1,
    IntoPhase_2,
    OutofPhase_2,
    IntoPhase_3,
    OutofPhase_3,
};

[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(EnemyShootController))]
public class MosterController : MonoBehaviour
{
    EnemyShootController enemyShoot;
    PlayerCharacter character;
    Transform targetTrans;
    public float direct;

    //记录攻击方式进行的次数
    public int attackCount = 0;

    // 当前所处的阶段
    public MonsterPhase phase = MonsterPhase.Normal;

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
        // if (attackCount >= 3)
        // {
        //     enemyShoot.ShootBullet_StraightLine();
        //     attackCount = 0;
        // }
        // else
        // {
        //     enemyShoot.ShootBullet_Parabola();
        //     attackCount++;
        // }
        enemyShoot.ShootBullet_Parabola();
        attackCount++;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Attack();
        }
    }

    void VerticallyChase()
    {
        // character.Move(1f, false);
        direct = targetTrans.position.x - character.transform.position.x;
        character.Move(direct, false); //参数：方向，是否跳跃
    }

    void VerticallyPatrol()
    {
        if (character.transform.position.x <= -10)
        {
            direct = 3;
        }
        else if (character.transform.position.x >= 10)
        {
            direct = -3;
        }
        character.Move(direct, false); //参数：方向，是否跳跃
    }

    void FlyIntoSky(float destHeight)
    {
        character.setGravity(0);
        // 飞
        character.FlyIntoSky(destHeight);

        if (character.transform.position.y >= destHeight)
        {
            character.setVelocity(Vector2.zero);
            phase = MonsterPhase.Phase_1;
        }
    }

    void LandOnGround()
    {
        // 降落
        character.setGravity(10);
        character.Move(direct, false);

        if (character.transform.position.y <= 0)
        {
            character.setVelocity(Vector2.zero);
            phase = MonsterPhase.Normal;
        }
    }

    void Update()
    {
        //Debug.Log(direct);
        switch (phase)
        {
            case MonsterPhase.Normal:
                VerticallyChase();
                break;
            case MonsterPhase.Phase_1:
                VerticallyPatrol();
                break;
            case MonsterPhase.Phase_2:
                break;
            case MonsterPhase.Phase_3:
                break;
            case MonsterPhase.IntoPhase_1:
                FlyIntoSky(2.5f);
                break;
            case MonsterPhase.OutofPhase_1:
                LandOnGround();
                break;
            case MonsterPhase.IntoPhase_2:
                break;
            case MonsterPhase.OutofPhase_2:
                break;
            case MonsterPhase.IntoPhase_3:
                break;
            case MonsterPhase.OutofPhase_3:
                break;
        }

        // 更新phase
        if (attackCount == 5)
        {
            phase = MonsterPhase.IntoPhase_1;
        }
    }
}
