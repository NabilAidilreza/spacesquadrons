using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AgentBehaviour
{
    //move towards target 
    public override steering GetSteering()
    {
        steering steer = new steering();
        steer.linear = target.transform.position - transform.position;
        transform.up = steer.linear;
        steer.linear.Normalize();
        steer.linear = steer.linear * agent.maxAccel;
        return steer;
    }
}
