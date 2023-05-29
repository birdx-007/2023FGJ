using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(EnemyShootController))]
public class WolfController : MonoBehaviour
{
    public PropertyBarController healthBar;
    EnemyShootController enemyShoot;
    PlayerCharacter character;
    Transform targetTrans;
    public float direct;
    private bool isChanging = false;
    public float maxChangeTime = 3f;
    public float changeTimer = 0f;

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
        if (phase == MonsterPhase.Normal)
        {
            enemyShoot.ShootBullet_StraightLine();
            character.animator.SetTrigger("throw");
            interval = 4.5f;
        }
        else if (phase == MonsterPhase.Phase_2)
        {
            // 向下方直射子弹
            //enemyShoot.ShootBullet_Vertically_Down();
            // 向下方随即角度散射抛物线子弹
             int random = Random.Range(1, 181);
             for (int i = 0; i < 10; i++)
             {
                 Vector2 direction = new Vector2(
                     Mathf.Cos(random) + direct * 0.1f,
                     -Mathf.Sin(random)
                 );
                 enemyShoot.ShootBullet_Parabola(direction);
             }
             interval = 0.5f;
        }
        attackCount++;
    }

    IEnumerator AttackLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (!character.isAlive)
            {
                break;
            }
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
        if (character.transform.position.x <= -18)
        {
            direct = 3;
        }
        else if (character.transform.position.x >= 18)
        {
            direct = -3;
        }
        character.Move(direct, false); //参数：方向，是否跳跃
    }

    void FlyIntoSky(float destHeight)
    {
        character.setGravity(0);
        // 飞
        GetComponent<Rigidbody2D>().AddForce(10f*Vector2.up,ForceMode2D.Force);

        if (character.transform.position.y >= destHeight)
        {
            character.setVelocity(Vector2.zero);
            phase = MonsterPhase.Phase_2;
        }
    }

    IEnumerator ChangeToCloud()
    {
        isChanging = true;
        yield return new WaitForSeconds(3f);
        isChanging = false;
        phase = MonsterPhase.Phase_1;
    }

    void Update()
    {
        //Debug.Log(direct);
        if (phase == MonsterPhase.Normal)
        {
            VerticallyPatrol();
        }
        else if (phase == MonsterPhase.IntoPhase_1) // 变身
        {
            if (!isChanging)
            {
                character.animator.SetTrigger("change");
                isChanging = true;
            }
            else
            {
                changeTimer += Time.deltaTime;
                if (changeTimer >= maxChangeTime)
                {
                    changeTimer = 0f;
                    phase = MonsterPhase.Phase_1;
                }
            }
        }
        else if (phase == MonsterPhase.Phase_1)
        {
            FlyIntoSky(3f);
        }
        else if (phase == MonsterPhase.Phase_2)
        {
            VerticallyPatrol();
        }
        healthBar.SetValue(character.currentHealth/character.maxHealth);
        // 更新phase
        if (character.currentHealth < character.maxHealth * 0.6f && !isChanging)
        {
            phase = MonsterPhase.IntoPhase_1;
        }
    }
}
