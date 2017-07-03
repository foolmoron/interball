using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [Range(1, 20)]
    public float Speed;
    float currentSpeed;

    Vector3 spawnPosition;

    void Start() {
        spawnPosition = transform.position;
        Reset();
    }

    void FixedUpdate() {
        // fire constantly to make sure ball never gets "stuck"
        SetSpeed(Speed);
    }

    public void Reset() {
        transform.position = spawnPosition;
        Stop();
    }

    public void Stop() {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().Sleep();
    }

    public void Move() {
        SetSpeed(Speed);
        SetDirectionVector(new Vector2(Random.value - 0.5f, Random.value - 0.5f));
    }

    public void SetSpeed(float speed) {
        currentSpeed = speed;
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * currentSpeed;
    }

    public void SetDirectionRadians(float rad) {
        SetDirectionVector(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)));
    }

    public void SetDirectionVector(Vector2 direction) {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * currentSpeed;
    }
}
