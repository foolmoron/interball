using UnityEngine;
using System.Collections;

public class Bumper : MonoBehaviour {

    public enum BumperMode { NormalBounce, ForceDirection }
    public BumperMode Mode = BumperMode.NormalBounce;

    [Range(0, 360)]
    public float DirectionOffset = 0;

    FrameShake frameShake;

	void Start() {
	    frameShake = FindObjectOfType<FrameShake>();
	}
	
	void Update() {
	}

    void OnCollisionEnter2D(Collision2D other) {
        var ball = other.gameObject.GetComponent<Ball>();
        if (ball) {
            switch (Mode) {
                case BumperMode.NormalBounce:
                    break;
                case BumperMode.ForceDirection:
                    var realOffset = DirectionOffset * Mathf.Deg2Rad + Mathf.PI / 2;
                    var transformedDirection = transform.TransformDirection(Mathf.Cos(realOffset), Mathf.Sin(realOffset), 0);
                    ball.SetDirectionVector(transformedDirection);
                    break;
            }
            if (frameShake.ShakesRemaining < 2) { // queue up 1 extra shake at most
                frameShake.ShakesRemaining++;
            }
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = new Color(1, 1, 0, 0.5f);
        switch (Mode) {
            case BumperMode.NormalBounce: // nothing extra
                break;
            case BumperMode.ForceDirection: // line showing what direction it bounces
                var realOffset = DirectionOffset * Mathf.Deg2Rad + Mathf.PI / 2;
                var transformedDirection = transform.TransformDirection(Mathf.Cos(realOffset), Mathf.Sin(realOffset), 0);
                Gizmos.DrawLine(transform.position, transform.position + transformedDirection);
                Gizmos.DrawWireSphere(transform.position + transformedDirection, 0.1f);
                break;
        }
    }
}
