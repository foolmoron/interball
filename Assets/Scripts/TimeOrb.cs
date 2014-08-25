using System.Net.Mime;
using UnityEngine;
using System.Collections;

public class TimeOrb : MonoBehaviour {

    AudioManager audioManager;
    TimeManager timeManager;
    TimeMeter timeMeter;
    [Range(0, 10)]
    public float TimeBonus = 1f;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        timeManager = FindObjectOfType<TimeManager>();
        timeMeter = FindObjectOfType<TimeMeter>();
	    Reset();
	}

    public void Reset() {
        renderer.enabled = true;
        collider2D.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        audioManager.PlayRandomTimeOrbSound();
        timeManager.CurrentTimeLeft += TimeBonus;
        timeMeter.Flash();
        renderer.enabled = false;
        collider2D.enabled = false;
    }
}
