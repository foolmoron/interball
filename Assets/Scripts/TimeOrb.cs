using System.Net.Mime;
using UnityEngine;
using System.Collections;

public class TimeOrb : MonoBehaviour {

    AudioManager audioManager;
    TimeManager timeManager;
    TimeMeter timeMeter;

    [Range(0, 10)]
    public float TimeBonus = 1f;

    public AnimationCurve RotationAnimation;
    float animationTime;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        timeManager = FindObjectOfType<TimeManager>();
        timeMeter = FindObjectOfType<TimeMeter>();
	    Reset();
	}

    void Update() {
        animationTime += Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, 0, RotationAnimation.Evaluate(animationTime) * 360);
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
