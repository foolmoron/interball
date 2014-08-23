using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public float TimeAlive;
    public int WorldsEncountered;
    public bool CountingTime;

    void Update() {
        if (CountingTime) {
            TimeAlive += Time.deltaTime;
        }
    }
}
