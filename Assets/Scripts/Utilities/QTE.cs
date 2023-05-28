using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[System.Serializable]
public struct QTEInput
{
    public int index;        // Order of the input in the sequence
    public string prompt;    // Text or visual cue for the input
    public KeyCode key;      // Key associated with the input
}

public class QTE : MonoBehaviour
{
    // Define the QTE inputs and their associated keys
    public QTEInput[] qteInputs;

    // Time window for input detection
    public float inputDetectionWindow = 0.5f;

    // Reference to UI elements for displaying QTE prompts and feedback
    public QTEUI qteUI;

    private bool qteActive = false;
    private int currentQTEIndex = 0;
    private float qteTimer = 0f;
    public UnityEvent onQTECompleted;

    private void Start()
    {
        qteTimer = inputDetectionWindow;
        qteUI.enabled = false;
    }

    private void Update()
    {
        if (qteActive)
        {
            qteTimer -= Time.deltaTime;
            
            if (qteTimer <= 0f)
            {
                qteTimer = 0f;
                // Handle failure condition if time window expires
                QTEFailed();
            }

            qteUI.UpdateQTEFillImage(qteTimer / inputDetectionWindow);

            // Check for QTE input
            foreach (QTEInput input in qteInputs)
            {
                if (Input.GetKeyDown(input.key))
                {
                    if (input.index == currentQTEIndex)
                    {
                        // Handle successful QTE input
                        QTESuccessful();
                    }
                    else
                    {
                        // Handle failure condition if wrong input
                        QTEFailed();
                    }
                }
            }
        }
    }

    public void StartQTE()
    {
        // Initialize QTE sequence
        currentQTEIndex = 0;
        qteActive = true;
        qteTimer = inputDetectionWindow;

        // Display QTE prompt
        qteUI.enabled = true;
        qteUI.ResetUI();
        qteUI.DisplayQTEPrompt(qteInputs[currentQTEIndex].prompt);
    }

    private void QTESuccessful()
    {
        // Proceed to the next QTE input
        currentQTEIndex++;

        if (currentQTEIndex >= qteInputs.Length)
        {
            // Complete QTE if all inputs are successful
            QTECompleted();
        }
        else
        {
            // Display next QTE prompt
            qteUI.DisplayQTEPrompt(qteInputs[currentQTEIndex].prompt);
            qteTimer = inputDetectionWindow;
        }
    }

    private void QTEFailed()
    {
        // Handle failure condition
        qteActive = false;
        qteUI.DisplayQTEFeedback(false);

        // Perform necessary actions on failure (e.g., penalty, reset, etc.)

        // Reset QTE sequence
        currentQTEIndex = 0;
    }

    private void QTECompleted()
    {
        // Handle successful completion of the QTE
        qteActive = false;
        qteUI.UpdateQTEFillImage(0f);
        qteUI.DisplayQTEFeedback(true);

        onQTECompleted.Invoke();
        // Perform necessary actions on successful completion
    }
}
