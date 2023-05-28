using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public Dialog dialog;

    // Start is called before the first frame update
    void Start()
    {
        dialog.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
    }
}