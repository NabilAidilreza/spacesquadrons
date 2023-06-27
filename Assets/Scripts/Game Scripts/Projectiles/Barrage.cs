using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrage : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public float Acceleration;
    private Rigidbody2D rb;
    public float RotationControl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Target != null)
        {
            Vector2 Dir = transform.position - Target.position;
            Dir.Normalize();
            float cross = Vector3.Cross(Dir, transform.up).z;
            rb.angularVelocity = RotationControl * cross;

            Vector2 Vel = transform.up * 1.0f * Acceleration;
            rb.AddForce(Vel);

            float thrust = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right)) * 1.0f;

            Vector2 RelForce = Vector2.up * thrust;

            rb.AddForce(rb.GetRelativeVector(RelForce));

            if (rb.velocity.magnitude > Speed)
            {
                rb.velocity = rb.velocity.normalized * Speed;
            }
        }
        else
        {
            Vector2 Dir = transform.up;
            Dir.Normalize();
            float cross = Vector3.Cross(Dir, transform.up).z;
            rb.angularVelocity = RotationControl * cross;

            Vector2 Vel = transform.up * 1.0f * Acceleration;
            rb.AddForce(Vel);

            float thrust = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.right)) * 1.0f;

            Vector2 RelForce = Vector2.down * thrust;

            rb.AddForce(rb.GetRelativeVector(RelForce));

            if (rb.velocity.magnitude > Speed)
            {
                rb.velocity = rb.velocity.normalized * Speed;
            }
        }
    }
    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
