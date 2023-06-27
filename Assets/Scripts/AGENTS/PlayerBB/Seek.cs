using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : AgentBehaviour
{
    //move towards target
    public override steering GetSteering()
    {
        if(target == null){
            steering steer = new steering();
            steer.linear = transform.position;
            transform.up = steer.linear;
            steer.linear.Normalize();
            steer.linear = steer.linear * agent.maxAccel;
            return steer;
        }
        else{
            steering steer = new steering();
            steer.linear = target.transform.position - transform.position;
            transform.up = steer.linear;
            steer.linear.Normalize();
            steer.linear = steer.linear * agent.maxAccel;
            return steer;
        }
    }
}
