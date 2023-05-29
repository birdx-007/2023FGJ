using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBarController : MonoBehaviour
{
    public Image barContent;

    public void SetValue(float ratio)
    {
        barContent.fillAmount = ratio;
    }
}
