using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum MonsterPhase
{
    Normal,
    Phase_1, // 只飞天
    Phase_2, // 飞天+毒雾
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

    // Sprites
    public string enemyName;
    public Sprite[] sprites;

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
        Debug.Log("Attacking...");
        if (
            phase == MonsterPhase.Normal
            || phase == MonsterPhase.IntoPhase_1
            || phase == MonsterPhase.OutofPhase_1
            || phase == MonsterPhase.IntoPhase_2
            || phase == MonsterPhase.OutofPhase_2
        )
        {
            // 从抛物线和直线中随机选择一种攻击方式
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                enemyShoot.ShootBullet(character.getVelocity(), BulletType.Normal, BulletTrack.StraightLine);
            }
            else
            {
                enemyShoot.ShootBullet(character.getVelocity(), BulletType.Normal, BulletTrack.Parabola);
            }

            interval = 1f;
        }
        else if (phase == MonsterPhase.Phase_1)
        {
            // 向下方直射子弹
            enemyShoot.ShootBullet(character.getVelocity(), BulletType.Normal, BulletTrack.Vertical);
            interval = 0.5f;
        }
        else if (phase == MonsterPhase.Phase_2)
        {
            // 向下方直射子弹，有毒雾
            enemyShoot.ShootBullet(character.getVelocity(), BulletType.Poison, BulletTrack.Vertical);
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

    bool FlyIntoSky(float destHeight)
    {
        character.setGravity(0);
        // 飞
        character.FlyIntoSky(destHeight);

        if (character.transform.position.y >= destHeight)
        {
            character.setVelocity(Vector2.zero);
            return true;
        }

        return false;
    }

    bool LandOnGround()
    {
        // 降落
        character.setGravity(10);
        character.Move(direct, false);

        if (character.transform.position.y <= 0)
        {
            character.setVelocity(Vector2.zero);
            return true;
        }

        return false;
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
                // 修改character的模型，从圆形变成方形，同时修改碰撞体积
                character.setVelocity(Vector2.zero);
                VerticallyChase();
                break;
            case MonsterPhase.Phase_2:
                // 修改character的模型，从圆形变成方形，同时修改碰撞体积
                character.setVelocity(Vector2.zero);
                VerticallyPatrol();
                break;
            case MonsterPhase.Phase_3:
                break;
            case MonsterPhase.IntoPhase_1:
                if (FlyIntoSky(2.5f))
                    phase = MonsterPhase.Phase_1;
                break;
            case MonsterPhase.OutofPhase_1:
                if (LandOnGround())
                    phase = MonsterPhase.Normal;
                break;
            case MonsterPhase.IntoPhase_2:
                if (FlyIntoSky(2.5f))
                    phase = MonsterPhase.Phase_2;
                break;
            case MonsterPhase.OutofPhase_2:
                if (LandOnGround())
                    phase = MonsterPhase.Normal;
                break;
            case MonsterPhase.IntoPhase_3:
                break;
            case MonsterPhase.OutofPhase_3:
                break;
        }

        // 更新phase
        if (enemyName == "boss_1")
        {
            return;
        }
        else if (enemyName == "boss_2")
        {
            if (attackCount == 10 && phase == MonsterPhase.Normal)
            {
                phase = MonsterPhase.IntoPhase_1;
                ChangeSprite(2);
                attackCount = 0;
            }
            else if (attackCount == 30 && phase == MonsterPhase.Phase_1)
            {
                phase = MonsterPhase.OutofPhase_1;
                ChangeSprite(0);
                attackCount = 0;
            }
        }
        else if (enemyName == "boss_3")
        {
            if (attackCount == 10 && phase == MonsterPhase.Normal)
            {
                phase = MonsterPhase.IntoPhase_2;
                ChangeSprite(1);
                attackCount = 0;
            }
            else if (attackCount == 30 && phase == MonsterPhase.Phase_2)
            {
                phase = MonsterPhase.OutofPhase_2;
                ChangeSprite(0);
                attackCount = 0;
            }
        }
    }
}