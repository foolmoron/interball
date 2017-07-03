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
        GetComponent<Renderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        audioManager.PlayRandomTimeOrbSound();
        timeManager.CurrentTimeLeft += TimeBonus;
        timeMeter.Flash();
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}
