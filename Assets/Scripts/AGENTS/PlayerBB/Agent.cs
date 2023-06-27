using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    public float maxSpeed = 10.0f;
    public float truemaxSpeed;
    public float maxAccel = 30.0f;

    public float orientation;
    public float rotation;
    public Vector3 velocity;
    protected steering steer;

    public float maxRotation = 45.0f;
    public float maxAngularAccel = 45.0f;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        steer = new steering();
        truemaxSpeed = maxSpeed;
    }
    public void SetSteering(steering steer,float weight)
    {
        this.steer.linear += (weight * steer.linear);
        this.steer.angular += (weight * steer.angular);
    }
    // Update is called once per frame
    public virtual void Update()
    {
        Vector3 displacement = velocity * Time.deltaTime;
        displacement.z = 0;
        orientation += rotation * Time.deltaTime;
        if (orientation < 0.0f)
        {
            orientation += 360.0f;
        }
        else if(orientation > 360.0f)
        {
            orientation -= 360.0f;
        }
        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }
    public virtual void LateUpdate()
    {
        velocity += steer.linear * Time.deltaTime;
        rotation += steer.angular * Time.deltaTime;
        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if(steer.linear.magnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }
        steer = new steering();
    }
    public void speedReset()
    {
        maxSpeed = truemaxSpeed;
    }
}
