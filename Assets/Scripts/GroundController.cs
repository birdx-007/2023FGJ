using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform foregroundTransform;

    public SpriteRenderer foregroundRenderer;
    public SpriteRenderer backgroundRenderer;

    public Sprite dirtyForeground;
    public Sprite cleanForeground;
    public Sprite dirtyBackground;
    public Sprite cleanBackground;

    public float foregroundMoveRatio = 1.5f;
    private float transformXSum;

    private void Start()
    {
        SetRendererSprite(false);
        transformXSum = cameraTransform.position.x + foregroundTransform.position.x;
    }

    void Update()
    {
        float deltaXCamera = cameraTransform.position.x - transformXSum;
        foregroundTransform.position = new Vector2(transformXSum - deltaXCamera * foregroundMoveRatio,
            foregroundTransform.position.y);
    }

    public void SetRendererSprite(bool isClean)
    {
        if (isClean)
        {
            backgroundRenderer.sprite = cleanBackground;
            if (foregroundRenderer.sprite != null)
            {
                foregroundRenderer.sprite = cleanForeground;
            }
        }
        else
        {
            backgroundRenderer.sprite = dirtyBackground;
            if (foregroundRenderer.sprite != null)
            {
                foregroundRenderer.sprite = dirtyForeground;
            }
        }
    }
}
