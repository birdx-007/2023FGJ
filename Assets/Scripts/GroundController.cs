using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform foregroundTransform;

    public float foregroundMoveRatio = 1.5f;
    public float transformXSum;

    private void Start()
    {
        transformXSum = cameraTransform.position.x + foregroundTransform.position.x;
    }

    void Update()
    {
        float deltaXCamera = cameraTransform.position.x - transformXSum;
        foregroundTransform.position = new Vector2(transformXSum - deltaXCamera * foregroundMoveRatio,
            foregroundTransform.position.y);
    }
}
