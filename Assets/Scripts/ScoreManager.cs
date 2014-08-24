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
}
