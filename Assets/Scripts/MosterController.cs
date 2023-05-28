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
        if (
            phase == MonsterPhase.Normal
            || phase == MonsterPhase.IntoPhase_1
            || phase == MonsterPhase.OutofPhase_1
        )
        {
            // 从抛物线和直线中随机选择一种攻击方式
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                enemyShoot.ShootBullet_Parabola(targetTrans.position - transform.position);
            }
            else
            {
                enemyShoot.ShootBullet_StraightLine();
            }
            interval = 1f;
        }
        else if (phase == MonsterPhase.Phase_1)
        {
            // 向下方直射子弹
            enemyShoot.ShootBullet_Vertically_Down();
            // 向下方随即角度散射抛物线子弹
            // int random = Random.Range(1, 181);
            // for (int i = 0; i < 10; i++)
            // {
            //     Vector2 direction = new Vector2(
            //         Mathf.Cos(random) + direct * 0.1f,
            //         -Mathf.Sin(random)
            //     );
            //     enemyShoot.ShootBullet_Parabola(direction);
            // }
            interval = 0.5f;
        }
        attackCount++;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
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
            // 目标图片在
            phase = MonsterPhase.Phase_1;
            ChangeSprite(1);
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

    void ChangeSprite(int idx)
    {
        Debug.Log("ChangeSprite");
        character.ChangeSprite(idx);
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
                // 修改character的模型，从圆形变成方形，同时修改碰撞体积，使其能够覆盖整个
                character.setVelocity(Vector2.zero);
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
