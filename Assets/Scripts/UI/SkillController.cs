using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public PlayerSkillType skillType;
    public PlayerSkill skill;
    public Image CDFillImage;
    private Button skillButton;
    private void Awake()
    {
        switch (skillType)
        {
            case PlayerSkillType.Skill_1:
            {
                skill = gameObject.AddComponent<PlayerSkill_1>();
                break;
            }
            default:
            {
                skill = null;
                break;
            }
        }

        if (skill == null)
        {
            Debug.LogError("No player skill is assigned to " + gameObject.name);
        }

        skillButton = GetComponent<Button>();
    }

    private void Update()
    {
        CDFillImage.fillAmount = skill.CDratio;
        skillButton.interactable = skill.canUseSkill;
    }

    public void Use()
    {
        if (skill == null)
        {
            return;
        }
        skill.ResetCD();
        skill.OnSkillUsed();
    }
}
