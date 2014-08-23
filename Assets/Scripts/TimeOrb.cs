using System.Net.Mime;
using UnityEngine;
using System.Collections;

public class TimeOrb : MonoBehaviour {

    TimeManager timeManager;
    [Range(0, 10)]
    public float TimeBonus = 1f;

	void Start() {
	    timeManager = FindObjectOfType<TimeManager>();
	    Reset();
	}

    public void Reset() {
        renderer.enabled = true;
        collider2D.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        timeManager.CurrentTimeLeft += TimeBonus;
        renderer.enabled = false;
        collider2D.enabled = false;
    }
}
