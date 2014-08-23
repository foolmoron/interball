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
        Move();
    }

    void Update() {
        if (Speed != currentSpeed) {
            SetSpeed(Speed);
        }
    }

    public void Reset() {
        transform.position = spawnPosition;
        Stop();
    }

    public void Stop() {
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.Sleep();
    }

    public void Move() {
        SetSpeed(Speed);
        SetDirectionVector(new Vector2(Random.value, Random.value));
    }

    public void SetSpeed(float speed) {
        currentSpeed = speed;
        rigidbody2D.velocity = rigidbody2D.velocity.normalized * currentSpeed;
    }

    public void SetDirectionRadians(float rad) {
        SetDirectionVector(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)));
    }

    public void SetDirectionVector(Vector2 direction) {
        rigidbody2D.velocity = direction.normalized * currentSpeed;
    }
}
