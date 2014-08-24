using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public float TimeAlive;
    public int WorldsEncountered;
    public bool CountingTime;

    public void Reset() {
        WorldsEncountered = 0;
        TimeAlive = 0;
        CountingTime = false;
    }

    void Update() {
        if (CountingTime) {
            TimeAlive += Time.deltaTime;
        }
    }

    public void SaveScores() {
        PlayerPrefs.SetFloat("hightime", Mathf.Max(TimeAlive, PlayerPrefs.GetFloat("hightime", 0)));
        PlayerPrefs.SetInt("highworlds", Mathf.Max(WorldsEncountered, PlayerPrefs.GetInt("highworlds", 0)));
    }
}
