﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public float weight = 1.0f;

    public GameObject target;
    public Agent agent;
    protected Vector3 dest;

    public float maxSpeed = 50.0f;
    public float maxAccel = 50.0f;
    public float maxRotation = 5.0f;
    public float maxAngularAccel = 50.0f;
    public virtual void Start()
    {
        agent = gameObject.GetComponent<Agent>();
    }
    public virtual void Update()
    {
        agent.SetSteering(GetSteering(), weight);
    }
    public float MapToRange(float rotation)
    {
        rotation %= 360.0f;
        if (Mathf.Abs(rotation) > 180.0f)
        {
            if(rotation < 0.0f)
            {
                rotation += 360.0f;
            }
            else
            {
                rotation -= 360.0f;
            }
        }
        return rotation;
    }
    public virtual steering GetSteering()
    {
        return new steering();
    }
}
