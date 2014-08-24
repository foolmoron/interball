using UnityEngine;
using System.Collections;

public class FrameShake : MonoBehaviour {

    [Range(0, 100)]
    public int ShakesRemaining;
    [Range(0, 1)]
    public float Strength = 0.05f;
    [Range(1, 10)]
    public int FrameInterval = 1;

    int frameCount;
    Vector3 previousShake;

    void Update() {
        if (Time.deltaTime == 0)
            return;

        frameCount++;
        if (frameCount % FrameInterval == 0) {
            if (ShakesRemaining > 0) {
                Shake();
                ShakesRemaining--;
            } else if (previousShake != Vector3.zero) {
                transform.localPosition = transform.localPosition - previousShake;
                previousShake = Vector3.zero;
            }
        }
    }

    void Shake() {
        transform.localPosition -= previousShake;
        Vector3 shake = Random.insideUnitCircle.normalized * Strength;
        transform.localPosition += shake;
        previousShake = shake;
    }
}
