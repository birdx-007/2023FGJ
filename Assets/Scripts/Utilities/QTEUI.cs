using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTEUI : MonoBehaviour
{
    public Text promptText;          // Text element to display the QTE prompt
    public Image feedbackImage;      // Image element for providing visual feedback
    public Image fillImage;

    public void ResetUI()
    {
        promptText.text = "";
        feedbackImage.color = Color.red;
    }

    public void UpdateQTEFillImage(float ratio)
    {
        fillImage.fillAmount = ratio;
    }

    public void DisplayQTEPrompt(string prompt)
    {
        // Display the QTE prompt text
        promptText.text = prompt;
    }

    public void DisplayQTEFeedback(bool success)
    {
        // Display visual feedback based on QTE success
        feedbackImage.color = success ? Color.green : Color.red;
    }
}
