using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerSkill))]
public class SkillController : MonoBehaviour
{
    private PlayerSkill skill;
    public Image CDFillImage;
    private Button skillButton;
    private void Awake()
    {
        skill = GetComponent<PlayerSkill>();
        skillButton = GetComponent<Button>();
    }

    private void Update()
    {
        CDFillImage.fillAmount = skill.CDratio;
        skillButton.interactable = skill.canUseSkill;
    }

    public void Use() // exposed to player
    {
        if (skill == null)
        {
            return;
        }
        skill.ResetCD();
        skill.OnSkillUsed.Invoke();
    }
}
