using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSkillType
{
    None = 0,
    Skill_1
}

public abstract class PlayerSkill : MonoBehaviour
{
    public bool canUseSkill;
    protected float totalCD;
    protected float leftCD;
    public float CDratio;

    void Start()
    {
        leftCD = 0f;
        canUseSkill = true;
    }

    void Update()
    {
        if (!canUseSkill)
        {
            leftCD -= Time.deltaTime;
        }

        if (leftCD <= 0f)
        {
            leftCD = 0f;
            canUseSkill = true;
        }

        CDratio = leftCD / totalCD;
    }

    public void ResetCD()
    {
        leftCD = totalCD;
        canUseSkill = false;
    }

    public abstract void OnSkillUsed();
}

public class PlayerSkill_1 : PlayerSkill
{
    private void Awake()
    {
        totalCD = 0.5f;
        canUseSkill = true;
    }

    public override void OnSkillUsed()
    {
        //throw new NotImplementedException();
    }
}
