using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    [Range(1, 20)]
    public float Speed;
    float currentSpeed;
    [Range(0, 1080)]
    public float MaxAngularSpeed;
    [Range(0, 1080)]
    public float MinAngularSpeed;

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
        transform.localRotation = Quaternion.identity;
        Stop();
    }

    public void Stop() {
        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.Sleep();
    }

    public void Move() {
        SetSpeed(Speed);
        SetDirectionVector(new Vector2(Random.value - 0.5f, Random.value - 0.5f));
        SetRandomAngularVelocity();
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

    public void SetRandomAngularVelocity() {
        var angularSpeed = (MaxAngularSpeed - MinAngularSpeed) * Random.value + MinAngularSpeed;
        rigidbody2D.angularVelocity = Random.value >= 0.5f ? angularSpeed : -angularSpeed;
    }
}
