using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swarmProjectile : MonoBehaviour
{
    public Vector3 velocity;
    public float speed = 10.0f;
    public float homingStrength = 1f; // A value between 0 and 1 that determines how strongly the missile homes in on the target.
    // public float maxLateralAcceleration = 10.0f;
    // public float lateralAccelerationPeriod = 0.5f;
    // private float lateralAccelerationTimer = 0.0f;
    // private float currentLateralAcceleration = 0.0f;
    private Transform target;
    private Vector3 lastKnownPos;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Update()
    {
        if(target != null){
            lastKnownPos = target.position;
        }
        
        if (target == null)
        {
            transform.position = Vector3.MoveTowards(transform.position,lastKnownPos, Time.deltaTime * speed);
            // Apply a random lateral offset to the position to create a more natural-looking swerve
            transform.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f);
            // If there is no target, just move forward in the current direction
            //transform.position += transform.up * speed * Time.deltaTime;
            return;
        }
        // Calculate the direction to the target and adjust the velocity to move in that direction
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 newVelocity = Vector3.Lerp(transform.up, targetDirection, homingStrength * Time.deltaTime);
        newVelocity.Normalize();
        // velocity = newVelocity * speed;

        // Apply the current velocity to the position
        //transform.position += velocity * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,target.position, Time.deltaTime * speed);
        transform.rotation = Quaternion.LookRotation(newVelocity);

        // // Update the lateral acceleration timer
        // lateralAccelerationTimer += Time.deltaTime;
        // if (lateralAccelerationTimer >= lateralAccelerationPeriod)
        // {
        //     // Change the lateral acceleration direction periodically
        //     currentLateralAcceleration = Random.Range(-maxLateralAcceleration, maxLateralAcceleration);
        //     lateralAccelerationTimer = 0.0f;
        // }

        // // Apply the lateral acceleration to the velocity
        // Vector3 lateralVelocity = new Vector3(-velocity.y, velocity.x, 0.0f) * currentLateralAcceleration;
        // velocity += lateralVelocity * Time.deltaTime;

        // Apply a random lateral offset to the position to create a more natural-looking swerve
        transform.position += new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0.0f);
    }

}