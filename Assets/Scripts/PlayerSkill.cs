using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkill : MonoBehaviour
{
    public bool canUseSkill;
    public float totalCD;
    protected float leftCD;
    public float CDratio;
    public UnityEvent OnSkillUsed;

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
}
