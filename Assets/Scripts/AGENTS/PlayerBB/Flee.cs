using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : AgentBehaviour
{
    public override steering GetSteering()
    {
        steering steer = new steering();
        steer.linear = transform.position - target.transform.position;
        transform.up = steer.linear;
        steer.linear.Normalize();
        steer.linear = steer.linear * agent.maxAccel;
        return steer;
    }
}
