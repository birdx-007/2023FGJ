using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Subtitles : MonoBehaviour
{
        public Text subtitleText; 
    public TextAsset subtitlesAsset; 
    private float[] timeStamps; 
    private string[] texts;
    void Start() {
    LoadSubtitles();
    StartSubtitles();
    }

void LoadSubtitles() {
    string[] lines = subtitlesAsset.text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
    timeStamps = new float[lines.Length];
    texts = new string[lines.Length];
    for (int i = 0; i < lines.Length; i++) {
        string line = lines[i];
        int timeEnd = line.IndexOf("]");
        string timeString = line.Substring(1, timeEnd - 1);
        float timeStamp = float.Parse(timeString);
        string text = line.Substring(timeEnd + 1);
        timeStamps[i] = timeStamp;
        texts[i] = text;
    }
}

void StartSubtitles() {
    for (int i = 0; i < timeStamps.Length; i++) {
        StartCoroutine(ShowSubtitle(timeStamps[i], texts[i]));
    }
}

IEnumerator ShowSubtitle(float timeStamp, string text) {
    yield return new WaitForSeconds(timeStamp);
    subtitleText.text = text;
}

}
