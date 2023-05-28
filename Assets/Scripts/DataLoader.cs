using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataLoader
{
    public TextAsset dataFile;

    void Start()
    {
        dataFile = Resources.Load("Data") as TextAsset;
        string[] data = dataFile.text.Split(new char[] { '\n' });
        foreach (string line in data)
        {
            Debug.Log(line);
        }
    }

    void Update()
    {

    }

    public void LoadData()
    {
        dataFile = Resources.Load("Data") as TextAsset;
        string[] data = dataFile.text.Split(new char[] { '\n' });
        foreach (string line in data)
        {
            Debug.Log(line);
        }
    }
}