using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotatable : MonoBehaviour {

    new Camera camera;
    AudioSource rotationSound;

    [Range(1f, 20f)]
    public float MaxRotationPerFrame = 10f;
    public bool RotationEnabled;
	public bool Rotating {get; private set;}
    float previousAngle;

    [Range(1, 100)]
    public int RotationSoundSamples = 10;
    [Range(0, 1)]
    public float RotationMinVolume = 0.25f;
    [Range(0, 1)]
    public float RotationMaxVolume = 1f;
    [Range(0, 20)]
    public float MaxRotationMaxVolume = 10;
    [Range(0, 1)]
    public float MinRotationMinVolume = 0.1f;
    Queue<float> rotationMagnitudes = new Queue<float>();
    
	void Start() {
        camera = Camera.main;
        rotationSound = GetComponent<AudioSource>();
		Rotating = false;
	}

	void Update() {
	    if (!RotationEnabled) {
            Rotating = false;
	        return;
	    }

	    if (Input.GetMouseButtonDown(0)) {
            Rotating = true;
            var mouseNormalized = (camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            previousAngle = Mathf.Atan2(mouseNormalized.y, mouseNormalized.x) * Mathf.Rad2Deg;
		} else if (Input.GetMouseButtonUp(0)) {
			Rotating = false;
		}
	}

    void FixedUpdate() {
        if (Rotating) {
            var mouseNormalized = (camera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            var mouseAngle = Mathf.Atan2(mouseNormalized.y, mouseNormalized.x) * Mathf.Rad2Deg;

            var newRotation = Mathf.MoveTowardsAngle(previousAngle, mouseAngle, MaxRotationPerFrame);
            var rotationDelta = newRotation - previousAngle;
            transform.Rotate(0, 0, rotationDelta);
            previousAngle = newRotation;

            rotationMagnitudes.Enqueue(Mathf.Abs(rotationDelta));
        } else {
            rotationMagnitudes.Enqueue(0);
        }
        while (rotationMagnitudes.Count > RotationSoundSamples)
            rotationMagnitudes.Dequeue();

        var rotationAverage = (rotationMagnitudes.Count > 0) ? rotationMagnitudes.Average() : 0;
        var volume = (rotationAverage > MinRotationMinVolume) ? Mathf.Lerp(RotationMinVolume, RotationMaxVolume, rotationAverage / MaxRotationMaxVolume) : 0;
        rotationSound.volume = volume;
    }
}