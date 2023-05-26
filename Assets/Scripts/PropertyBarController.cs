using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropertyBarController : MonoBehaviour
{
    public Image barMask;
    public Image barContent;
    public float fullLength;
    public float currentLength;

    private void Awake()
    {
        currentLength = fullLength = barContent.rectTransform.rect.width;
    }

    public void SetValue(float ratio)
    {
        currentLength = fullLength * ratio;
        Vector2 offsetMax = barMask.rectTransform.offsetMax;
        offsetMax.x = currentLength - fullLength;
        barMask.rectTransform.offsetMax = offsetMax;
    }
}
