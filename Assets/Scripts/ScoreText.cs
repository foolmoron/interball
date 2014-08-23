using UnityEngine;
using System.Collections;

public class ScoreText : MonoBehaviour {

    ScoreManager scoreManager;
    UnityEngine.UI.Text text;

    void Start() {
        scoreManager = FindObjectOfType<ScoreManager>();
        text = GetComponent<UnityEngine.UI.Text>();
    }

    void Update() {
        text.text = scoreManager.TimeAlive.ToString("0.00");
    }
}