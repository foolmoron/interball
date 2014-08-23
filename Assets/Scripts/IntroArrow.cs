using System.Net.Mime;
using UnityEngine;
using System.Collections;

public class IntroArrow : MonoBehaviour {

    public AnimationCurve ArrowRotationOffset;
    public float ArrowRotationAmount = 40;
    float animationTime;
    float originalRotation;

    void Start() {
        originalRotation = transform.localRotation.eulerAngles.z;
    }

    void Update() {
        animationTime += Time.deltaTime;
        var rotationOffset = ArrowRotationOffset.Evaluate(animationTime) * ArrowRotationAmount;
        var newRotation = Quaternion.Euler(0, 0, originalRotation + rotationOffset);
        transform.localRotation = newRotation;
    }
}
