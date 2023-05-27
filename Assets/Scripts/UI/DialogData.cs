using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//DialogData.cs
[CreateAssetMenu(fileName = "DailogData", menuName = "Dialog/DailogData")]
public class DialogData : ScriptableObject
{
    public List<string> dialogList;
}