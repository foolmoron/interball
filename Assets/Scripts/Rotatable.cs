using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotatable : MonoBehaviour {

	new Camera camera;

    [Range(1f, 20f)]
    public float MaxRotationPerFrame = 10f;
    public bool RotationEnabled;
	public bool Rotating {get; private set;}
    float previousAngle;

	void Start() {
	    camera = Camera.main;
		Rotating = false;
	}

	void Update() {
	    if (!RotationEnabled)
	        return;

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
            transform.Rotate(0, 0, newRotation - previousAngle);
            previousAngle = newRotation;
        }
    }
}